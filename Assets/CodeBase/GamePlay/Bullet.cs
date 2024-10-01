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
    }
}