using UnityEngine;
using UnityEngine.InputSystem;

namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public bool IsMoving { get; private set; }

        [Header("Move Settings")]
        [SerializeField] private bool willAccelerate;
        [SerializeField, Range(0f, 10f)] private float moveSpeed = 3f;
        [SerializeField, Range(0f, 15f)] private float maxSpeed = 10f;
        [SerializeField, Range(0f, 20f)] private float posAccel = 5f;
        [SerializeField, Range(0f, 20f)] private float negAccel = 10f;

        [Header("Knockback Settings")]
        [SerializeField, Range(0f, 3f)] private float knockbackMultiplier = 1f;
        [SerializeField, Range(0f, 5f)] private float knockbackDecrease = 1f;

        private Rigidbody2D _rigidbody;
        private Vector2 _currentDirection;
        private Vector2 _knockbackVector;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
        private void FixedUpdate()
        {
            _knockbackVector = Vector2.MoveTowards(_knockbackVector, Vector2.zero, knockbackDecrease);
            Move();
        }

        private void Move()
        {
            if (!willAccelerate)
                return;

            Vector2 targetSpeed;
            float acceleration;

            IsMoving = _currentDirection != Vector2.zero;

            if (IsMoving)
            {
                targetSpeed = _currentDirection * moveSpeed;
                acceleration = posAccel;
            }
            else
            {
                targetSpeed = -_rigidbody.velocity;
                acceleration = negAccel;
            }

            _rigidbody.AddForce(targetSpeed * acceleration);

            var clampedVelocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
            _rigidbody.velocity = clampedVelocity + _knockbackVector;
        }

        public void ApplyKnockback(Vector2 direction, float force)
        {
            _knockbackVector = knockbackMultiplier * force * direction.normalized;
        }

        public void ReflectMovement(Vector2 normal)
        {
            var newDirection = Vector2.Reflect(_currentDirection, normal);
            SetCurrentDirection(newDirection);
        }

        public void StopMovement()
        {
            _currentDirection = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
        }

        public Vector2 GetCurrentDirection()
        {
            return _currentDirection;
        }

        public void SetCurrentDirection(Vector2 direction)
        {
            _currentDirection = direction.normalized;

            if (willAccelerate)
                return;

            _rigidbody.velocity = _currentDirection * moveSpeed;
        }

        public void SetCurrentDirection(InputAction.CallbackContext ctx) => SetCurrentDirection(ctx.ReadValue<Vector2>());
    }
}