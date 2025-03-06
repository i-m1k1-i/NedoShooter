using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public event UnityAction<bool> MenuModeSetted;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private float _slopeForce = 5f;
    [SerializeField] private float _slopeRayLength = 1.5f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private FootStepsSounds _footStepsSounds;

    private PlayerInput _playerInput;
    private CharacterController _controller;
    private Character _character;
    private Vector3 _moveDirection;
    private const float _stepDistance = 2.5f;
    private float _coveredDistance;
    private float _moveMultiplier = 1;

    public float MoveSpeed => _moveSpeed;

    public event UnityAction RightClick;

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

        _playerInput = new PlayerInput();
        _playerInput.Character.Ability1.performed += cntx => _character.Ability1();
        _playerInput.Character.Ability2.performed += cntx => _character.Ability2();
        _playerInput.Character.Ability3.performed += cntx => _character.Ability3();
        _playerInput.Player.RightClick.performed += cntx => RightClick.Invoke();
    }

    private void Update()
    {
        if (_controller.isGrounded)
        {
            SetMoveDirection();

            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveMultiplier * Time.deltaTime * _moveDirection);
        _moveMultiplier = 1;
    }

    private void FixedUpdate()
    {
        Slope();
    }

    private void SetMoveDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        inputDirection = transform.TransformDirection(inputDirection).normalized;
        _moveDirection = inputDirection * _moveSpeed;

        if (horizontalInput == 0 && verticalInput == 0)
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

    private void SetMoveDirectionInAir()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput == 0 && verticalInput == 0)
        {
            print("No input in air");
            return;
        }

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        inputDirection = transform.TransformDirection(inputDirection).normalized;
        float moveDirectoinY = _moveDirection.y;
        _moveDirection = (inputDirection + _moveDirection).normalized * (_moveSpeed / 2);
        _moveDirection.y = moveDirectoinY;
    }

    private void Jump()
    {
        _moveDirection.y = _jumpForce;
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
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
