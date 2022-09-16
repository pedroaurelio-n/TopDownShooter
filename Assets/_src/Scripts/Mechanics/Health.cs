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

        private IDestroyable _destroyable;

        private void Awake()
        {
            Initialize();
            
            if (!TryGetComponent<IDestroyable>(out _destroyable))
                Debug.LogWarning($"Object with health doesn't have IDestroyable");
        }

        public void Initialize() => _currentHealth = maxHealth;

        private void Die()
        {
            _currentHealth = 0f;
            _destroyable.Destroy();
        }
    }
}