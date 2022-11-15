using UnityEngine;
 
namespace TopDownShooter
{
    public class Health : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private IntEvent healthModifiedEvent;
        [SerializeField] private float startHealth = 5f;
        [SerializeField] private float maxHealth = 5f;

        private float _currentHealth;

        private IKillable _killable;

        private void OnValidate()
        {
            if (startHealth > maxHealth)
                startHealth = maxHealth;
            
            if (maxHealth < startHealth)
                maxHealth = startHealth;
        }

        private void Awake()
        {
            if (!TryGetComponent<IKillable>(out _killable))
                Debug.LogWarning($"Object with health doesn't have IDestroyable");

            _currentHealth = startHealth;
        }

        private void Start() => healthModifiedEvent?.RaiseEvent((int)_currentHealth);

        public void ModifyHealth(float value)
        {
            _currentHealth += value;

            if (_currentHealth > maxHealth)
                _currentHealth = maxHealth;

            healthModifiedEvent?.RaiseEvent((int)_currentHealth);

            if (_currentHealth <= 0f)
            {
                _currentHealth = 0f;
                Die();
                return;
            }

            _killable.Damage();
        }

        private void Die()
        {
            _currentHealth = 0f;
            _killable.Death();
        }
    }
}