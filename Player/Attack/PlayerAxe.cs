using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxe : MonoBehaviour, IPlayerWeapon
{
    [SerializeField] private int _indexOfAxeAnimatorLayer = 2;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimatorLayer()
    {
        Debug.Log("Axe");
        for (int i = _indexOfAxeAnimatorLayer; i < _playerAnimator.layerCount; i++)
        {
            _playerAnimator.SetLayerWeight(i, 0f);
        }
        _playerAnimator.SetLayerWeight(_indexOfAxeAnimatorLayer, 1f);
    }

    public float Attack(Skill skill, float skillStartTime)
    {
        if (skill == null) return skillStartTime;

        if (Time.time - skillStartTime > skill.TimeCoolDown[skill.Level-1])
        {
            skillStartTime = Time.time;
            _playerAnimator.CrossFade(skill.AnimationName, 0f);
        }
        return skillStartTime;
    }
}
