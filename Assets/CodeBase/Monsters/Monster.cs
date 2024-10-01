using System;
using CodeBase.Data;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.Monsters
{
    public class Monster : MonoBehaviour
    {
        public event Action Deactivated;

        private float _speed;
        private int _health;
        public void Init(MonsterData data)
        {
            _speed = data.speed;
            _health = data.health;
        }

        //todo вытащить, обдумать
        public void Move()
        {
            transform.Translate(Vector3.back * (_speed * Time.deltaTime));
        }

    }
}