using UnityEngine;
using PedroAurelio.EventSystem;
 
namespace PedroAurelio.TopDownShooter
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

        public void SetCurrentHealth(float health)
        {
            var h = Mathf.Min(health, maxHealth);
            _currentHealth = h;
        }

        public void IncreaseHealth(float value)
        {
            ModifyHealth(value, out _);
        }

        public void IncreaseHealth(float value, out bool reachedMax)
        {
            ModifyHealth(value, out reachedMax);
        }

        public void DecreaseHealth(float value)
        {
            ModifyHealth(value, out _);
            
            if (_currentHealth == 0f)
            {
                _killable.Death();
                return;
            }

            _killable.Damage();
        }

        private void ModifyHealth(float value, out bool max)
        {
            max = false;

            _currentHealth += value;

            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
                max = true;
            }

            if (_currentHealth < 0f)
                _currentHealth = 0f;

            healthModifiedEvent?.RaiseEvent((int)_currentHealth);
        }
    }
}