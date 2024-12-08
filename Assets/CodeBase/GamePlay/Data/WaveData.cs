using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Wave", fileName = "Wave" )]
    public class WaveData : ScriptableObject
    {
        public MonsterData[] monsters;
        public int[] ratio; 
        public int monsterCount;

        public MonsterData GetMonsterData()
        {
            //todo ограничить на стадии внесения данных
            if (monsters.Length != ratio.Length)
            {
                Debug.LogError("Monsters and ratios don't match!");
                return monsters[0];
            }

            if (monsters.Length == 0)
            { 
                return monsters[0];
            }

            var sumRatio = ratio.Sum();
            var unitNumber = Random.Range(0, sumRatio);

            var number = 0;
            for (int i = 0; i < ratio.Length; i++)
            {
                number += ratio[i];
                if (unitNumber < number)
                {
                    return monsters[i];
                }
            }
            
            Debug.LogError("Unit number is out of range!");
            return monsters[0];
        }
        
    }
}