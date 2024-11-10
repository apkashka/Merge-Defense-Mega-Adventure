using CodeBase.GamePlay;
using CodeBase.Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {
        [FormerlySerializedAs("_unitSystem")] [SerializeField] private MonsterSystem monsterSystem;
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private TowerSystem _towerSystem;
        
        
        [SerializeField] private Level _level;

        [SerializeField] private BuildField _buildField;
        private void Start()
        {
            monsterSystem.Init(_level.SpawnPoint);
            _bulletSystem.Init();
            
            _weaponSystem.Construct(_bulletSystem);
            _weaponSystem.Init();

            _towerSystem.Init(); //todo construct??
            var field = new Field();
            _buildField.Init(field);

            _level.Construct(monsterSystem);
            _level.StartLevel();
        }
    }
}