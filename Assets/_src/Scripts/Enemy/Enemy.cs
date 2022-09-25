using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.Utils;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour, IDestroyable
    {
        public delegate void EnemySpawned(Enemy enemy);
        public static event EnemySpawned onEnemySpawned;

        public delegate void EnemyDefeated(Enemy enemy);
        public static event EnemyDefeated onEnemyDefeated;

        [Header("Score Settings")]
        [SerializeField] private IntEvent enemyScoreEvent;
        [SerializeField] private int defeatScore;

        [Header("Drop Settings")]
        [SerializeField] private List<Collectable> collectablePrefabs;

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

        protected virtual void Start() => onEnemySpawned?.Invoke(this);

        public void DisableMovement()
        {
            _Movement.StopMovement();
            _Movement.enabled = false;
        }

        public void Destroy()
        {
            CheckForDrop();
            
            enemyScoreEvent?.RaiseEvent(defeatScore);
            onEnemyDefeated?.Invoke(this);
            Destroy(gameObject);
        }

        private void CheckForDrop()
        {
            foreach (Collectable collectable in collectablePrefabs)
            {
                if (BoolUtils.CalculateRandomChance(collectable.DropChance))
                {
                    var c = Instantiate(collectable, transform.position, Quaternion.identity, LevelDependencies.Dynamic);
                    break;
                }
            }
        }
    }
}