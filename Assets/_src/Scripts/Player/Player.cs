using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour, IDestroyable
    {
        private Animator _animator;

        private Movement _movement;
        private Aim _aim;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
        }

        public void Destroy()
        {
            gameObject.SetActive(false);
        }
    }
}