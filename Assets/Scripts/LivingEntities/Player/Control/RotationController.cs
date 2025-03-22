using UnityEngine;

namespace Assets.Scripts.LivingEntities.Player
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private InputReader _input;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _rotationSpeed;

        private float _rotationX;
        public float RotationX => _rotationX;

        private void Update()
        {
            HandleRotation(_input.MouseDelta);
        }

        private void HandleRotation(Vector2 mouseInput)
        {
            float mouseInputX = mouseInput.x;
            float mouseInputY = mouseInput.y;
            Debug.Log("Mouse x: " + mouseInputX);
            Debug.Log("Mouse y: " + mouseInputY);

            RotateAroundX(mouseInputY);
            RotateAroundY(mouseInputX);
        }

        private void RotateAroundX(float inputY)
        {
            _rotationX -= inputY * _rotationSpeed * Time.deltaTime; //_rotationSpeed * InputY * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            _camera.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
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
