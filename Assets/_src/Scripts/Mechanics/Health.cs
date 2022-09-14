using UnityEngine;
 
namespace TopDownShooter
{
    public class Health : MonoBehaviour
    {
        public float HealthValue
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;

                if (_currentHealth <= 0f)
                    Die();
            }
        }

        [Header("Settings")]
        [SerializeField] private float maxHealth = 100f;

        private float _currentHealth;

        private void Awake() => _currentHealth = maxHealth;

        private void Die()
        {
            _currentHealth = 0f;
            gameObject.SetActive(false);
        }
    }
}