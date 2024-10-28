using CodeBase.Data;
using CodeBase.Monsters;
using UniRx;
using UnityEngine;

namespace CodeBase.Systems
{
    public class UnitSystem: MonoBehaviour
    {
        private PoolList<Monster> _poolList;
    
        //todo container???
        public void Init(Transform container)
        {
            _poolList = new PoolList<Monster>(container);
            Observable.EveryUpdate().Subscribe(_ =>
            {
                foreach (var monster in _poolList)
                {
                    monster.Move();
                }
            }).AddTo(this);;
        }
    
        //todo positioning??
        public void CreateMonster(MonsterData data)
        {
            var monster = _poolList.GetOrCreate(data.prefab);
            monster.Init(data);
            monster.transform.localPosition = new Vector3(Random.Range(-5f, 6f), 0, 0);
            monster.Health.Subscribe(val =>
            {
                if (val <= 0)
                {
                    _poolList.MoveToUnused(monster);
                }
            }).AddTo(this);
        }
    }
}
