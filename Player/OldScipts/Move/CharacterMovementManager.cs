using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementManager : MonoBehaviour
{
    //
    [SerializeField] private int _indexOfClimbAnimationLayer = 1;
    [SerializeField] private int _indexOfGrappleAnimationLayer = 2;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private CheckColliderForClimb _checkColliderForClimb;

    // Character component
    private CharacterController _characterController;
    private Animator _characterAnimator;
    private CharacterMovement _characterMovement;
    private CharacterClimb _characterClimb;
    private CharacterGrapple _characterGrapple;

    // Attribute
    private bool _isGrounded;

    // Input
    private bool _isGrapplePressed;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _characterAnimator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterClimb = GetComponent<CharacterClimb>();
        _characterGrapple = GetComponent<CharacterGrapple>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = CheckIsGrounded();
        _isGrapplePressed = _characterGrapple.IsGrapplePressed;
        Climb();
        Grapple();
        Movement();
    }

    private void Climb()
    {
        if (!_isGrounded && _checkColliderForClimb.CanClimb)
        {
            //Set up for climb
            SetUpForClimb();
        }    
    }

    private void Grapple()
    {
        if (!_isGrounded && _isGrapplePressed)
        {
            //Set up for grapple
            SetUpForGrapple();
        }
    }

    private void Movement()
    {
        if (!_isGrapplePressed && !_checkColliderForClimb.CanClimb)
        {
            //Set up for movement
            SetUpForMovement();
        }

        if (_isGrounded && _checkColliderForClimb.CanClimb)
        {
            //Set up for movement
            SetUpForMovement();
        }
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f, _whatIsGround);
    }

    private void SetUpForClimb()
    {
        _characterMovement.enabled = false;
        _characterClimb.enabled = true;
        _characterAnimator.SetLayerWeight(_indexOfClimbAnimationLayer, 1f);
        _characterAnimator.SetLayerWeight(_indexOfGrappleAnimationLayer, 0f);
    }

    private void SetUpForMovement()
    {       
        _characterClimb.enabled = false;
        _characterMovement.enabled = true;
        _characterAnimator.SetLayerWeight(_indexOfClimbAnimationLayer, 0f);
        _characterAnimator.SetLayerWeight(_indexOfGrappleAnimationLayer, 0f);
    }

    private void SetUpForGrapple()
    {
        _characterMovement.enabled = false;
        _characterClimb.enabled = false;
        _characterGrapple.enabled = true;
        _characterAnimator.SetLayerWeight(_indexOfGrappleAnimationLayer, 1f);
    }
}
