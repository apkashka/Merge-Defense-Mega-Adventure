using CodeBase.Data;
using CodeBase.GamePlay;
using CodeBase.ToRework.PoolList;
using UniRx;
using UnityEngine;

namespace CodeBase.Systems
{
    public class TowerSystem : MonoBehaviour //todo separate build and tower control, buildField mix with field
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private MonsterSystem _monsterSystem;
        [SerializeField] private TowerData _towerData;
        [SerializeField] private Tower _towerPref;

        private MultiplePool<Tower> _towerPool;
        private Vector3 _closestMonster;
        
        public void Init()
        {
            _towerPool = new MultiplePool<Tower>(null);
            
            Observable.EveryUpdate().Subscribe(_ =>
            {
                _closestMonster = _monsterSystem.GetClosestMonster();

                foreach (var tower in _towerPool.GetAll())
                {
                    Rotate(tower);
                    Shoot(tower);
                }
            });  
        }
        
        private void Rotate(Tower tower)
        {
            var direction = _closestMonster - tower.SpawnPoint.position;
            var targetRotation = Quaternion.LookRotation(direction);
            tower.transform.rotation = Quaternion.Slerp(tower.transform.rotation,
                Quaternion.Euler(0, targetRotation.eulerAngles.y, 0), Time.deltaTime * 5f);
        }
        
        //todo block shot while rotating
        private void Shoot(Tower tower)
        {
            if (tower.CanShoot())
            {
                var direction = (_closestMonster - tower.SpawnPoint.position).normalized; 
                _bulletSystem.CreateBullet(tower.SpawnPoint.position, direction, tower.Data.damage);
            }
        }

        public Tower CreateTower(SpotData spotData)
        {
            var tower = _towerPool.GetOrCreate(_towerPref); ; //todo createTowerData
            tower.Init(_towerData, spotData.Level);
            return tower;
        }

        public void RemoveTower(Tower tower)
        {
            _towerPool.Remove(tower, false);
        }
        
    }
}