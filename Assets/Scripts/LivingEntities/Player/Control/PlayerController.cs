using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Required components")]
        [SerializeField] private InputReader _input;
        [SerializeField] private FootStepsSounds _footStepsSounds;
        [SerializeField] private RotationController _rotationController;

        [Header("Value settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 4f;
        [SerializeField] private float _slopeForce = 5f;
        [SerializeField] private float _slopeRayLength = 1.5f;
        [SerializeField] private float _gravity = 9.8f;

        private const float _stepDistance = 2.5f;
        private float _moveMultiplier = 1;

        private CharacterController _controller;

        private Vector3 _moveLocal;

        private float _coveredDistance;

        private Vector2 _nonMoveableInput;
        private bool _isNonMoveableInput;
        private bool _handleMoveInput = true;

        private bool _isJumping;

        public float MoveSpeed => _moveSpeed;
        public bool IsGrounded => _controller.isGrounded;

        public event UnityAction<bool> MenuModeSetted;

        public void LockMouse(bool lockMouse)
        {
            if (lockMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _input.EnablePlayerInput();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                _input.DisablePlayerInput();
            }

            MenuModeSetted?.Invoke(lockMouse);
        }

        public void SetCanMove(bool can)
        {
            _handleMoveInput = can;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            _moveLocal = _moveLocal.normalized * moveSpeed;
        }

        public void MultiplyMoveDirection(float multiplier)
        {
            _moveMultiplier = multiplier;
        }

        private void Awake()
        {
            LockMouse(true);
            _controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _input.JumpEvent += HandleJump;
        }

        private void OnDisable()
        {
            _input.JumpEvent -= HandleJump;
        }

        private void Update()
        {
            if (_controller.isGrounded && _isNonMoveableInput && _handleMoveInput)
            {
                Vector3 direction = new Vector3(_nonMoveableInput.x, 0, _nonMoveableInput.y);
                _moveLocal = direction * _moveSpeed;

                _isNonMoveableInput = false;
            }

            SetMoveDirection(_input.MoveValue);

            _moveLocal.y -= _gravity * Time.deltaTime;

            Vector3 globalMove = transform.TransformDirection(_moveLocal);
            _controller.Move(_moveMultiplier * Time.deltaTime * globalMove);
        }

        private void FixedUpdate()
        {
            Slope();
        }

        private void SetMoveDirection(Vector2 input)
        {
            if (_controller.isGrounded == false || _handleMoveInput == false)
            {
                Debug.Log("move off");
                _nonMoveableInput = input;
                _isNonMoveableInput = true;
                return;
            }

            Vector3 direction = new Vector3(input.x, 0, input.y);
            _moveLocal = direction * _moveSpeed;
            Debug.Log("Input direction: " + _moveLocal);

            if (_isJumping)
            {
                _moveLocal.y = _jumpForce;
                _isJumping = false;
            }

            if (input.magnitude == 0)
            {
                _coveredDistance = 0;
            }
            else
            {
                _coveredDistance += _moveSpeed * Time.deltaTime;
                if (_coveredDistance >= _stepDistance)
                {
                    _coveredDistance -= _stepDistance;
                    _footStepsSounds.Play();
                }
            }
        }

        private void HandleJump()
        {
            if (_controller.isGrounded)
            {
                _isJumping = true;
            }
            else
            {
                Debug.Log("isGrounded false");
            }
        }

        private void Slope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _slopeRayLength) == false)
            {
                return;
            }

            if (Vector3.Angle(hit.normal, Vector3.up) > _controller.slopeLimit)
            {
                _moveLocal.x += (1f - hit.normal.y) * hit.normal.x * _slopeForce;
                _moveLocal.z += (1f - hit.normal.y) * hit.normal.z * _slopeForce;
                _moveLocal.y -= _slopeForce;
            }
        }
    }
}