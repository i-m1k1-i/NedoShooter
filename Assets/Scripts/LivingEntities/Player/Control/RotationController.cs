using UnityEngine;

namespace Assets.Scripts.LivingEntities.Player
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _rotationSpeed;

        private float _rotationX;
        public float RotationX => _rotationX;

        private void Update()
        {
            HandleRotation();
        }

        private void HandleRotation()
        {
            float mouseXInput = Input.GetAxis("Mouse X");
            float mouseYInput = Input.GetAxis("Mouse Y");

            // around x
            RotateAroundX(mouseYInput);

            // around y
            RotateAroundY(mouseXInput);
        }

        private void RotateAroundX(float YInput)
        {
            _rotationX -= YInput; //_rotationSpeed * YInput * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            _camera.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        }

        private void RotateAroundY(float XInput)
        {
            Vector3 rotation = _rotationSpeed * XInput * Time.deltaTime * transform.up;
            rotation.x = 0;
            rotation.z = 0;
            transform.Rotate(rotation);
        }

        public void AddRotateX(float rotate)
        {
            _rotationX -= rotate;
            Debug.Log("recoil: " + rotate);
        }
    }
}
