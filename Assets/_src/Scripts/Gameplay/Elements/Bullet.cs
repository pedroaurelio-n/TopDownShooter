using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(ApplyDamage))]
    public class Bullet : MonoBehaviour, IKillable
    {
        public BulletSO BulletSO { get; set; }

        [Header("Dependencies")]
        [SerializeField] private GameObject vfxObject;

        [Header("Bullet Settings")]
        [SerializeField] private float knockbackForce;
        [SerializeField] private BulletParticles bulletParticles;

        private Health _health;
        private ApplyDamage _damage;

        private SpriteRenderer _spriteRenderer;
        private Transform _spriteTransform;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider2D;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _damage = GetComponent<ApplyDamage>();

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteTransform = _spriteRenderer.transform;
            _collider2D = GetComponent<CapsuleCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            ActivateComponents(true);

            _health.SetCurrentHealth(BulletSO.Health);
            _damage.SetDamage(BulletSO.Damage);

            _spriteRenderer.sprite = BulletSO.Sprite;
            _spriteRenderer.color = BulletSO.Color;
            _spriteTransform.localScale = BulletSO.Size;
            _collider2D.size = BulletSO.ColliderSize;
        }

        public void Setup(Vector3 position, Vector3 rotation, float speed)
        {
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

        private void ActivateComponents(bool value)
        {
            bulletParticles.gameObject.SetActive(!value);

            _spriteRenderer.enabled = value;
            _collider2D.enabled = value;
            vfxObject.SetActive(value);
        }

        public void Damage()
        {
        }

        public void Death()
        {
            _rigidbody.velocity = Vector2.zero;
            ActivateComponents(false);

            bulletParticles.transform.SetParent(LevelDependencies.Dynamic);
            bulletParticles.Initialize(this);
        }
    }
}