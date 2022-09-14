using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float damage;

        private Rigidbody2D _rigidbody;

        private IObjectPool<Bullet> _pool;
        private bool _isActiveOnPool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetPool(IObjectPool<Bullet> pool) => _pool = pool;

        public void Initialize(Vector3 position, Vector3 rotation, float speed)
        {
            _isActiveOnPool = true;

            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Health>(out Health target))
                target.HealthValue -= damage;
                
            if (_isActiveOnPool)
            {
                _pool.Release(this);
                _isActiveOnPool = false;
            }
        }
    }
}