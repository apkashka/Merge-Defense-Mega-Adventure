using System.Collections;
using CodeBase.Data;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class BattleGround : MonoBehaviour
    {
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
            foreach (var wave in _levelData.waves)
            {
                for (int i = 0; i < wave.amount; i++)
                {
                    _monsterSystem.CreateMonster(wave.GetMonsterData());
                }

                yield return new WaitForSeconds(_levelData.delay);
            }
            
            Debug.Log("Waves Generation Finished");
        }
    }
}