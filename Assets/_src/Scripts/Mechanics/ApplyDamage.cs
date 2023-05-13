using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class ApplyDamage : MonoBehaviour
    {
        [Header("Damage Settings")]
        [SerializeField] private float damage;
        [SerializeField] private LayerMask damageLayers;

        [Header("Knockback Settings")]
        [SerializeField] private float knockbackForce;

        private void OnValidate()
        {
            if (damage < 0f)
                damage = 0f;
        }

        private void CheckForDamage(GameObject other)
        {
            var objectLayer = other.layer;
            var objectIsInDamageLayer = (1 << objectLayer & damageLayers) != 0;

            if (!objectIsInDamageLayer)
                return;

            if (other.TryGetComponent<Health>(out Health objectHealth))
                objectHealth.DecreaseHealth(-damage, gameObject);

            if (other.TryGetComponent<Movement>(out Movement targetMovement))
            {
                var direction = targetMovement.transform.position - transform.position;
                targetMovement.ApplyKnockback(direction, knockbackForce);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) => CheckForDamage(other.gameObject);
        private void OnTriggerEnter2D(Collider2D other) => CheckForDamage(other.gameObject);
    }
}