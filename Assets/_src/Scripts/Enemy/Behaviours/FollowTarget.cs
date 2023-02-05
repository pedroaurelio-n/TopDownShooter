using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(Movement))]
    public class FollowTarget : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;

        private Aim _aim;
        private Movement _movement;

        private void Awake()
        {
            _aim = GetComponent<Aim>();
            _movement = GetComponent<Movement>();
        }

        private void Start()
        {
            if (TryGetComponent<Enemy>(out Enemy enemy))
                target = enemy.Target;

            if (target)
                _aim.SetAimDirection(target);
        }

        private void Update()
        {
            if (!target)
                return;
            
            _aim.SetAimDirection(target);
            _movement.SetCurrentDirection(_aim.LookDirection);
        }
    }
}