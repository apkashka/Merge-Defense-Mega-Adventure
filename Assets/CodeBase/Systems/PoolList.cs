using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Systems
{
    //todo НЕ ТРОГАТЬ ПОКА НЕ СДЕЛАЮ НОРМ ПО ПРОЕКТУ
    public class PoolList<T> : IEnumerable<T> where T : MonoBehaviour
    {
        private readonly Transform _container;
        private readonly List<T> _activeUnits;
        private readonly Stack<T> _unusedUnits;

        public PoolList(Transform container)
        {
            _container = container;
            _activeUnits = new DisposableList<T>();
            _unusedUnits = new Stack<T>();
        }

        //todo think about prefab, can't init pref without monsterData
        public T GetOrCreate(T prefab)
        {
            if (_unusedUnits.TryPop(out var oldElem))
            {
                oldElem.gameObject.SetActive(true);
                return oldElem;
            }

            var elem = Object.Instantiate(prefab, _container);
            _activeUnits.Add(elem);
            return elem;
        }

        //todo think
        public void MoveToUnused(T element)
        {
            //_activeUnits.AddToRemove(element);
            _activeUnits.Remove(element);
            element.gameObject.SetActive(false);
            _unusedUnits.Push(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _activeUnits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    //todo think about
    public class DisposableList<T> : List<T>, IDisposable
    {
        private readonly List<T> _toRemoveList = new();

        public void AddToRemove(T element)
        {
            _toRemoveList.Add(element);
        }

        public void Dispose()
        {
            if (_toRemoveList.Count == 0)
            {
                return;
            }

            foreach (var element in _toRemoveList)
            {
                Remove(element);
            }

            _toRemoveList.Clear();
        }
    }
}