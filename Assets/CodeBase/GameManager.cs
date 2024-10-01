using CodeBase.GamePlay;
using UnityEngine;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UnitSystem _unitSystem;
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Level _level;

        private void Start()
        {
            _unitSystem.Init(_level.SpawnPoint);
            _bulletSystem.Init();
            _weaponSystem.Construct(_bulletSystem);
            _weaponSystem.Init();
            _level.Construct(_unitSystem);
            _level.StartLevel();
        }
    }
}