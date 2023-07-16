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
        [SerializeField, Range(0f, 20f)] private float moveSpeed = 3f;
        [SerializeField, Range(0f, 20f)] private float maxSpeed = 10f; 
        [SerializeField, Range(0f, 20f)] private float posAccel = 5f;
        [SerializeField, Range(0f, 20f)] private float negAccel = 10f;

        [Header("Knockback Settings")]
        [SerializeField, Range(0f, 10f)] private float knockbackMultiplier = 1f;

        private Rigidbody2D _rigidbody;
        private Vector2 _currentDirection;
        private Vector2 _knockbackVector;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (!willAccelerate)
                return;

            IsMoving = _currentDirection != Vector2.zero;

            var targetVelocity = _currentDirection * moveSpeed;
            var acceleration = IsMoving ? posAccel : negAccel;

            var velocityChange = targetVelocity - _rigidbody.velocity;
            var force = velocityChange / Time.fixedDeltaTime;
            force *= _rigidbody.mass * acceleration;
            _rigidbody.AddForce(force * Time.deltaTime, ForceMode2D.Force);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }

        public void ApplyKnockback(Vector2 direction, float force)
        {
            _knockbackVector = knockbackMultiplier * force * direction.normalized;
            _rigidbody.AddForce(_knockbackVector  * _rigidbody.mass, ForceMode2D.Impulse);
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