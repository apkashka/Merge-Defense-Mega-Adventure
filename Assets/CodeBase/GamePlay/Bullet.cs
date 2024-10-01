using System;
using CodeBase.Systems;
using UnityEngine;

namespace CodeBase.GamePlay
{
    public class Bullet : MonoBehaviour
    {
        public event Action Deactivated;
        
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        private Vector3 _direction;
        public void Init(Vector3 direction)
        {
            _direction = direction;
        }

        public void Move()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
        
        
    }
}