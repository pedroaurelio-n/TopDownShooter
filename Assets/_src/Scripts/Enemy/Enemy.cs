using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour, IDestroyable
    {
        [SerializeField] private IntEvent enemyDefeatedEvent;
        [SerializeField] private int defeatScore;

        private Animator _animator;

        protected Movement _Movement;
        protected Aim _Aim;
        protected ShootBullets _Shoot;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            TryGetComponent<Movement>(out _Movement);
            TryGetComponent<Aim>(out _Aim);
            _Shoot = GetComponentInChildren<ShootBullets>();
        }

        public void Destroy()
        {
            enemyDefeatedEvent?.RaiseEvent(defeatScore);
            gameObject.SetActive(false);
        }
    }
}