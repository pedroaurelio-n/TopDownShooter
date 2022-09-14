using UnityEngine;
 
namespace TopDownShooter
{
    public class Aim : MonoBehaviour
    {
        public Vector2 LookDirection { get; private set; }

        [Header("Settings")]
        [SerializeField] private bool startFacingDirection;
        [SerializeField, Range(100f, 1500f)] private float rotationSpeed = 500f;

        private Transform _targetReference;

        private Vector2 _aimPosition;
        private Vector2 _rawDirection;

        private void Start()
        {
            if (startFacingDirection)
                SnapToAngle();
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, RotateTowardsAngle());
            LookDirection = transform.right;
        }

        private float GetDesiredAngle()
        {
            _rawDirection = _aimPosition - (Vector2)transform.position;
            return Mathf.Atan2(_rawDirection.y, _rawDirection.x) * Mathf.Rad2Deg;
        }

        public void SnapToAngle()
        {
            var desiredAngle = GetDesiredAngle();
            transform.rotation = Quaternion.Euler(0f, 0f, desiredAngle);
        }

        private float RotateTowardsAngle()
        {
            var desiredAngle = GetDesiredAngle();
            return Mathf.MoveTowardsAngle(transform.eulerAngles.z, desiredAngle, rotationSpeed * Time.deltaTime);
        }

        public void SetAimDirection(Vector2 position) => _aimPosition = position;
        public void SetAimDirection(Transform target)
        {
            _targetReference = target;
            SetAimDirection(_targetReference.position);
        }
    }
}