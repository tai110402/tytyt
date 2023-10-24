using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSword : MonoBehaviour
{
    // Character component
    private Animator _characterAnimator;
    private PlayerInput _playerInput;
    private CharacterMovementManager _characterMovementManager;
    private CharacterMovement _characterMovement;

    // SetUp Atributes
    [SerializeField] private int _indexOfSwordbAnimationLayer = 1;
    [SerializeField] private float _blockTime = 0.2f;
    [SerializeField] private GameObject[] _VFXNormalSkillObject;
    [SerializeField] private GameObject[] _VFXFirstSkillObject;
    [SerializeField] private GameObject[] _VFXSecondSkillObject;
    [SerializeField] private GameObject[] _VFXThirdSkillObject;

    // Attributes
    private bool _isFirstSwordSkillPressed;
    private float _timeNormalClicked;
    private int _numberOfClicks;
    private float _timeSetVFXFalse = 0.4f;

    // Awake
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterAnimator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterMovementManager = GetComponent<CharacterMovementManager>();

        _playerInput.CharacterControls.FirstSwordSkill.started += FirstSwordSkillStart;
        _playerInput.CharacterControls.FirstSwordSkill.started += FirstSwordSkillCancel;
        _playerInput.CharacterControls.SecondSwordSkill.started += SecondSwordSkillStart;
        _playerInput.CharacterControls.SecondSwordSkill.started += SecondSwordSkillCancel;
        _playerInput.CharacterControls.ThirdSwordSkill.started += ThirdSwordSkillStart;
        _playerInput.CharacterControls.ThirdSwordSkill.canceled += ThirdSwordSkillCancel;
        _playerInput.CharacterControls.NormalSwordSkill.started += NormalSwordSkillStart;
        _playerInput.CharacterControls.NormalSwordSkill.canceled += NormalSwordSkillCancel;
        _playerInput.CharacterControls.SwordBlock.started += SwordBlockStart;
        _playerInput.CharacterControls.SwordBlock.canceled += SwordBlockCancel;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        _playerInput.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharacterControls.Disable();
    }

    private void FirstSwordSkillStart(InputAction.CallbackContext context)
    {
        _characterAnimator.CrossFade("FirstSwordSkill", 0f, _indexOfSwordbAnimationLayer);   
    }

    private void FirstSwordSkillCancel(InputAction.CallbackContext context)
    {
        _VFXFirstSkillObject[0].SetActive(false);
    }

    private void SecondSwordSkillStart(InputAction.CallbackContext context)
    {
        _characterAnimator.CrossFade("SecondSwordSkill", 0f, _indexOfSwordbAnimationLayer);
    }

    private void SecondSwordSkillCancel(InputAction.CallbackContext context)
    {
        _VFXSecondSkillObject[0].SetActive(false);
        _VFXSecondSkillObject[1].SetActive(false);
        _VFXSecondSkillObject[2].SetActive(false);
    }

    private void ThirdSwordSkillStart(InputAction.CallbackContext context)
    {
        _characterAnimator.CrossFade("ThirdSwordSkill", 0f, _indexOfSwordbAnimationLayer);
    }

    private void ThirdSwordSkillCancel(InputAction.CallbackContext context)
    {
        _VFXThirdSkillObject[0].SetActive(false);
    }

    private void NormalSwordSkillStart(InputAction.CallbackContext context)
    {
        if (_timeNormalClicked + 0.2f > Time.time)
        {
            _numberOfClicks += 1;
        }
        else
        {
            _numberOfClicks = 1;
            _timeNormalClicked = Time.time;
        }



        if (_numberOfClicks == 1)
        {
            Invoke(nameof(WaitForCalculateNumberOfClicks), 0.2f);
        }

        if (_numberOfClicks == 2)
        {
            CancelInvoke(nameof(WaitForCalculateNumberOfClicks));
            _characterAnimator.CrossFade("NormalSwordSkill1", 0f, _indexOfSwordbAnimationLayer);
        }
    }

    private void NormalSwordSkillCancel(InputAction.CallbackContext context)
    {
        _VFXNormalSkillObject[0].SetActive(false);
        _VFXNormalSkillObject[1].SetActive(false);
    }

    private void SwordBlockStart(InputAction.CallbackContext context)
    {
        _characterAnimator.CrossFade("SwordBlock", 0f, _indexOfSwordbAnimationLayer);
        _characterMovementManager.enabled = false;
        _characterMovement.enabled = false;
        Invoke(nameof(EnableMovementComponent), _blockTime);
    }

    private void SwordBlockCancel(InputAction.CallbackContext context)
    {
        _characterMovementManager.enabled = true;
        _characterMovement.enabled = true;

    }

    // enable movement after blocking
    private void EnableMovementComponent()
    {
        _characterMovementManager.enabled = true;
        _characterMovement.enabled = true;
    }

    // Normal Skill Set
    void WaitForCalculateNumberOfClicks()
    {
        _characterAnimator.CrossFade("NormalSwordSkill", 0f, _indexOfSwordbAnimationLayer);

    }

    // use in event animation
    void SetVFXNormalSkillActiveTrue()
    {
        _VFXNormalSkillObject[0].SetActive(true);
        Invoke(nameof(SetVFXNormalSkillActiveFalse), _timeSetVFXFalse);
    }
    void SetVFXNormalSkillActiveFalse()
    {
        _VFXNormalSkillObject[0].SetActive(false);
    }

    void SetVFXNormalSkill1ActiveTrue()
    {
        _VFXNormalSkillObject[1].SetActive(true);
        Invoke(nameof(SetVFXNormalSkill1ActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXNormalSkill1ActiveFalse()
    {
        _VFXNormalSkillObject[1].SetActive(false);
    }

    void SetVFXFirstSkillActiveTrue()
    {
        _VFXFirstSkillObject[0].SetActive(true);
        Invoke(nameof(SetVFXFirstSkillActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXFirstSkillActiveFalse()
    {
        _VFXFirstSkillObject[0].SetActive(false);
    }

    void SetVFXSecondSkillActiveTrue()
    {
        _VFXSecondSkillObject[0].SetActive(true);
        Invoke(nameof(SetVFXSecondSkillActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXSecondSkillActiveFalse()
    {
        _VFXSecondSkillObject[0].SetActive(false);
    }

    void SetVFXSecondSkill1ActiveTrue()
    {
        _VFXSecondSkillObject[1].SetActive(true);
        Invoke(nameof(SetVFXSecondSkill1ActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXSecondSkill1ActiveFalse()
    {
        _VFXSecondSkillObject[1].SetActive(false);
    }

    void SetVFXSecondSkill2ActiveTrue()
    {
        _VFXSecondSkillObject[2].SetActive(true);
        Invoke(nameof(SetVFXSecondSkill2ActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXSecondSkill2ActiveFalse()
    {
        _VFXSecondSkillObject[2].SetActive(false); 
    }

    void SetVFXThirdSkillActiveTrue()
    {
        _VFXThirdSkillObject[0].SetActive(true);
        Invoke(nameof(SetVFXThirdSkillActiveFalse), _timeSetVFXFalse);
    }

    void SetVFXThirdSkillActiveFalse()
    {
        _VFXThirdSkillObject[0].SetActive(false);
    }
}
