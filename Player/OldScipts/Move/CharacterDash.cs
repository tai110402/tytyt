using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : MonoBehaviour
{
    [SerializeField] private int _indexOfSwordAnimationLayer = 1;
    [SerializeField] private int _indexOfGunAnimationLayer = 2;
    [SerializeField] private Transform _camXZDirection;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private float _dashStart;

    private CharacterMovement _characterMovement;
    private CharacterController _characterController;
    private Animator _characterAnimator;
    [SerializeField] private CharacterTrail[] _characterTrail;

    // Start is called before the first frame update
    void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterController = GetComponent<CharacterController>();
        _characterAnimator = GetComponent<Animator>();

        _dashStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && (_characterMovement.MoveDirection.x != 0f || _characterMovement.MoveDirection.y != 0f))
        {
            
            if (Time.time - _dashStart > _dashCooldown)
            {
                _dashStart = Time.time;

                _characterAnimator.CrossFade("Dash", 0f, 0);
                _characterAnimator.CrossFade("Dash", 0f, _indexOfSwordAnimationLayer);
                _characterAnimator.CrossFade("Dash", 0f, _indexOfGunAnimationLayer);

                StartCoroutine(Dash());
                for (int i=0; i<_characterTrail.Length; i++)
                {
                    _characterTrail[i].StartCharacterTrail();
                }
            }
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        while(Time.time < startTime + _dashTime)
        {
            Vector2 moveDirection = _characterMovement.MoveDirection;
            Vector3 dashDirection = (moveDirection.x * _camXZDirection.right + moveDirection.y * _camXZDirection.forward);
            _characterController.Move(dashDirection * _dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
