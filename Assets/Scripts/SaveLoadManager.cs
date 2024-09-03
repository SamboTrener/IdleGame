using System;
using System.Collections.Generic;
using System.Linq;

public static class SaveLoadManager
{
    static int level;
    static int money;
    static int activatedSkillId;
    static DateTime quitTime; //for daily bonus
    static List<Skill> skills;

    public static int GetMoney() => money;

    public static void EarnMoney(int value) 
    {
        money += value;
        HUD.Instance.OnMoneyVisualChanged?.Invoke(money);
    }
    public static void LooseMoney(int value)
    {
        money -= value;
        HUD.Instance.OnMoneyVisualChanged?.Invoke(money);
    } 

    public static Skill GetSkillByID(int id) => skills.FirstOrDefault(skill => id == skill.ID);

    public static int GetActivatedSkillId() => activatedSkillId;

    public static int GetLevel() => level;

    public static void AchiveNewLevel() => level++;

    public static void AssignSkills(ActiveSkill[] activeSkills)
    {
        skills = new List<Skill>();
        foreach(var activeSkill in activeSkills)
        {
            skills.Add(new Skill(activeSkill.ID, activeSkill.DamageLevel, activeSkill.SpeedLevel, activeSkill.Unlocked));
        }
    }

    public static void Reset() //function used when starting a new game
    {
        level = 1;

        money = 10000;

        activatedSkillId = 0;

        skills = new List<Skill>();

        quitTime = DateTime.Now;

        for (int i = 0; i < skills.Count; i++)
        {
            var skill = new Skill(i, 0, 0, false);

            skills[i] = skill;
        }
    }
}
