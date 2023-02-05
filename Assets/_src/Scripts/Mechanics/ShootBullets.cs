using UnityEngine;
// using UnityEngine.Pool;
using UnityEngine.InputSystem;
 
namespace TopDownShooter
{
    public class ShootBullets : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private IntEvent ammoEvent;
        [SerializeField] private ShootingPattern defaultPattern;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform dynamic;

        [Header("Settings")]
        [SerializeField, Range(0f, 360f)]private float initialRotation;

        private ShootingPattern _pattern;
        private int _ammoRemaining;

        private bool _shootInput;
        private float _fireTime;
        private Vector3 _rotation;

        private void Awake()
        {
            if (spawnPosition == null)
                spawnPosition = transform;
        }

        private void Start()
        {
            InitializePattern(defaultPattern);
            _fireTime = _pattern.StartDelay;

            if (dynamic == null)
                dynamic = LevelDependencies.Dynamic;
        }

        private void InitializePattern(ShootingPattern newPattern)
        {
            var oldPattern = _pattern;
            _pattern = newPattern;

            if (_pattern == oldPattern && _pattern != defaultPattern)
            {
                _ammoRemaining += newPattern.StartAmmo;
                
                if (_ammoRemaining > _pattern.MaxAmmo)
                    _ammoRemaining = _pattern.MaxAmmo;
            }
            else
            {
                if (_pattern.StartAmmo <= 0)
                    _ammoRemaining = -1;
                else
                    _ammoRemaining = _pattern.StartAmmo;
            }


            ammoEvent?.RaiseEvent(_ammoRemaining);

            if (!_pattern.IsAiming)
                _rotation.z = initialRotation;
        }

        private void FixedUpdate()
        {
            if (_fireTime > 0f)
            {
                _fireTime -= Time.deltaTime;
                return;
            }

            if (_pattern.NeedInput)
            {
                if (_shootInput) Shoot();
                return;
            }
            
            Shoot();
        }

        private void Shoot()
        {
            UpdateRotation();

            var missRange = new Vector3(0f, 0f, _pattern.MissAngleOpening * Random.Range(-_pattern.MissRate, _pattern.MissRate));
            var direction = _rotation + missRange + new Vector3(0f, 0f, _pattern.AngleOffset);

            if (_pattern.IsAiming)
            {
                var currentRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                direction += currentRotation;
            }

            if (_pattern.SideCount == 1)
            {
                var bullet = BulletManager.Instance.GetBullet(_pattern.BulletSO, _pattern.BulletPrefab);
                bullet.Setup(spawnPosition.position, direction, _pattern.BulletSpeed);
            }
            else
            {
                var angleOffset = Vector3.zero;
                float angleDivision;

                if (_pattern.AngleOpening != 360f)
                    angleDivision = _pattern.AngleOpening / (_pattern.SideCount - 1);
                else
                    angleDivision = _pattern.AngleOpening / _pattern.SideCount;

                for (int i = 0; i < _pattern.SideCount; i++)
                {
                    angleOffset.z = angleDivision * i;

                    var bullet = BulletManager.Instance.GetBullet(_pattern.BulletSO, _pattern.BulletPrefab);
                    bullet.Setup(spawnPosition.position, direction + angleOffset, _pattern.BulletSpeed);
                }
            }

            _fireTime = _pattern.FireRate;

            if (_ammoRemaining > 0)
            {
                _ammoRemaining--;

                ammoEvent?.RaiseEvent(_ammoRemaining);

                if (_ammoRemaining == 0)
                    ChangePattern(defaultPattern);
            }
        }

        private void UpdateRotation()
        {
            if (_pattern.IsAiming)
                _rotation.z = -_pattern.AngleOpening * 0.5f;
            else
                _rotation.z += _pattern.SpinRate;
        }

        public void ChangePattern(ShootingPattern newPattern)
        {
            InitializePattern(newPattern);
        }

        public void SetShootBool(bool value) => _shootInput = value;
        public void SetShootBool(InputAction.CallbackContext ctx)
        {
            switch (ctx.phase)
            {
                case InputActionPhase.Performed: SetShootBool(true); break;
                case InputActionPhase.Canceled: SetShootBool(false); break;
                default: break;
            }
        }
    }
}