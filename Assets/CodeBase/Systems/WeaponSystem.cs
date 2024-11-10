using UniRx;
using UnityEngine;

namespace CodeBase.Systems
{
    public class WeaponSystem : MonoBehaviour
    {
        [SerializeField] private int _rotateLim = 30;
        [SerializeField] private float _shootTime = 0.5f;
        [SerializeField] private Weapon _weapon;

        [SerializeField] private Camera _camera; //todo move somewhere
    
        private BulletSystem _bulletSystem;

        private bool _active;
        public bool Active
        {
            get => _active;
            set
            {
                _weapon.gameObject.SetActive(_active);
                _active = value;
            }
        }

        private float _shootTimer;
    
        public void Construct(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }
        public void Init()
        {
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!Active)
                {
                    return;
                }
            
                RotateWeapon();
                Shoot();
            }).AddTo(this);
        }

        private void RotateWeapon()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            var screenPosition = Input.mousePosition;
            screenPosition.z = Mathf.Abs(_camera.transform.position.y - _weapon.transform.position.y);
            var worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            var direction = worldPosition - _weapon.transform.position;
            direction.y = 0;

            _weapon.transform.rotation = GetLimitedRotation(direction);
        }
    
        //todo упростить???
        private Quaternion GetLimitedRotation(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            var targetAngleY = targetRotation.eulerAngles.y;
            targetAngleY = Mathf.Repeat(targetAngleY + 180f, 360f) - 180f;
            targetAngleY = Mathf.Clamp(targetAngleY, -_rotateLim, _rotateLim);

            return Quaternion.Slerp(_weapon.transform.rotation, Quaternion.Euler(0, targetAngleY, 0), Time.deltaTime * 5f);
        }

        private void Shoot()
        {
            _shootTimer -= Time.deltaTime;
            if (_shootTimer > 0)
            {
                return;
            }

            var direction = (_weapon.SpawnPoint.position - _weapon.transform.position).normalized;
            _bulletSystem.CreateBullet(_weapon.SpawnPoint.position, direction, _weapon.Damage);
            _shootTimer = _shootTime;
        }
    }
}
