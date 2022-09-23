using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    public class ParticlesManager : MonoBehaviour
    {
        public static ParticlesManager Instance;

        [SerializeField] private Particles pBullet;
        [SerializeField] private Particles pDamage;
        [SerializeField] private Particles eDamage;
        [SerializeField] private Particles eDeath;

        private ObjectPool<Particles> _pBulletPool;
        private ObjectPool<Particles> _pDamagePool;
        private ObjectPool<Particles> _eDamagePool;
        private ObjectPool<Particles> _eDeathPool;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            _pBulletPool = new ObjectPool<Particles>(() => OnCreateParticle(pBullet, _pBulletPool), OnGetParticle, OnReleaseParticle);
        }

        #region Pooling Methods
        private Particles OnCreateParticle(Particles p, ObjectPool<Particles> pool)
        {
            var particles = Instantiate(p, LevelDependencies.Dynamic);
            particles.SetPool(pool);
            return particles;
        }

        private void OnGetParticle(Particles particles) => particles.gameObject.SetActive(true);
        private void OnReleaseParticle(Particles particles) => particles.gameObject.SetActive(false);
        #endregion

        public void CreatePlayerBulletParticles(Vector3 position, Quaternion rotation)
        {
            var particles = _pBulletPool.Get();
            particles.Initialize(position, rotation);
        }
    }
}