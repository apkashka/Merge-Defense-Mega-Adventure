using CodeBase.GamePlay.Monsters;
using UnityEngine;

namespace CodeBase.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Monster", fileName = "Monster")]
    public class MonsterData : ScriptableObject
    {
        public Monster prefab;
        public float speed;
        public int health;
    }
}