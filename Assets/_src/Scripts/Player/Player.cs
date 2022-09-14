using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform shootPos;
        private Animator _animator;
        private SpriteRenderer _sprite;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }
    }
}