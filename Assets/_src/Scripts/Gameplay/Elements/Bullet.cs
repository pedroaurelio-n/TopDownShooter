using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class Bullet : MonoBehaviour, IKillable
    {
        [field: Header("Dependencies")]
        [field: SerializeField] public BulletSO BulletSO { get; set; }
        [SerializeField] private GameObject vfxObject;

        [field: Header("Settings")]
        [SerializeField] private float knockbackForce;
        [SerializeField] private BulletParticles bulletParticles;

        private SpriteRenderer _spriteRenderer;
        private Transform _spriteTransform;
        private Rigidbody2D _rigidbody;
        private CapsuleCollider2D _collider2D;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteTransform = _spriteRenderer.transform;
            _collider2D = GetComponent<CapsuleCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            ActivateComponents(true);

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