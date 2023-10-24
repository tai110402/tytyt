using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterClimb : MonoBehaviour
{
    // References
    [Header("References")]
    [SerializeField] private InputActionReference _movementOnWallControl;

    private PlayerInput _playerInput;

    // Character component
    private CharacterController _characterController;
    private Animator _characterAnimator;

    // Setup attributes
    [Header("Attributes")]
    [SerializeField] private float _climbSpeed = 2f;
    [SerializeField] private float _jumpCooldown = 1f;
    [SerializeField] private float _jumpForce = 230f;

    // Attributes
    private bool _isJumpOnWallPressed;
    private Vector3 _climbDirection;
    private float _timeJump;

    // Animator Hash
    private int _climbXHash;
    private int _climbYHash;

    // Awake
    private void Awake()
    {
        // Get component of character
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _characterAnimator = GetComponent<Animator>();

        // Handel Input
        _playerInput.CharacterControls.JumpOnWall.started += OnJumpOnWall;
        _playerInput.CharacterControls.JumpOnWall.canceled += OnJumpOnWall;

        // Hash Animator Parameters
        _climbXHash = Animator.StringToHash("ClimbX");
        _climbYHash = Animator.StringToHash("ClimbY");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandelClimbDirection();
        HandleClimb();
        HandleJump();
        HandleClimbAnimation();
    }

    // Handle animation
    private void HandleClimbAnimation()
    {
        if (_climbDirection.x != 0f || _climbDirection.y !=0f)
        {
            _characterAnimator.SetFloat(_climbXHash, _climbDirection.x);
            _characterAnimator.SetFloat(_climbYHash, _climbDirection.y);
        } else
        {
            _characterAnimator.SetFloat(_climbXHash, 0f);
            _characterAnimator.SetFloat(_climbYHash, 0f);
        }
    }

    // Move on wall
    private void HandleClimb()
    {
        _characterController.Move((transform.up * _climbDirection.y + transform.right * _climbDirection.x) * Time.deltaTime * _climbSpeed);
        
    }

    // Handle climb direction
    private void HandelClimbDirection()
    {
        Vector2 inputClimb = _movementOnWallControl.action.ReadValue<Vector2>();
        _climbDirection.x = inputClimb.x;
        _climbDirection.y = inputClimb.y;
    }

    // Handle jump on wall
    private void HandleJump()
    {
        if (_isJumpOnWallPressed && Time.time - _timeJump > _jumpCooldown)
        {
            _characterController.Move((transform.up + transform.forward * 0.3f) * Time.deltaTime * _jumpForce);
            _timeJump = Time.time;
        }
    }

    // Enable and Disable
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

    private void OnJumpOnWall(InputAction.CallbackContext context)
    {
        _isJumpOnWallPressed = context.ReadValueAsButton();
    }
}
