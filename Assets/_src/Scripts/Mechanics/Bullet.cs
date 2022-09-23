using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class Bullet : MonoBehaviour, IDestroyable
    {
        [Header("Settings")]
        [SerializeField] private float damage;
        [SerializeField] private float knockbackForce;
        [SerializeField] private bool collideWithEnemyBullets;

        private Rigidbody2D _rigidbody;
        private Health _health;

        private IObjectPool<Bullet> _pool;
        private bool _isActiveOnPool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        public void Initialize(Vector3 position, Vector3 rotation, float speed)
        {
            _isActiveOnPool = true;
            _health.Initialize();

            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Health>(out Health target))
            {
                if (!collideWithEnemyBullets && target.TryGetComponent<Bullet>(out Bullet targetBullet))
                    return;

                if (target.TryGetComponent<Movement>(out Movement targetMovement))
                {
                    var direction = targetMovement.transform.position - transform.position;
                    targetMovement.ApplyKnockback(direction, knockbackForce);
                }
                
                target.ModifyHealth(-damage);
                _health.ModifyHealth(-1f);
                return;
            }

            _health.ModifyHealth(Mathf.NegativeInfinity);
        }

        public void Destroy()
        {
            if (_isActiveOnPool)
            {
                // _pool.Release(this);
                Destroy(gameObject);
                _isActiveOnPool = false;
            }
        }
    }
}