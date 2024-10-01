using CodeBase.GamePlay;
using CodeBase.Systems;
using UniRx;
using UnityEngine;

public class BulletSystem : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    
    private PoolList<Bullet> _bullets;

    public void Init()
    {
        _bullets = new PoolList<Bullet>(transform);
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            MoveBullets();
        }).AddTo(this);
    }

    private void MoveBullets()
    {
        foreach (var bullet in _bullets)
        {
            bullet.Move();
        }
    }

    public void CreateBullet(Vector3 position, Vector3 direction)
    {
        var bullet = _bullets.GetOrCreate(_bulletPrefab);
        bullet.transform.position = position;
        bullet.Init(direction);
    }
}
