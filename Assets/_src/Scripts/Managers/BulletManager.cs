using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }

        [SerializeField] private Bullet baseBullet;
        [SerializeField] private Transform dynamic;

        private BulletPool _pool;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            _pool = new BulletPool(baseBullet);
        }

        public Bullet GetBullet(BulletSO bulletSO, Bullet bulletPrefab)
        {
            var bullet = _pool.GetObject(bulletSO, bulletPrefab);
            bullet.transform.SetParent(dynamic);
            return bullet;
        }

        public void ReleaseBullet(Bullet bullet)
        {
            _pool.ReleaseObject(bullet);
        }
    }
}