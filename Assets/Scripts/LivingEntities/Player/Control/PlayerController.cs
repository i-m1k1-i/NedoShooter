using Assets.Scripts.Player;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 4f;
    [SerializeField] private float _slopeForce = 5f;
    [SerializeField] private float _slopeRayLength = 1.5f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private FootStepsSounds _footStepsSounds;

    private CharacterController _controller;
    private Vector3 _moveDirection;
    private const float _stepDistance = 2.5f;
    private float _coveredDistance;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _controller = GetComponent<CharacterController>();
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
        else 
        {
            SetMoveDirectionInAir();
        }
        _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Slope();
    }

    private void SetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

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
}
