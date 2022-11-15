using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class Bullet : MonoBehaviour, IKillable
    {
        [Header("Settings")]
        [SerializeField] private float knockbackForce;
        [SerializeField] private GameObject collisionParticles;

        private Rigidbody2D _rigidbody;

        // private IObjectPool<Bullet> _pool;
        // private bool _isActiveOnPool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        public void Initialize(Vector3 position, Vector3 rotation, float speed)
        {
            // _isActiveOnPool = true;

            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Movement>(out Movement targetMovement))
            {
                var direction = targetMovement.transform.position - transform.position;
                targetMovement.ApplyKnockback(direction, knockbackForce);
            }
        }

        public void Damage()
        {
        }

        public void Death()
        {
            collisionParticles.SetActive(true);
            collisionParticles.transform.SetParent(LevelDependencies.Dynamic);
            Destroy(gameObject);
            // if (_isActiveOnPool)
            // {
            //     _pool.Release(this);
            //     _isActiveOnPool = false;
            // }
        }
    }
}