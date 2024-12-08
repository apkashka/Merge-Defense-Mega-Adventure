using System.Collections;
using CodeBase.Data;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class BattleGround : MonoBehaviour
    {

        public System.Action LastWaveCreated;
        [SerializeField] private Transform _spawnPoint;
        
        private LevelData _levelData;
        public Transform SpawnPoint => _spawnPoint;
        private MonsterSystem _monsterSystem;
        private Coroutine _wavesRoutine;

        public void Construct(MonsterSystem monsterSystem)
        {
            _monsterSystem = monsterSystem;
        }

        public void StartLevel(LevelData level) //todo check if need to move
        {
            _levelData = level;
            if (_levelData.waves.Length == 0)
            {
                Debug.LogError("No waves selected");
                return;
            }

            _wavesRoutine = StartCoroutine(StartWavesRoutine());
        }
        
        //todo to tweens or else
        private IEnumerator StartWavesRoutine()
        {
            yield return new WaitForSeconds(1);
            for (var index = 0; index < _levelData.waves.Length; index++)
            {
                var wave = _levelData.waves[index];
                for (int i = 0; i < wave.monsterCount; i++)
                {
                    _monsterSystem.CreateMonster(wave.GetMonsterData());
                }

                if (index == _levelData.waves.Length - 1)
                {
                    LastWaveCreated?.Invoke();
                }
                yield return new WaitForSeconds(_levelData.delay);
            }

            Debug.Log("Waves Generation Finished");
        }
    }
}