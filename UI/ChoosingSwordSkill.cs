using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingSwordSkill : MonoBehaviour
{
    [SerializeField] private Image _firstSwordSkill001BackGround;
    [SerializeField] private Image _firstSwordSkill002BackGround;
    [SerializeField] private Image _firstSwordSkill003BackGround;

    [SerializeField] private Image _secondSwordSkill001BackGround;
    [SerializeField] private Image _secondSwordSkill002BackGround;
    [SerializeField] private Image _secondSwordSkill003BackGround;

    [SerializeField] private Image _thirdSwordSkill001BackGround;
    [SerializeField] private Image _thirdSwordSkill002BackGround;
    [SerializeField] private Image _thirdSwordSkill003BackGround;

    private void Update()
    {
        // Skill 1
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUsing == false)
            {
                _firstSwordSkill001BackGround.color = Color.white;
            } else
            {
                _firstSwordSkill001BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUsing == false)
            {
                _firstSwordSkill002BackGround.color = Color.white;
            } else
            {
                _firstSwordSkill002BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUsing == false)
            {
                _firstSwordSkill003BackGround.color = Color.white;
            }
            else
            {
                _firstSwordSkill003BackGround.color = new Color32(182, 242, 255, 255);
            }
        }

        // Skill 2
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUsing == false)
            {
                _secondSwordSkill001BackGround.color = Color.white;
            }
            else
            {
                _secondSwordSkill001BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUsing == false)
            {
                _secondSwordSkill002BackGround.color = Color.white;
            }
            else
            {
                _secondSwordSkill002BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUsing == false)
            {
                _secondSwordSkill003BackGround.color = Color.white;
            }
            else
            {
                _secondSwordSkill003BackGround.color = new Color32(182, 242, 255, 255);
            }
        }

        // Skill 3
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUsing == false)
            {
                _thirdSwordSkill001BackGround.color = Color.white;
            }
            else
            {
                _thirdSwordSkill001BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUsing == false)
            {
                _thirdSwordSkill002BackGround.color = Color.white;
            }
            else
            {
                _thirdSwordSkill002BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUnlocked == true)
        {
            if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUsing == false)
            {
                _thirdSwordSkill003BackGround.color = Color.white;
            }
            else
            {
                _thirdSwordSkill003BackGround.color = new Color32(182, 242, 255, 255);
            }
        }
    }


    // Skill 1
    public void UsingFirstSwordSkill001()
    {
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingFirstSwordSkill002()
    {
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingFirstSwordSkill003()
    {
        if (RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["FirstSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["FirstSwordSkill003"].IsUsing = true;
        }
    }

    // Skill 2
    public void UsingSecondSwordSkill001()
    {
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingSecondSwordSkill002()
    {
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingSecondSwordSkill003()
    {
        if (RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["SecondSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["SecondSwordSkill003"].IsUsing = true;
        }
    }

    // Skill 3
    public void UsingThirdSwordSkill001()
    {
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingThirdSwordSkill002()
    {
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUsing = true;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUsing = false;
        }
    }

    public void UsingThirdSwordSkill003()
    {
        if (RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUnlocked == true)
        {
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"].IsUsing = false;
            RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"].IsUsing = true;
        }
    }
}
