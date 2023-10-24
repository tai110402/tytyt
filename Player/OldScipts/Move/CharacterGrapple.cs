using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterGrapple : MonoBehaviour
{
    // References
    [Header("References")]
    [SerializeField] private InputActionReference _movementOnGrappleControl;
    [SerializeField] private Camera _cam;
    [SerializeField] private Transform _gunTip;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _whatIsGrappleable;
    [SerializeField] private Transform _camXZDirection;

    // Character component
    private CharacterController _characterController;
    private CharacterMovement _characterMovement;
    private Animator _characterAnimator;

    // Setup attribute
    [Header("Attributes")]
    [SerializeField] private float _maxDistanceCheckGrapple = 200f;
    [SerializeField] private float _grappleSpeed = 20f;
    [SerializeField] private float _grappleMoveSpeed = 5f;
    [SerializeField] private float _maxDistanceOnGrappleMultiplier = 1.3f;
    [SerializeField] private float _fakeRenderTime = 0.1f;

    // Attribute
    private PlayerInput _playerInput;
    private float _maxDistanceOnGrapple;
    private float _currentDistanceGrappleMove;
    private bool _isGrapplePressed;
    private Vector3 _grapplePoint;
    private Vector3 _grapplePointStart;
    private Vector3 _grappleMoveDirection;
    private Vector3 _grappleDirection;
    private bool _isGrounded;
    private bool _canGrapple;

    // Getter and Setter
    public bool IsGrapplePressed { get { return _isGrapplePressed; } set { _isGrapplePressed = value; } }

    // Awake
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAnimator = GetComponent<Animator>();

        _playerInput.CharacterControls.Grapple.started += OnStartGrapple;
        _playerInput.CharacterControls.Grapple.canceled += OnCancelGrapple;
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = CheckIsGrounded();
        SetLinePosition();
        SetLineRender();
        ExcuteGrapple();
    }

    private void ExcuteGrapple()
    {
        if (_canGrapple)
        {
            _currentDistanceGrappleMove = CurrentDistanceOnGrappleMove();
            if (_isGrapplePressed && _currentDistanceGrappleMove < _maxDistanceOnGrapple)
            {
                _characterController.Move(_grappleDirection * _grappleSpeed * Time.deltaTime);

                Vector2 grappleMoveInput = _movementOnGrappleControl.action.ReadValue<Vector2>();
                _grappleMoveDirection.x = grappleMoveInput.x;
                _grappleMoveDirection.y = grappleMoveInput.y;
                _characterController.Move((transform.up * grappleMoveInput.y + transform.right * grappleMoveInput.x) * _grappleMoveSpeed * Time.deltaTime);
            }
            else if (_currentDistanceGrappleMove >= _maxDistanceOnGrapple)
            {
                _isGrapplePressed = false;
                _characterAnimator.SetBool("Grappling", false);
            } else
            {
                _characterAnimator.SetBool("Grappling", false);
            }
        } else if (!_canGrapple && _isGrapplePressed)
        {
            Invoke(nameof(FakeRenderLine), _fakeRenderTime);
        }   
    }

    // Use to fake render line when grapple in air
    private void FakeRenderLine()
    {
        _isGrapplePressed = false;
    }

    private float CurrentDistanceOnGrappleMove()
    {
        Vector3 distance = transform.position - _grapplePointStart;
        return distance.magnitude;
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.2f);
    }

    private void FindGrapplePoint()
    {
        RaycastHit hit;
        Vector3 rayOrigin = _cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(rayOrigin, _cam.transform.forward, out hit, _maxDistanceCheckGrapple, _whatIsGrappleable))
        {
            _grapplePoint = hit.point;
            _canGrapple = true;
        }
        else
        {
            _grapplePoint = rayOrigin + _cam.transform.forward * _maxDistanceCheckGrapple;
            _canGrapple = false;
        }
    }

    // Set line position
    private void SetLinePosition()
    {
        _lineRenderer.SetPosition(0, _gunTip.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }

    // Set line render
    private void SetLineRender()
    {
        if (_isGrapplePressed)
        {
            _lineRenderer.enabled = true;
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    // Enable and Disable
    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
        _movementOnGrappleControl.action.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
        _movementOnGrappleControl.action.Disable();
    }

    private void OnStartGrapple(InputAction.CallbackContext context)
    {
        if (!_isGrounded)
        {
            Quaternion toRotation = Quaternion.LookRotation(_camXZDirection.forward, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 2000f);

            _isGrapplePressed = context.ReadValueAsButton();
            FindGrapplePoint();
            _grappleDirection = _grapplePoint - _cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // calculate distance on grapple
            _maxDistanceOnGrapple = _grappleDirection.magnitude * _maxDistanceOnGrappleMultiplier;

            // normalize vector direction
            _grappleDirection = _grappleDirection.normalized;

            // calculate grapple point start
            _grapplePointStart = transform.position;

            // setup animation
            _characterAnimator.SetBool("Grappling", true);
        }
    }

    private void OnCancelGrapple(InputAction.CallbackContext context)
    {
        _isGrapplePressed = context.ReadValueAsButton();
    }
}
