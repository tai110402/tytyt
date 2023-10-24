using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSkill : MonoBehaviour
{
    // Skill 1
    public void FirstSwordSkill001Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["FirstSwordSkill001"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level-1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void FirstSwordSkill002Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["FirstSwordSkill002"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void FirstSwordSkill003Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["FirstSwordSkill003"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    // Skill 2
    public void SecondSwordSkill001Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["SecondSwordSkill001"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void SecondSwordSkill002Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["SecondSwordSkill002"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void SecondSwordSkill003Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["SecondSwordSkill003"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    // Skill 3
    public void ThirdSwordSkill001Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["ThirdSwordSkill001"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void ThirdSwordSkill002Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["ThirdSwordSkill002"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }

    public void ThirdSwordSkill003Upgrade()
    {
        Skill skill = RuntimeSkillData.SkillDictionary["ThirdSwordSkill003"];
        PlayerData playerData = RuntimePlayerData.PlayerData;
        if (skill.IsUnlocked == true && playerData.Exp >= skill.ExpToUpgradeArray[skill.Level - 1] && skill.Level < skill.MaxLevel)
        {
            if (skill.Level == skill.MaxLevel - 1)
            {
                if (playerData.BossKillPoint >= 1)
                {
                    playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                    playerData.BossKillPoint -= 1;
                    skill.Level += 1;
                }
            }
            else
            {
                playerData.Exp -= skill.ExpToUpgradeArray[skill.Level - 1];
                Debug.Log(skill.Level + "  " + skill.ExpToUpgradeArray[skill.Level - 1]);
                skill.Level += 1;
            }
        }
    }
}
