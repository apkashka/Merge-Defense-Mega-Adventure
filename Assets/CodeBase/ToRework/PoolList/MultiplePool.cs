using System.Collections.Generic;
using System.Linq;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.ToRework.PoolList
{
    public class MultiplePool<T> where T : MonoBehaviour
    {
        private readonly Dictionary<int, PoolList<T>> _poolsDic;
        private readonly Transform _container;

        public MultiplePool(Transform container)
        {
            _poolsDic = new Dictionary<int, PoolList<T>>();
            _container = container;
        }

        public T GetOrCreate(T prefab)
        {
            if (!_poolsDic.TryGetValue(prefab.GetHashCode(), out var pool))
            {
                pool = new PoolList<T>(_container);
                _poolsDic.Add(prefab.GetHashCode(), pool);
            }

            return pool.GetOrCreate(prefab);
        }

        //todo separate container
        public void Remove(T element, bool fromForeach = true)
        {
            var pool = _poolsDic[element.GetHashCode()]; //todo GetHashCode to interface or prefab name??
            pool.MoveToUnused(element, fromForeach);
        }

        public IEnumerable<T> GetAll()
        {
            foreach (var poolList in _poolsDic.Values)
            {
                poolList.RemoveDelayed();
            }

            return _poolsDic.Values.SelectMany(pool => pool);
        }
    }
}