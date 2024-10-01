using CodeBase.GamePlay;
using UnityEngine;

namespace CodeBase
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UnitSystem _unitSystem;
        [SerializeField] private WeaponSystem _weaponSystem;
        [SerializeField] private Level _level;

        private void Start()
        {
            _unitSystem.Init(_level.SpawnPoint);
            _weaponSystem.Init();
            _level.Construct(_unitSystem);
            _level.StartLevel();
        }
    }
}