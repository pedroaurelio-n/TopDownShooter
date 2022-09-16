using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(Movement))]
    public class FollowTarget : Enemy
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;

        private void Update()
        {
            if (target == null)
                return;
                
            _Aim.SetAimDirection(target);
            _Movement.SetCurrentDirection(_Aim.LookDirection);
        }
    }
}