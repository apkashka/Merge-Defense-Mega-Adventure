using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Systems
{
    //todo to use only inside MultiplePool
    public class PoolList<T> : IEnumerable<T> where T : MonoBehaviour
    {
        private const int MaxUnusedSize = 8; //todo

        private readonly Transform _container;
        private readonly DelayedList<T> _activeUnits;
        private readonly Stack<T> _unused;

        public PoolList(Transform container)
        {
            _container = container;
            _activeUnits = new DelayedList<T>();
            _unused = new Stack<T>();
        }

        public T GetOrCreate(T prefab)
        {
            if (_unused.TryPop(out var elem))
            {
                elem.gameObject.SetActive(true);
            }
            else
            {
                elem = Object.Instantiate(prefab, _container);
            }

            _activeUnits.Add(elem);
            return elem;
        }

        public void MoveToUnused(T element, bool withDelay)
        {
            if (withDelay)
            {
                _activeUnits.AddToRemove(element);
            }
            else
            {
                _activeUnits.Remove(element);
            }
            
            element.gameObject.SetActive(false);
            
            if (_unused.Count < MaxUnusedSize)
            {
                _unused.Push(element);
            }
            else
            {
                Object.Destroy(element.gameObject); // Удаление лишних объектов
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _activeUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void RemoveDelayed()
        {
            _activeUnits.DelayedRemove();
        }
    }

    public class DelayedList<T> : List<T>
    {
        private readonly List<T> _toRemoveList = new();

        public void AddToRemove(T element)
        {
            _toRemoveList.Add(element);
        }

        public void DelayedRemove()
        {
            if (_toRemoveList.Count == 0)
            {
                return;
            }
            
            foreach (var element in _toRemoveList)
            {
                Debug.Log($"{element} removed from active");
                Remove(element);
            }

            _toRemoveList.Clear();
        }
    }
}