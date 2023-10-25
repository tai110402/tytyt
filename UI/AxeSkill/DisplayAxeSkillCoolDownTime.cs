using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DisplayAxeSkillCoolDownTime : MonoBehaviour
{
    [SerializeField] private PlayerAxe _playerAxe;
    [SerializeField] private Text _firstAxeSkillCoolDownText;
    [SerializeField] private Text _secondAxeSkillCoolDownText;
    [SerializeField] private Text _thirdAxeSkillCoolDownText;


    // Update is called once per frame
    void Update()
    {
        if (_playerAxe.FirstSkill != null)
        {
            float skillStartTime = _playerAxe.FirstSkillStartTime;
            float timeToReset = skillStartTime + _playerAxe.FirstSkill.TimeCoolDown[_playerAxe.FirstSkill.Level - 1] - Time.time;
            decimal decimalTimeToReset = Math.Round((decimal)timeToReset, 2);
            if (decimalTimeToReset > 0)
            {
                _firstAxeSkillCoolDownText.fontSize = 16;
                _firstAxeSkillCoolDownText.color = Color.red;
                _firstAxeSkillCoolDownText.text = decimalTimeToReset.ToString();
            }
            else
            {
                _firstAxeSkillCoolDownText.fontSize = 14;
                _firstAxeSkillCoolDownText.color = Color.black;
                _firstAxeSkillCoolDownText.text = "Q";
            }
        }

        if (_playerAxe.SecondSkill != null)
        {
            float skillStartTime = _playerAxe.SecondSkillStartTime;
            float timeToReset = skillStartTime + _playerAxe.SecondSkill.TimeCoolDown[_playerAxe.SecondSkill.Level - 1] - Time.time;
            decimal decimalTimeToReset = Math.Round((decimal)timeToReset, 2);
            if (decimalTimeToReset > 0)
            {
                _secondAxeSkillCoolDownText.fontSize = 16;
                _secondAxeSkillCoolDownText.color = Color.red;
                _secondAxeSkillCoolDownText.text = decimalTimeToReset.ToString();
            }
            else
            {
                _secondAxeSkillCoolDownText.fontSize = 14;
                _secondAxeSkillCoolDownText.color = Color.black;
                _secondAxeSkillCoolDownText.text = "E";
            }
        }

        if (_playerAxe.ThirdSkill != null)
        {
            float skillStartTime = _playerAxe.ThirdSkillStartTime;
            float timeToReset = skillStartTime + _playerAxe.ThirdSkill.TimeCoolDown[_playerAxe.ThirdSkill.Level - 1] - Time.time;
            decimal decimalTimeToReset = Math.Round((decimal)timeToReset, 2);
            if (decimalTimeToReset > 0)
            {
                _thirdAxeSkillCoolDownText.fontSize = 16;
                _thirdAxeSkillCoolDownText.color = Color.red;
                _thirdAxeSkillCoolDownText.text = decimalTimeToReset.ToString();
            }
            else
            {
                _thirdAxeSkillCoolDownText.fontSize = 14;
                _thirdAxeSkillCoolDownText.color = Color.black;
                _thirdAxeSkillCoolDownText.text = "R";
            }
        }
    }
}

