using System.Linq;
using CodeBase.Data;
using CodeBase.GamePlay.Monsters;
using CodeBase.ToRework.PoolList;
using UniRx;
using UnityEngine;

namespace CodeBase.Systems
{
    public class MonsterSystem: MonoBehaviour
    {
        [SerializeField] private Transform _lifeLine;
        private MultiplePool<Monster> _poolList;
        private int _id;
        
        //todo container???
        public void Init(Transform container)
        {
            _poolList = new MultiplePool<Monster>(container);
            var posZ = _lifeLine.position.z;
            Observable.EveryUpdate().Subscribe(_ =>
            {
                foreach (var monster in _poolList.GetAll())
                {
                    monster.transform.Translate(Vector3.back * (monster.Speed * Time.deltaTime));
                    if (monster.transform.position.z < posZ)
                    {
                        _poolList.Remove(monster);
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
            _id++;
            monster.transform.localPosition = new Vector3(Random.Range(-5f, 6f), 0, 0);
            
            monster.Health.Subscribe(val =>
            {
                if (val <= 0)
                {
                    Debug.Log($"{monsterName} killed: {Time.time}, health: {val}");
                    _poolList.Remove(monster);
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
