using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.Utils;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, IKillable
    {
        public static List<Enemy> EnemyInstances { get; set; } = new();
        public Transform Target { get; set; }

        [Header("Death Settings")]
        [SerializeField] private IntEvent enemyScoreEvent;
        [SerializeField] private int defeatScore;
        [SerializeField] private GameObject deathParticles;

        [Header("Drop Settings")]
        [SerializeField] private List<Collectable> collectablePrefabs;

        private Animator _animator;

        private Movement _movement;
        private ShootBullets _shoot;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            TryGetComponent<Movement>(out _movement);
            _shoot = GetComponentInChildren<ShootBullets>();

            EnemyInstances.Add(this);
        }

        public void Disable()
        {
            _movement.StopMovement();
            _movement.enabled = false;

            if (_shoot)
            {
                _shoot.SetShootBool(false);
                _shoot.enabled = false;
            }
        }

        public void Damage()
        {
        }

        public void Death()
        {
            CheckForDrop();

            deathParticles.SetActive(true);
            deathParticles.transform.SetParent(LevelDependencies.Dynamic);
            
            enemyScoreEvent?.RaiseEvent(defeatScore);
            EnemyInstances.Remove(this);
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