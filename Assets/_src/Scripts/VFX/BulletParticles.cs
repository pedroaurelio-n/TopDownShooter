using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class BulletParticles : MonoBehaviour
    {
        private Bullet _bullet;

        private bool _isLastParticle;

        public void Initialize(Bullet bullet, bool isLastParticle)
        {
            _bullet = bullet;
            _isLastParticle = isLastParticle;
        }

        private void OnParticleSystemStopped()
        {
            if (_isLastParticle)
            {
                transform.SetParent(_bullet.transform);
                BulletManager.Instance.ReleaseBullet(_bullet);
                return;
            }

            Destroy(gameObject);
        }
    }
}