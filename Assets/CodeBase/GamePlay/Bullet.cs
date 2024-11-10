using System;
using CodeBase.GamePlay.Monsters;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class Bullet : MonoBehaviour
    {
        private const int BulletTime = 10; //todo move somewhere
        
        public Action<Bullet> Hit;
        public Action<Bullet> OnTimer;

        [SerializeField] private Collider _col;
        [SerializeField] private float _speed;
        
        private float _damage;
        private Vector3 _direction;
        private float _timer;

        private void Awake()
        {
            _col.OnTriggerEnterAsObservable().Subscribe(col =>
            {
                var monster = col.GetComponent<Monster>();
                if (monster == null)
                {
                    Debug.LogError("Wrong hit, need to check!");
                    return;
                }
//                Debug.Log("Hit occured");

                Hit?.Invoke(this);
                monster.Health.Value -= (int)_damage; //todo all to float or int, move somewhere
            }).AddTo(this);
            //todo remove from collider

        }

        public void Init(Vector3 direction, float damage)
        {
            _direction = direction;
            _timer = BulletTime;
            _damage = damage;
        }

        public void Move()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }

        public void CheckTime()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                OnTimer?.Invoke(this);
            }
        }

        public override int GetHashCode()
        {
            return 37;
        }
    }
}