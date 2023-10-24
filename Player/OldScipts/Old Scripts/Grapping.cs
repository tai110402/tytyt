using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grapping : MonoBehaviour
{

    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _gunTip;
    [SerializeField] private LayerMask _whatIsGrappleable;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _maxGrappDistance = 30f;
    [SerializeField] private float _maxDistanceOnGrapplingMultiplier = 1.3f;
    private float _maxDistanceOnGrappling;
    private Vector3 _startGrapplePosition;

    [SerializeField] private float _grapDelayTime = 0.5f;


    [SerializeField] private InputActionReference _movementOnGrappleControl;
    private AnimationAndMovementController _animationAndMovementController;
    private Animator _animatorController;
    private PlayerInput _playerInput;
    private CharacterController _characterController;

    private Vector3 _grapplePoint;

    private float _grapplingCd;
    private float _grapplingCdTimer;

    private bool _isGrappPressed;
    private bool _isGrappling;
    private bool _grounded;
    private Vector3 _grappleDirection;


    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _animationAndMovementController = GetComponent<AnimationAndMovementController>();
        _animatorController = GetComponent<Animator>();

        _playerInput.CharacterControls.Grapple.started += OnGrappling;
        _playerInput.CharacterControls.Grapple.canceled += OnGrappling;
    }

    private void OnGrappling(InputAction.CallbackContext context)
    {
        _isGrappPressed = context.ReadValueAsButton();

        if(!_isGrappPressed) _animatorController.SetBool("isGrappling", false);

        if (_isGrappPressed && !_grounded)
        {
            

            RaycastHit hit;
            if (Physics.Raycast(_cam.position + _cam.forward, _cam.forward, out hit, _maxGrappDistance, _whatIsGrappleable))
            {
                if (!_grounded)
                {
                    _animationAndMovementController.enabled = false;
                    _animatorController.SetLayerWeight(2, 1f);
                    _animatorController.SetBool("isGrappling", true);
                    //_animatorController.CrossFade("Jump", 0.0f);

                    _grapplePoint = hit.point;
                    _grappleDirection = hit.point - transform.position;
                    _maxDistanceOnGrappling = _grappleDirection.magnitude * _maxDistanceOnGrapplingMultiplier;

                    _grappleDirection = _grappleDirection.normalized;
                    _startGrapplePosition = transform.position;
                }

                _isGrappPressed = false;
                Invoke(nameof(SetIsGrapplePressTrue), 0.4f);

            }
            else
            {
                _grapplePoint = _cam.position + _cam.forward * _maxGrappDistance;

                _lineRenderer.enabled = true;
                Invoke(nameof(TurnOfGrappleLine), 0.1f);
            }   
        }
        else
        {
            _lineRenderer.enabled = false;
            _animatorController.SetLayerWeight(2, 0.0f);
        }
    }

    private void SetIsGrapplePressTrue()
    {
        _lineRenderer.enabled = true;
        _isGrappPressed = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, 0.5f, _whatIsGround);
        //Debug.Log(_grounded);
        if (!_grounded)
        {
            CheckGrapple();
        }
        

        if (_isGrappPressed)
        {           
            RenderGrappleLine();

            if (_isGrappling)
            {
                //move character
                 Invoke(nameof(ExcuteGrapple), _grapDelayTime);              
            } else
            {
                //Invoke(nameof(TurnOfGrappleLine), 0.1f);
                
            }
        }
        else
        {
            
            _animationAndMovementController.enabled = true;
        }
    }

    private void TurnOfGrappleLine()
    {
        _lineRenderer.enabled = false;
    }

    private void ExcuteGrapple()
    {
        Vector2 _inputMoveOnGrapple = _movementOnGrappleControl.action.ReadValue<Vector2>();
        //movement character
        _animationAndMovementController.enabled = false;
        _characterController.Move(_grappleDirection * Time.deltaTime * 50f);
        _characterController.Move((transform.up * _inputMoveOnGrapple.y + transform.right * _inputMoveOnGrapple.x) * Time.deltaTime * 15f);
    }

    private void LateUpdate()
    {
        
    }

    private void CheckGrapple()
    {
        

        if (_grounded)
        {
            _lineRenderer.enabled = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(_cam.position, _cam.forward, out hit, _maxGrappDistance, _whatIsGrappleable))
        {         
            if (!_grounded)
            {
                _isGrappling = true;               
            }
            //_isGrappling = true;
        } else
        {
            if (!_isGrappPressed)
            {
                _isGrappling = false;
            }
        }

        //move character
        Vector3 overMaxDistanceOnGrappling = transform.position - _startGrapplePosition;

        if (overMaxDistanceOnGrappling.magnitude > _maxDistanceOnGrappling)
        {
            _isGrappling = false;
            _animatorController.SetLayerWeight(2, 0);
            _lineRenderer.enabled = false;
        }
    }

    private void RenderGrappleLine()
    {
        _lineRenderer.SetPosition(0, _gunTip.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }

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
}
