using CodeBase.GamePlay;
using CodeBase.Systems;
using CodeBase.ToRework.PoolList;
using UniRx;
using UnityEngine;

public class BulletSystem : SystemBehavior
{
    [SerializeField] private Bullet _bulletPrefab;
    private MultiplePool<Bullet> _bullets;

    public void Init()
    {
        _bullets = new MultiplePool<Bullet>(transform);
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (!Active)
            {
                return;
            }
            
            foreach (var bullet in _bullets.GetAll())
            {
                bullet.Move();
                bullet.CheckTime();
            }
        }).AddTo(this);
    }

    protected override void OnActive()
    {
        //todo make saloot of bullets
    }

    //todo separate init and creation
    public void CreateBullet(Vector3 position, Vector3 direction, float damage)
    {
        //todo norm
        var bullet = _bullets.GetOrCreate(_bulletPrefab);
        bullet.transform.position = position;
        bullet.Init(direction, damage);
        bullet.Hit += OnHit;
        bullet.OnTimer += OnTimer;
    }

    private void OnTimer(Bullet bullet)
    {
        bullet.OnTimer -= OnTimer;
        bullet.Hit -= OnHit;
        _bullets.Remove(bullet);    }

    private void OnHit(Bullet bullet)
    {
        bullet.Hit -= OnHit;
        bullet.OnTimer -= OnTimer;
        _bullets.Remove(bullet);
    }
    
    
}
