using System;
using CodeBase.Data;
using CodeBase.Systems;
using UniRx;
using UnityEngine;

namespace CodeBase.Monsters
{
    public class Monster : MonoBehaviour
    {
        private float _speed;
        public IntReactiveProperty Health = new();
        public void Init(MonsterData data)
        {
            _speed = data.speed;
            Health.Value = data.health;
        }

        //todo вытащить, обдумать
        public void Move()
        {
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
        }

    }
}