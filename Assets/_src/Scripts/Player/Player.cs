using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour, IDestroyable
    {
        public Health Health { get; private set; }

        [SerializeField] private float collisionKnockback;

        private Animator _animator;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            Health = GetComponent<Health>();

            _animator = GetComponentInChildren<Animator>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                var direction = transform.position - enemy.transform.position;
                _movement.ApplyKnockback(direction, collisionKnockback);

                Health.HealthValue--;
                enemy.Destroy();
            }
        }
    }
}