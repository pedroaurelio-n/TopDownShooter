using UnityEngine;
using PedroAurelio.EventSystem;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour, IKillable
    {
        public Health Health { get; private set; }
        public ShootBullets Shoot { get; private set; }

        [SerializeField] private GameEvent deathEvent;
        [SerializeField] private float collisionKnockback;

        private Animator _animator;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            Health = GetComponent<Health>();
            Shoot = GetComponentInChildren<ShootBullets>();

            _animator = GetComponentInChildren<Animator>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }

        public void Damage()
        {
        }

        public void Death()
        {
            deathEvent?.RaiseEvent();
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                var direction = transform.position - enemy.transform.position;
                _movement.ApplyKnockback(direction, collisionKnockback);
            }
        }
    }
}