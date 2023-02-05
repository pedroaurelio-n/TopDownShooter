using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class ApplyDamage : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float damage;
        [SerializeField] private LayerMask damageLayers;

        private float _damage;

        private void Awake()
        {
            _damage = damage;
        }

        private void OnValidate()
        {
            if (damage < 0f)
                damage = 0f;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        private void CheckForDamage(GameObject other)
        {
            var objectLayer = other.layer;
            var objectIsInDamageLayer = (1 << objectLayer & damageLayers) != 0;

            if (objectIsInDamageLayer)
            {
                if (other.TryGetComponent<Health>(out Health objectHealth))
                    objectHealth.DecreaseHealth(-damage);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) => CheckForDamage(other.gameObject);
        private void OnTriggerEnter2D(Collider2D other) => CheckForDamage(other.gameObject);
    }
}