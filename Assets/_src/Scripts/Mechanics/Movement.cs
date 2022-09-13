using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDownShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        public bool IsMoving { get; private set; }

        [Header("Settings")]
        [SerializeField, Range(0f, 10f)] private float moveSpeed = 3f;
        [SerializeField, Range(0f, 15f)] private float maxSpeed = 10f;
        [SerializeField, Range(0f, 10f)] private float posAccel = 5f;
        [SerializeField, Range(0f, 10f)] private float negAccel = 10f;

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

        public void SetCurrentDirection(Vector2 direction) => _currentDirection = direction.normalized;

        public void SetCurrentDirection(InputAction.CallbackContext ctx) => SetCurrentDirection(ctx.ReadValue<Vector2>());
    }
}