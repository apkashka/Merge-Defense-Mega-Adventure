using System.Collections;
using System.Collections.Generic;
using CodeBase.GamePlay;
using CodeBase.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private int _rotateLim = 30;
    [SerializeField] private float _shootTime = 0.2f;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Bullet _bulletPref;
    [SerializeField] private Camera _camera; //todo move somewhere

    private PoolList<Bullet> _bullets;
    
    public bool Active { get; set; }
    private float _shootTimer;


    public void Construct()
    {
        
    }
    public void Init()
    {
        _bullets = new PoolList<Bullet>(_weapon.SpawnPoint);
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            //MoveBullets();
            RotateWeapon();

            if (!Active)
            {
                return;
            }

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
        var targetRotation = Quaternion.LookRotation(direction); // todo slerp??
        if (targetRotation.eulerAngles.y > _rotateLim)
        {
            targetRotation.y = _rotateLim;
        }
        else if (targetRotation.eulerAngles.y < -_rotateLim)
        {
            targetRotation.y = -_rotateLim;
        }
        _weapon.transform.rotation = Quaternion.Slerp(_weapon.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    private void Shoot()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer > 0)
        {
            return;
        }

        var bullet = _bullets.GetOrCreate(_bulletPref);

    }
}
