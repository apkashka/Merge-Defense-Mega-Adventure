using CodeBase.Data;
using UniRx;
using UnityEngine;

namespace CodeBase.GamePlay.Monsters
{
    public class Monster : MonoBehaviour
    {
        public float Speed { get; private set; }

        public readonly IntReactiveProperty Health = new();

        public void Init(MonsterData data)
        {
            Speed = data.speed;
            Health.Value = data.health;
        }

        public override int GetHashCode()
        {
            return -37; //todo replace
        }
    }
}