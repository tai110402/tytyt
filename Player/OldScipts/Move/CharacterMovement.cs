using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // References
    [Header("References")]
    [SerializeField] private Transform _camXZDirection;
    [SerializeField] private InputActionReference _movementControl;
    [SerializeField] private LayerMask _whatIsGround;
    private PlayerInput _playerInput;

    // Character component
    private Animator _characterAnimator;
    private CharacterController _characterController;

    // Set up attributes
    [Header("Attributes SetUp")]
    [SerializeField] private int _indexOfSwordAnimationLayer = 1;
    [SerializeField] private int _indexOfGunAnimationLayer = 2;
    [SerializeField] private float _gravity = -20f;   
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _runSpeed = 4f;
    [SerializeField] private float _minYVelocity = -10f;
    [SerializeField] private float _rotateSpeed = 720f;

    // Attributes
    private Vector3 _velocity;
    private Vector2 _movementDirection;
    private float _initialJumpVelocity;
    private float _previousYVelocity;
    private bool _isJumpPressed;
    private bool _isRunPressed;
    private bool _isGrounded;

    // Animator Hash
    private int _moveXHash;
    private int _moveZHash;

    // Getter ans Setter
    public Vector2 MoveDirection { get { return _movementDirection; } set { _movementDirection = value; } }

    // Awake
    private void Awake()
    {
        // Get component of character
        _characterController = GetComponent<CharacterController>();
        _characterAnimator = GetComponent<Animator>();

        // Handel Input
        _playerInput = new PlayerInput();

        _playerInput.CharacterControls.Jump.started += OnJumpStart;
        _playerInput.CharacterControls.Jump.canceled += OnJumpCancel;

        _playerInput.CharacterControls.Run.started += OnRunStart;
        _playerInput.CharacterControls.Run.canceled += OnRunCancel;

        // Hash Animator Parameters
        _moveXHash = Animator.StringToHash("MoveX");
        _moveZHash = Animator.StringToHash("MoveZ");
    }
    
    // Update is called once per frame
    void Update()
    {
        _isGrounded = CheckIsGrounded();
        HandleMoveDirection();
        HandleAnimation();
        HandleRotation();

        HandleOnGroundGravity();
        HandleJump();
        HandleMove();
        HandleRun();
        _characterController.Move(_velocity);
    }

    void HandleRotation()
    {
        if (_movementDirection != Vector2.zero)
        {
            Vector3 target = (_movementDirection.x * _camXZDirection.right + _movementDirection.y * _camXZDirection.forward);
            Quaternion toRotation = Quaternion.LookRotation(target, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotateSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion toRotation = Quaternion.LookRotation(_camXZDirection.forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotateSpeed * Time.deltaTime);
        }
    }

    // Handel animation
    private void HandleAnimation()
    {
        // Handle move, run and idle animation
        if (_movementDirection.x != 0f || _movementDirection.y != 0f)
        {
            if (_isRunPressed)
            {
                _characterAnimator.SetFloat("Movement", 1f);
            }
            else
            {
                _characterAnimator.SetFloat("Movement", 0.5f);
            }
        }
        else
        {
            _characterAnimator.SetFloat("Movement", 0f);
        }

        // Handle jump animation: handle in funtion "OnJumpStart"

        // Handel falling animation  
        if (!Physics.Raycast(transform.position, Vector3.down, _jumpHeight, _whatIsGround))
        {
            _characterAnimator.SetFloat("Movement", -1f);
        }
    }

    // Handle move direction
    private void HandleMoveDirection()
    {
        _movementDirection = _movementControl.action.ReadValue<Vector2>();
    }

    // Handle on ground gravity
    private void HandleOnGroundGravity()
    {
        if (_characterController.isGrounded && !_isJumpPressed)
        {
            _previousYVelocity = 0f;
        }
    }

    // Handel movement: jump, run, move
    private void HandleJump()
    {
        _previousYVelocity = _previousYVelocity + _gravity * Time.deltaTime;
        _velocity.y = _previousYVelocity * Time.deltaTime + 0.5f * _gravity * Time.deltaTime * Time.deltaTime;
        _velocity.y = Mathf.Clamp(_velocity.y, _minYVelocity, 1000f);
    }

    private void HandleMove()
    {
        Vector3 move = (_movementDirection.x * _camXZDirection.right + _movementDirection.y * _camXZDirection.forward) * Time.deltaTime * _walkSpeed;

        _velocity.x = move.x;
        _velocity.z = move.z;
    }

    void HandleRun()
    {
        if (_isRunPressed)
        {
            Vector3 run = (_movementDirection.x * _camXZDirection.right + _movementDirection.y * _camXZDirection.forward) * Time.deltaTime * _runSpeed;
            _velocity.x = run.x;
            _velocity.z = run.z;
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f, _whatIsGround);
    }

    // Enable and Disable
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        _movementControl.action.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        _movementControl.action.Disable();
    }

    void OnJumpStart(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();

        if (_isGrounded == true)
        {
            _initialJumpVelocity = Mathf.Sqrt(-2 * _gravity * _jumpHeight);
            _previousYVelocity = _initialJumpVelocity;

            // Handle jump animation
            _characterAnimator.CrossFade("Jump", 0.0f, 0);
            _characterAnimator.CrossFade("Jump", 0.0f, _indexOfSwordAnimationLayer);
            _characterAnimator.CrossFade("Jump", 0.0f, _indexOfGunAnimationLayer);
        }
    }

    void OnJumpCancel(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }

    void OnRunStart(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnRunCancel(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }
}
