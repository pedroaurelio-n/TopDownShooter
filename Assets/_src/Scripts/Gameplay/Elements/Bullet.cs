using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ApplyDamage))]
    public class Bullet : MonoBehaviour, IKillable
    {
        public BulletSO BulletSO { get; set; }

        [Header("Dependencies")]
        [SerializeField] private GameObject vfxObject;
        [SerializeField] private BulletParticles collisionParticles;

        [Header("Bullet Settings")]
        [SerializeField] private float knockbackForce;

        private Health _health;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider2D;

        private void Awake()
        {
            _health = GetComponent<Health>();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _collider2D = GetComponent<CapsuleCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            ActivateComponents(true);
        }

        public void Setup(Vector3 position, Vector3 rotation, float speed)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
            _rigidbody.velocity = transform.right * speed;

            _health.Initialize();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Movement>(out Movement targetMovement))
            {
                var direction = targetMovement.transform.position - transform.position;
                targetMovement.ApplyKnockback(direction, knockbackForce);
            }
        }

        private void ActivateComponents(bool value)
        {
            collisionParticles.gameObject.SetActive(!value);

            _spriteRenderer.enabled = value;
            _collider2D.enabled = value;
            vfxObject.SetActive(value);
        }

        public void Damage()
        {
            var particles = Instantiate(collisionParticles, transform.position, transform.rotation, LevelDependencies.Dynamic);
            particles.gameObject.SetActive(true);
            particles.Initialize(this, false);
        }

        public void Death()
        {
            _rigidbody.velocity = Vector2.zero;
            ActivateComponents(false);

            collisionParticles.transform.SetParent(LevelDependencies.Dynamic);
            collisionParticles.Initialize(this, true);
        }
    }
}