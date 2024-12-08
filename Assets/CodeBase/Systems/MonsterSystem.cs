using System;
using System.Linq;
using CodeBase.Data;
using CodeBase.GamePlay.Monsters;
using CodeBase.Infrastructure.Services;
using CodeBase.ToRework.PoolList;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Systems
{
    public class MonsterSystem: SystemBehavior
    {
        [SerializeField] private Transform _lifeLine;
        [SerializeField] private float _xDelta;
        
        private MultiplePool<Monster> _poolList;
        private ProgressService _progress;
        private int _id;

        //todo container???
        public void Init(Transform container, ProgressService progress)
        {
            _poolList = new MultiplePool<Monster>(container);
            var posZ = _lifeLine.position.z;
            _progress = progress;
            
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!Active)
                {
                    return;
                }
                
                foreach (var monster in _poolList.GetAll())
                {
                    monster.transform.Translate(Vector3.back * (monster.Speed * Time.deltaTime));
                    if (monster.transform.position.z < posZ)
                    {
                        _poolList.Remove(monster);
                        _progress.Health.Value -= 20; //todo from monster??
                    }
                }
            }).AddTo(this);;
        }
    
        //todo positioning??
        public void CreateMonster(MonsterData data)
        {
            var monster = _poolList.GetOrCreate(data.prefab);
            monster.Init(data);
            var monsterName = "Monster_" + _id;
            monster.name = monsterName;
            _id++;//todo remove
            monster.transform.localPosition = new Vector3(Random.Range(-_xDelta, _xDelta), 0, 0);

            IDisposable monsterDispose = null;
            monsterDispose = monster.Health.Subscribe(val =>
            {
                if (val <= 0)
                {
                    Debug.Log($"{monsterName} killed: {Time.time}, health: {val}");
                    _poolList.Remove(monster);
                    monsterDispose?.Dispose(); //todo unsubscribe??
                    _progress.Gold.Value += 10; //todo to rewards
                }
            }).AddTo(monster);
        }

        public Vector3 GetClosestMonster()
        {
            var closest = _poolList.GetAll().OrderBy(monster => monster.transform.position.z).FirstOrDefault();
            if (closest != null)
            {
                return closest.transform.position;
            }
            
            //Debug.LogError("No Monsters but still searching"); //todo to think
            
            return Vector3.zero;
        }
    }
}
