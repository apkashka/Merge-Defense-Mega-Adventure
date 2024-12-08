using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _level;
        [SerializeField] private Transform _spawnPoint;

        public Transform SpawnPoint => _spawnPoint;
        public TowerData Data { get; private set; }
        private float _timer;

        public void Init(TowerData data, int level)
        {
            Data = data;
            _level.text = level.ToString();
        }
        
        public bool CanShoot()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
            {
                return false;
            }

            _timer = Data.reloadTime;
            return true;
        }
        
        public override int GetHashCode()
        {
            return 21;
        }


    }
}