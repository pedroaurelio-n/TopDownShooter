using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.Utils;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Health))]
    public abstract class Enemy : MonoBehaviour, IDestroyable
    {
        [Header("Score Settings")]
        [SerializeField] private IntEvent enemyDefeatedEvent;
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

        public void Destroy()
        {
            CheckForDrop();
            
            enemyDefeatedEvent?.RaiseEvent(defeatScore);
            gameObject.SetActive(false);
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