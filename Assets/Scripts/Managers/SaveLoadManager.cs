using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using YG;

public static class SaveLoadManager
{
    public static int GetMoney() => YandexGame.savesData.Money;

    public static void EarnMoney(int value)
    {
        YandexGame.savesData.Money += value;
        HUD.Instance.OnMoneyVisualChanged?.Invoke(YandexGame.savesData.Money);
        YandexGame.SaveProgress();
    }
    public static void LooseMoney(int value)
    {
        YandexGame.savesData.Money -= value;
        HUD.Instance.OnMoneyVisualChanged?.Invoke(YandexGame.savesData.Money);
        YandexGame.SaveProgress();
    }

    public static Skill GetSkillByID(int id) => YandexGame.savesData.Skills.FirstOrDefault(skill => id == skill.ID);

    public static int GetActivatedSkillId() => YandexGame.savesData.ActivatedSkillId;

    public static int GetLevel() => YandexGame.savesData.Level;

    public static void AchiveNewLevel()
    {
        YandexGame.savesData.Level++;
        YandexGame.SaveProgress();
    }

    public static void AssignSkills(ActiveSkill[] activeSkills)
    {
        YandexGame.savesData.Skills = new List<Skill>();
        foreach (var activeSkill in activeSkills)
        {
            YandexGame.savesData.Skills.Add(new Skill(activeSkill.ID, activeSkill.DamageLevel, 
                activeSkill.SpeedLevel, activeSkill.Unlocked, activeSkill.IsFirstPickup));
        }
        YandexGame.SaveProgress();
    }

    public static void SetSelectedSkillId(int ID)
    {
        YandexGame.savesData.ActivatedSkillId = ID;
        YandexGame.SaveProgress();
    }

    public static void UnlockSkillByID(int ID)
    {
        var skillToUnlock = YandexGame.savesData.Skills.FirstOrDefault(skill => skill.ID == ID);
        skillToUnlock.unlocked = true;
        YandexGame.SaveProgress();
    }

    public static void UpgradeSkillDamageByID(int ID)
    {
        var skillToUpgrade = YandexGame.savesData.Skills.FirstOrDefault(skill => skill.ID == ID);
        skillToUpgrade.damageLevel++;
        YandexGame.SaveProgress();
    }

    public static void UpgradeSkillSpeedByID(int ID)
    {
        var skillToUpgrade = YandexGame.savesData.Skills.FirstOrDefault(skill => skill.ID == ID);
        skillToUpgrade.speedLevel++;
        YandexGame.SaveProgress();
    }

    public static bool Is2XPurchased => YandexGame.savesData.IsX2Purchased;

    public static void PurchaseX2Mode() 
    {
        YandexGame.savesData.IsX2Purchased = true;
        YandexGame.SaveProgress();
    } 

    public static float GetGameSpeed() => YandexGame.savesData.GameSpeed;

    public static void SetGameSpeed(float value)
    {
        YandexGame.savesData.GameSpeed = value;
        YandexGame.SaveProgress();
    }

    public static void Reset() //function used when starting a new game
    {
        YandexGame.savesData.Level = 1;

        YandexGame.savesData.Money = 0;

        YandexGame.savesData.ActivatedSkillId = 0;

        YandexGame.savesData.IsX2Purchased = false;

        YandexGame.savesData.GameSpeed = 1f;

        YandexGame.savesData.Skills = new List<Skill>();

        for (int i = 0; i < YandexGame.savesData.Skills.Count; i++)
        {
            var skill = new Skill(i, 0, 0, false, true);

            YandexGame.savesData.Skills[i] = skill;
        }
        YandexGame.SaveProgress();
    }
}
