using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public event UnityAction<bool> MenuModeSetted;

    [Header("Required components")]
    [SerializeField] private InputReader _input;
    [SerializeField] private FootStepsSounds _footStepsSounds;

    [Header("Value settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private float _slopeForce = 5f;
    [SerializeField] private float _slopeRayLength = 1.5f;
    [SerializeField] private float _gravity = 9.8f;

    private const float _stepDistance = 2.5f;
    private float _moveMultiplier = 1;
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _coveredDistance;
    private Vector2 _nonMoveableInput;
    private bool _isNonMoveableInput;
    public bool MoveInput = true;
    private bool _isJumping;

    public float MoveSpeed => _moveSpeed;
    public bool IsGrounded => _controller.isGrounded;


    public void SetMenuMode(bool menuMode)
    {
        if (menuMode)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        MenuModeSetted?.Invoke(menuMode);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _moveDirection = _moveDirection.normalized * moveSpeed;
    }

    public void MultiplyMoveDirection(float multiplier)
    {
        _moveMultiplier = multiplier;
    }

    private void Awake()
    {
        SetMenuMode(false);
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_controller.isGrounded && _isNonMoveableInput && MoveInput)
        {
            Vector3 direction = new Vector3(_nonMoveableInput.x, 0, _nonMoveableInput.y);
            _moveDirection = direction * _moveSpeed;

            _isNonMoveableInput = false;
        }

        SetMoveDirection(_input.MoveValue);

        _moveDirection.y -= _gravity * Time.deltaTime;

        Vector3 move = transform.TransformDirection(_moveDirection);
        _controller.Move(_moveMultiplier * Time.deltaTime * move);
    }

    private void FixedUpdate()
    {
        Slope();
    }

    private void SetMoveDirection(Vector2 input)
    {
        if (_controller.isGrounded == false || MoveInput == false)
        {
            Debug.Log("move off");
            _nonMoveableInput = input;
            _isNonMoveableInput = true;
            return;
        }

        Vector3 direction = new Vector3(input.x, 0, input.y);
        _moveDirection = direction * _moveSpeed;
        Debug.Log("Input direction: " + _moveDirection);

        if (_isJumping)
        {
            _moveDirection.y = _jumpForce;
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

    private void Jump()
    {
        // _moveDirection.y = _jumpForce;
        _isJumping = true;
    }

    private void HandleJump()
    {
        if (_controller.isGrounded)
        {
            Jump();
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
            _moveDirection.x += (1f - hit.normal.y) * hit.normal.x * _slopeForce;
            _moveDirection.z += (1f - hit.normal.y) * hit.normal.z * _slopeForce;
            _moveDirection.y -= _slopeForce;
        }
    }

    private void OnEnable()
    {
        _input.JumpEvent += HandleJump;
    }

    private void OnDisable()
    {
        _input.JumpEvent -= HandleJump;
    }
}
