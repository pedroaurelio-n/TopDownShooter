using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Movement))]
    public class Pinball : MonoBehaviour
    {
        private Movement _movement;

        private Vector2 _moveDirection;

        private void Awake()
        {
            _movement = GetComponent<Movement>();

            _moveDirection = Random.insideUnitCircle.normalized;
            _movement.SetCurrentDirection(_moveDirection);
        }

        private void ReflectVelocity(Vector2 normal)
        {
            var newDirection = Vector2.Reflect(_moveDirection, normal);

            _movement.SetCurrentDirection(newDirection);
            _moveDirection = newDirection;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var hitNormal = other.contacts[0].normal;
            ReflectVelocity(hitNormal);
        }
    }
}