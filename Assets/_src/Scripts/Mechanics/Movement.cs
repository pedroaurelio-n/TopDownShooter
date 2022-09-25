using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public bool IsMoving { get; private set; }

        [Header("Move Settings")]
        [SerializeField, Range(0f, 10f)] private float moveSpeed = 3f;
        [SerializeField, Range(0f, 15f)] private float maxSpeed = 10f;
        [SerializeField, Range(0f, 20f)] private float posAccel = 5f;
        [SerializeField, Range(0f, 20f)] private float negAccel = 10f;

        [Header("Knockback Settings")]
        [SerializeField, Range(0f, 3f)] private float knockbackMultiplier = 1f;

        private Rigidbody2D _rigidbody;
        private Vector2 _currentDirection;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
        private void FixedUpdate() => Move();

        private void Move()
        {
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
            _rigidbody.velocity = clampedVelocity;
        }

        public void ApplyKnockback(Vector2 direction, float force)
        {
            _rigidbody.AddForce(knockbackMultiplier * force * direction.normalized, ForceMode2D.Impulse);
        }

        public void StopMovement() => _rigidbody.velocity = Vector2.zero;

        public void SetCurrentDirection(Vector2 direction) => _currentDirection = direction.normalized;

        public void SetCurrentDirection(InputAction.CallbackContext ctx) => SetCurrentDirection(ctx.ReadValue<Vector2>());
    }
}