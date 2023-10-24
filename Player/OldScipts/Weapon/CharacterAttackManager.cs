using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackManager : MonoBehaviour
{
    [SerializeField] private int _indexOfSwordAnimationLayer = 1;
    [SerializeField] private int _indexOfGunAnimationLayer = 2;

    [SerializeField] private Gun _gun;

    private CharacterSword _characterSword;
    private CharacterGun _characterGun;
    private CharacterSpellBook _characterSpellBook;
    private CharacterWand _characterWand;

    private Animator _characterAnimator;

    private bool _isUnEquip = true;
    private bool _isSwordEquip;
    private bool _isGunEquip;

    // Awake Fucntion
    private void Awake()
    {
        // Get character component
        _characterSword = GetComponent<CharacterSword>();
        _characterGun = GetComponent<CharacterGun>();
        _characterSpellBook = GetComponent<CharacterSpellBook>();
        _characterWand = GetComponent<CharacterWand>();

        _characterAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isSwordEquip = true;

            if (_isUnEquip)
            {
                SetUpForSword();
                _isUnEquip = false;
            }

            if (_isGunEquip)
            {
                _characterAnimator.CrossFade("UnArmGun", 0f, _indexOfGunAnimationLayer);
                Invoke(nameof(SetUpForSword), 1f);
                _isGunEquip = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _isGunEquip = true;

            if (_isUnEquip)
            {
                SetUpForGun();
                _isUnEquip = false;
            }

            if (_isSwordEquip)
            {
                _characterAnimator.CrossFade("UnArmSword", 0f, _indexOfSwordAnimationLayer);
                Invoke(nameof(SetUpForGun), 0.3f);   
                _isSwordEquip = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isUnEquip = true;
            if (_isSwordEquip)
            {
                _isSwordEquip = false;
                _characterAnimator.CrossFade("UnArmSword", 0f, _indexOfSwordAnimationLayer);
                StartCoroutine(SetSwordLayerWeight(_indexOfSwordAnimationLayer, 0f));
            }

            if (_isGunEquip)
            {
                _isGunEquip = false;
                _characterAnimator.CrossFade("UnArmGun", 0f, _indexOfGunAnimationLayer);
                StartCoroutine(SetSwordLayerWeight(_indexOfGunAnimationLayer, 0f));
                _gun.enabled = false;
            }
        }
    }

    IEnumerator SetSwordLayerWeight(int indexLayer, float weight)
    {
        yield return new WaitForSeconds(0.7f);
        _characterAnimator.SetLayerWeight(indexLayer, weight);
    }

    private void SetUpForGun()
    {
        _characterAnimator.CrossFade("ArmGun", 0f, _indexOfGunAnimationLayer);

        _gun.enabled = true;
        _characterGun.enabled = true;
        _characterSpellBook.enabled = false;
        _characterSword.enabled = false;
        _characterWand.enabled = false;

        _characterAnimator.SetLayerWeight(_indexOfSwordAnimationLayer, 0f);
        _characterAnimator.SetLayerWeight(_indexOfGunAnimationLayer, 1f);
    }

    private void SetUpForSword()
    {
        _characterAnimator.CrossFade("ArmSword", 0f, _indexOfSwordAnimationLayer);

        _characterGun.enabled = false;
        _characterSpellBook.enabled = false;
        _characterSword.enabled = true;
        _characterWand.enabled = false;
        _gun.enabled = false;

        _characterAnimator.SetLayerWeight(_indexOfSwordAnimationLayer, 1f);
        _characterAnimator.SetLayerWeight(_indexOfGunAnimationLayer, 0f);
    }

    private void SetUpForSpellBook()
    {
        _characterGun.enabled = false;
        _characterSpellBook.enabled = true;
        _characterSword.enabled = false;
        _characterWand.enabled = false;
    }

    private void SetUpForWand()
    {
        _characterGun.enabled = false;
        _characterSpellBook.enabled = false;
        _characterSword.enabled = false;
        _characterWand.enabled = true;
    }
}
