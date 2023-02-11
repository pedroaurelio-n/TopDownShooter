using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(Movement))]
    public class Pinball : MonoBehaviour
    {
        private Movement _movement;

        private void Awake() => _movement = GetComponent<Movement>();

        private void Start() => _movement.SetCurrentDirection(Random.insideUnitCircle.normalized);

        private void OnCollisionEnter2D(Collision2D other)
        {
            var hitNormal = other.contacts[0].normal;
            _movement.ReflectMovement(hitNormal);
        }
    }
}