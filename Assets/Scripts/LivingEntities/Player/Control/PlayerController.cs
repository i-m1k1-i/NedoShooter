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
    private Character _character;
    private Vector3 _moveDirection;
    private float _coveredDistance;
    private bool _isJumping;
    private Vector2 _airInput;
    private bool _isAirInput;

    public float MoveSpeed => _moveSpeed;


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
    }

    public bool TryMultiplyMoveDirection(float multiplier)
    {
        if (_controller.isGrounded)
        {
            _moveMultiplier = multiplier;
            return true;
        }

        return false;
    }

    private void Awake()
    {
        SetMenuMode(false);
        _controller = GetComponent<CharacterController>();
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (_controller.isGrounded && _isAirInput)
        {
            Vector3 direction = new Vector3(_airInput.x, 0, _airInput.y);
            _moveDirection = direction * _moveSpeed;

            _isAirInput = false;
        }

        if (_isJumping)
        {
            _moveDirection.y = _jumpForce;
            _isJumping = false;
        }
        _moveDirection.y -= _gravity * Time.deltaTime;

        Vector3 move = transform.TransformDirection(_moveDirection);
        _controller.Move(_moveMultiplier * Time.deltaTime * move);
        _moveMultiplier = 1;
    }

    private void FixedUpdate()
    {
        Slope();
    }

    private void SetMoveDirection(Vector2 input)
    {
        if (_controller.isGrounded == false)
        {
            _airInput = input;
            _isAirInput = true;
            return;
        }

        Vector3 direction = new Vector3(input.x, 0, input.y);
        _moveDirection = direction * _moveSpeed;
        Debug.Log("Input direction: " + _moveDirection);

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
        _moveDirection.y = _jumpForce;
        //_isJumping = true;
    }

    private void HandleJump()
    {
        if (_controller.isGrounded)
        {
            Jump();
        }
        else
        {
            Debug.Log("OnGround false");
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
        _input.Ability1Event += _character.Ability1;
        _input.Ability2Event += _character.Ability2;
        _input.Ability3Event += _character.Ability3;
        _input.MoveEvent += SetMoveDirection;
        _input.JumpEvent += HandleJump;
    }

    private void OnDisable()
    {
        _input.Ability1Event -= _character.Ability1;
        _input.Ability2Event -= _character.Ability2;
        _input.Ability3Event -= _character.Ability3;
        _input.MoveEvent -= SetMoveDirection;
        _input.JumpEvent -= HandleJump;
    }
}
