using UnityEngine;
 
namespace TopDownShooter
{
    public class Health : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private IntEvent healthModifiedEvent;
        [SerializeField] private float startHealth = 5f;

        private float _currentHealth;

        private IDestroyable _destroyable;

        private void Awake()
        {
            if (!TryGetComponent<IDestroyable>(out _destroyable))
                Debug.LogWarning($"Object with health doesn't have IDestroyable");
        }

        private void Start() => Initialize();

        public void Initialize()
        {
            _currentHealth = startHealth;
            healthModifiedEvent?.RaiseEvent((int)_currentHealth);
        }

        public void ModifyHealth(float value)
        {
            _currentHealth += value;

            healthModifiedEvent?.RaiseEvent((int)_currentHealth);

            if (_currentHealth <= 0f)
                Die();
        }

        private void Die()
        {
            _currentHealth = 0f;
            _destroyable.Destroy();
        }
    }
}