using System;
using CodeBase.Monsters;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class Bullet : MonoBehaviour
    {
        public Action<Bullet> Hit;
        [SerializeField] private Collider _col;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private Vector3 _direction;
        public void Init(Vector3 direction)
        {
            _direction = direction;

            //todo remove from collider
            _col.OnTriggerEnterAsObservable().Subscribe(col =>
            {
                var monster = col.GetComponent<Monster>();
                if (monster == null)
                {
                    Debug.LogError("Wrong hit, need to check!");
                    return;
                }
                
                Debug.Log("Hit occured");
                
                Hit?.Invoke(this);
                monster.Health.Value -= _damage;
            }).AddTo(this);
        }

        public void Move()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
        
        
        
        
    }
}