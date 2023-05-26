using DI;
using Services;
using UnityEngine;

namespace Hero
{
    public class HeroMove: MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;
        private Transform _transform;

        private void Awake()
        {
            _inputService = Container.Get<IInputService>();
            _transform = transform;
        }

        private void Start() => 
            _camera = Camera.main;

        public void LookAt(Vector2 screenPoint)
        {
            var worldPosition = _camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 20));
            var direction = (worldPosition - _transform.position).normalized;
            direction.y = 0;
            _transform.forward = direction;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.GetAxis().sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.GetAxis());
                movementVector.y = 0;
                movementVector.Normalize();
            }
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}