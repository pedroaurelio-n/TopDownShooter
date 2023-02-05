using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class BulletParticles : MonoBehaviour
    {
        private Bullet _bullet;

        public void Initialize(Bullet bullet)
        {
            _bullet = bullet;
        }

        private void OnParticleSystemStopped()
        {
            transform.SetParent(_bullet.transform);
            BulletManager.Instance.ReleaseBullet(_bullet);
        }
    }
}