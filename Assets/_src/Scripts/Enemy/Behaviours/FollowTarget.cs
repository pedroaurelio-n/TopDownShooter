using UnityEngine;
 
namespace TopDownShooter
{
    public class FollowTarget : Enemy
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;

        private void Update()
        {
            _Aim.SetAimDirection(target);
            _Movement.SetCurrentDirection(_Aim.LookDirection);
        }
    }
}