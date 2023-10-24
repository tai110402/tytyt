using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementControl;

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private Animator _animator;

    private int _isWalkingHash;
    private int _isRunningHash;
    private int _moveXHash;
    private int _moveZHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _currentRunMovement;
    private bool _isMovementPressed;
    private bool _isRunPressed;

    private bool _isMovementOnAir;

    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private float _walkMultiplier = 2.0f;


    [SerializeField] private float _runMultiplier = 4.0f;
    [SerializeField] private float _downDistanceToStartFallingAnimation = 3.0f;
    private int _zero = 0;
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _groundedGravity = -3f;

    private bool _isJumpPressed = false;
    private float _initialJumpVelocity;
    [SerializeField] private float _maxJumpHeight = 2.0f;
    [SerializeField] private float _jumpTime;
    private bool _isJumping = false;
    private int _isJumpingHash;
    private bool _isJumpAnimating = false;


    private Vector3 _velocity;
    private float _previousYVelocity;
    private float _gravityVelocity;
    private Vector2 _movementDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _moveXHash = Animator.StringToHash("MoveX");
        _moveZHash = Animator.StringToHash("MoveZ");

        _playerInput.CharacterControls.Jump.started += OnJumpStart;
        _playerInput.CharacterControls.Jump.canceled += OnJumpCancel;

        _playerInput.CharacterControls.Run.started += OnRunStart;
        _playerInput.CharacterControls.Run.canceled += OnRunCancel;
    }

    void OnJumpStart(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
        if (_characterController.isGrounded == true)
        {
            _initialJumpVelocity = Mathf.Sqrt(-2 * _gravity * _maxJumpHeight);
            _previousYVelocity = _initialJumpVelocity;

            _animator.CrossFade("Jump", 0.0f);
            //_jumpTime = 2 * -_initialJumpVelocity / _gravity;
            //Debug.Log(_jumpTime);

            //Invoke(nameof(SetIsJumpPress), _jumpTime);
        }
    }

    void SetIsJumpPress()
    {
        _isJumpPressed = !_isJumpPressed;
    }

    void OnJumpCancel(InputAction.CallbackContext context)
    {
        //_isJumpPressed = context.ReadValueAsButton();
    }

    void OnRunStart(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void OnRunCancel(InputAction.CallbackContext context)
    {
        _isRunPressed = context.ReadValueAsButton();
    }

    void HandleJump()
    {
        //if (_isJumpPressed)
        //{
        _previousYVelocity = _previousYVelocity + _gravity * Time.deltaTime;
        _velocity.y = _previousYVelocity * Time.deltaTime + 0.5f * _gravity * Time.deltaTime * Time.deltaTime;
        //}
    }

    void HandleGravity()
    {
        
        

    }

    void HandleMove()
    {
        Vector3 move = (_movementDirection.x * transform.right + _movementDirection.y * transform.forward) * Time.deltaTime * _walkMultiplier;
        _velocity.x = move.x;
        _velocity.z = move.z;
    }

    void TurnOnAnimationMove()
    {
        _animator.SetFloat(_moveXHash, _movementDirection.x);
        _animator.SetFloat(_moveZHash, _movementDirection.y);
    }

    void HandleRun()
    {
        Vector3 run = (_movementDirection.x * transform.right + _movementDirection.y * transform.forward) * Time.deltaTime * _runMultiplier;
        _velocity.x = run.x;
        _velocity.z = run.z;
    }

    void TurnOnAnimationRun()
    {
        _animator.SetFloat(_moveXHash, _movementDirection.x * 3f);
        _animator.SetFloat(_moveZHash, _movementDirection.y * 3f);
    }

    void TurnOffMoveAnimation()
    {
        _animator.SetFloat(_moveXHash, _movementDirection.x * 0f);
        _animator.SetFloat(_moveZHash, _movementDirection.y * 0f);
    }

    void TurnOnFallingAnimation()
    {
        _animator.SetFloat(_moveXHash, 6f);
        _animator.SetFloat(_moveZHash, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        

        _movementDirection = _movementControl.action.ReadValue<Vector2>();
        if (_movementDirection.x != 0 || _movementDirection.y != 0)
        {
            if (_isRunPressed == true)
            {
                HandleRun();
                if (_characterController.isGrounded) // CHUA SUWA, NHO SUA LAI PHAI CACH MAT DAT MOT KHOANG CAO HON
                                                     // KHONG LA LUC NHAY LEN, KHI XUONG GAN CHAM DAT BI CHUYNE SANG
                                                     // FALL LING
                {
                    TurnOnAnimationRun();
                } else
                {
                    TurnOnFallingAnimation();
                }
            } else
            {
                HandleMove();
                if (_characterController.isGrounded)
                {
                    TurnOnAnimationMove();
                } else
                {
                    TurnOnFallingAnimation();
                }
            }

        } else
        {
            _velocity.x = 0f;
            _velocity.z = 0f;
            TurnOffMoveAnimation();
            if (!_characterController.isGrounded)
            {
                TurnOnFallingAnimation();
            }
        }

        _characterController.Move(_velocity);
        HandleGravity();
    }

    private void LateUpdate()
    {
        if (_characterController.isGrounded)
        {
            // _groundedGravity cang lon thi co the di chuyen tren mat phang nghieng co do doc cang lon
            _previousYVelocity = _groundedGravity;
        }
        
        
    }

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
}
