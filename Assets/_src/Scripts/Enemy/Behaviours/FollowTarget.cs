using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Aim))]
    [RequireComponent(typeof(Movement))]
    public class FollowTarget : Enemy
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;

        protected override void Start()
        {
            base.Start();
            
            if (target == null)
                target = LevelDependencies.Player.transform;
                
            _Aim.SetAimDirection(target);
        }

        private void Update()
        {
            _Aim.SetAimDirection(target);
            _Movement.SetCurrentDirection(_Aim.LookDirection);
        }
    }
}