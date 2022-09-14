using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Aim))]
    public class PlayerInput : MonoBehaviour
    {
        private Movement _movement;
        private Aim _aim;
        private ShootBullets _shoot;
        private PlayerControls _controls;

        private Camera _mainCamera;

        private Vector2 _currentMousePosition;

        private void Awake()
        {
            _movement = GetComponent<Movement>();
            _aim = GetComponent<Aim>();
            _shoot = GetComponentInChildren<ShootBullets>();

            _mainCamera = Camera.main;
        }

        private void Update() => ConvertMouseToWorldPoint(_currentMousePosition);

        private void ConvertMouseToWorldPoint(Vector2 mousePosition)
        {
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0f;
            _aim.SetAimDirection(worldPosition);
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new PlayerControls();

                _controls.Gameplay.Move.performed += _movement.SetCurrentDirection;

                _controls.Gameplay.Aim.performed += ctx => _currentMousePosition = ctx.ReadValue<Vector2>();

                _controls.Gameplay.Shoot.performed += _shoot.SetShootBool;
                _controls.Gameplay.Shoot.canceled += _shoot.SetShootBool;

                _controls.Enable();
            }
        }

        private void OnDisable()
        {
            _controls.Gameplay.Move.performed -= _movement.SetCurrentDirection;

            _controls.Gameplay.Aim.performed -= ctx => _currentMousePosition = ctx.ReadValue<Vector2>();

            _controls.Gameplay.Shoot.performed -= _shoot.SetShootBool;
            _controls.Gameplay.Shoot.canceled -= _shoot.SetShootBool;

            _controls.Disable();
        }
    }
}