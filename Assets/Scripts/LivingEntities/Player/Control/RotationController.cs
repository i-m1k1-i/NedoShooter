using UnityEngine;
using Zenject;

namespace Nedoshooter.Players
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField]private Transform _cameraTransform;

        private InputReader _input;

        private bool _rotationActive = true;
        private float _rotationX;
        public float RotationX => _rotationX;

        [Inject]
        private void Initialize(InputReader inputReader)
        {
            _input = inputReader;
        }

        public void SetRotationActive(bool active)
        {
            _rotationActive = active;
        }

        private void Update()
        {
            HandleRotation(_input.MouseDelta);
        }

        private void HandleRotation(Vector2 mouseInput)
        {
            if (_rotationActive == false) return;

            float mouseInputX = mouseInput.x;
            float mouseInputY = mouseInput.y;
            Debug.Log("Mouse x: " + mouseInputX);
            Debug.Log("Mouse y: " + mouseInputY);

            RotateAroundX(mouseInputY);
            RotateAroundY(mouseInputX);
        }

        private void RotateAroundX(float inputY)
        {
            _rotationX -= inputY * _rotationSpeed * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }

        private void RotateAroundY(float inputX)
        {
            Vector3 rotation = inputX * _rotationSpeed * Time.deltaTime * transform.up;
            transform.Rotate(rotation);
        }

        public void AddRotateX(float rotate)
        {
            _rotationX -= rotate;
            Debug.Log("recoil: " + rotate);
        }
    }
}
