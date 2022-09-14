using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
 
namespace TopDownShooter
{
    public class ShootBullets : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ShootingPattern pattern;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Transform dynamic;

        [Header("Settings")]
        [SerializeField, Range(0f, 360f)]private float initialRotation;

        private ObjectPool<Bullet> _bulletPool;

        private bool _shootInput;
        private float _fireTime;
        private Vector3 _rotation;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(OnCreateBullet, OnGetBullet, OnReleaseBullet);

            if (spawnPosition == null)
                spawnPosition = transform;

            _fireTime = pattern.StartDelay;

            if (!pattern.IsAiming)
                _rotation.z = initialRotation;
        }

        #region Pooling Methods
        private Bullet OnCreateBullet()
        {
            var bullet = Instantiate(pattern.BulletPrefab, dynamic);
            bullet.SetPool(_bulletPool);
            return bullet;
        }

        private void OnGetBullet(Bullet bullet) => bullet.gameObject.SetActive(true);
        private void OnReleaseBullet(Bullet bullet) => bullet.gameObject.SetActive(false);
        #endregion

        private void FixedUpdate()
        {
            if (_fireTime > 0f)
            {
                _fireTime -= Time.deltaTime;
                return;
            }

            if (pattern.NeedInput)
            {
                if (_shootInput) Shoot();
                return;
            }
            
            Shoot();
        }

        private void Shoot()
        {
            UpdateRotation();

            var missRange = new Vector3(0f, 0f, pattern.MissAngleOpening * Random.Range(-pattern.MissRate, pattern.MissRate));
            var direction = _rotation + missRange + new Vector3(0f, 0f, pattern.AngleOffset);

            if (pattern.IsAiming)
            {
                var currentRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                direction += currentRotation;
            }

            if (pattern.SideCount == 1)
            {
                var bullet = _bulletPool.Get();
                bullet.Initialize(spawnPosition.position, direction, pattern.BulletSpeed);
            }
            else
            {
                var angleOffset = Vector3.zero;
                float angleDivision;

                if (pattern.AngleOpening != 360f)
                    angleDivision = pattern.AngleOpening / (pattern.SideCount - 1);
                else
                    angleDivision = pattern.AngleOpening / pattern.SideCount;

                for (int i = 0; i < pattern.SideCount; i++)
                {
                    angleOffset.z = angleDivision * i;

                    var bullet = _bulletPool.Get();
                    bullet.Initialize(spawnPosition.position, direction + angleOffset, pattern.BulletSpeed);
                }
            }

            _fireTime = pattern.FireRate;
        }

        private void UpdateRotation()
        {
            if (pattern.IsAiming)
                _rotation.z = -pattern.AngleOpening * 0.5f;
            else
                _rotation.z += pattern.SpinRate;
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