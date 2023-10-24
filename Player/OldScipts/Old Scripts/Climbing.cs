using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Climbing : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementOnWallControl;
    private PlayerInput _playerInput;

    [SerializeField] private Transform _orientation;

    private CharacterController _characterController;
    private AnimationAndMovementController _animationAndMovementController;
    private Animator _animatorController;
    private Grapping _grapping;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private float _jumpForce = 70f;
    [SerializeField] private float _jumpCooldown = 0.5f;
    [SerializeField] private float _moveOnWallSpeed = 2.0f;


    private bool _climbing;
    private Vector2 _inputMoveOnWall;
    private Vector3 _moveOnWall;
    private bool _grounded;
    private bool _isJumpOnWallPressed;
    private float _timeJumpSet;


    [SerializeField] private float _detectionLength;
    [SerializeField] private float _sphereCastRadius;
    [SerializeField] private float _maxWallLookAngle;
    private float _wallLookAngle;


    private RaycastHit _frontWallHit;
    private bool _wallFront;

    private void WallCheck()
    {
        _wallFront = Physics.SphereCast(_orientation.position, _sphereCastRadius, _orientation.forward, out _frontWallHit, _detectionLength, _whatIsWall);
        _wallLookAngle = Vector3.Angle(_orientation.forward, -_frontWallHit.normal);
        //Debug.Log(_wallLookAngle);
        //Debug.Log(_wallFront);

        _grounded = Physics.Raycast(transform.position, Vector3.down, 0.5f, _whatIsGround);
        //Debug.Log("is Grounded" + _grounded);
        if (_wallFront && _wallLookAngle < _maxWallLookAngle && !_characterController.isGrounded)
        {
            _climbing = true;
            _animationAndMovementController.enabled = false;
            _grapping.enabled = false;
            _orientation.position = transform.position - transform.forward * 0.38f - transform.up * 0.09f;     
        } 
        else
        {
            _climbing = false;
            _animationAndMovementController.enabled = true;
            _grapping.enabled = true;
            _orientation.position = transform.position - transform.forward * 0.38f + transform.up * 0.2f;
        }
    }

    private void Awake()
    {
        _timeJumpSet = Time.time;
        _playerInput = new PlayerInput();
        _animationAndMovementController = GetComponent<AnimationAndMovementController>();
        _characterController = GetComponent<CharacterController>();
        _grapping = GetComponent<Grapping>();
        _animatorController = GetComponent<Animator>();


        _playerInput.CharacterControls.JumpOnWall.started += OnJumpOnWall;
        _playerInput.CharacterControls.JumpOnWall.canceled += OnJumpOnWall;
    }

    private void OnJumpOnWall(InputAction.CallbackContext context)
    {
        _isJumpOnWallPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();

        _inputMoveOnWall = _movementOnWallControl.action.ReadValue<Vector2>();
        _moveOnWall = transform.right * _inputMoveOnWall.x + transform.up * _inputMoveOnWall.y + transform.forward * 0.1f;
        //Debug.Log(_moveOnWall + "from climb");

        if (_climbing)
        {
            _animatorController.SetLayerWeight(1, 1f);
            ClimbingMovement();
            
        } else
        {
            _animatorController.SetLayerWeight(1, 0f);
        }

        JumpHandle();
    }

    private void ClimbingMovement()
    {
        _characterController.Move(_moveOnWall * Time.deltaTime * _moveOnWallSpeed);
    }

    private void JumpHandle()
    {
        if (_isJumpOnWallPressed && _climbing && Time.time - _timeJumpSet > _jumpCooldown)
        {
            _animationAndMovementController.enabled = false;
            _grapping.enabled = false;
            _characterController.Move((transform.up + transform.forward * 0.3f) * Time.deltaTime * _jumpForce);
            _timeJumpSet = Time.time;
        }
    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        _movementOnWallControl.action.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        _movementOnWallControl.action.Disable();
    }
}
