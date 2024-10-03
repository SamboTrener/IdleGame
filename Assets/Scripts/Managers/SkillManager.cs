using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [SerializeField] ActiveSkill[] skillsOnScene;

    public ActiveSkill CurrentActiveSkill { get; private set; }
    public ActiveSkill SelectedSkill { get; set; }

    public ActiveSkill[] GetSkillsOnScene() => skillsOnScene;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveLoadManager.AssignSkills(skillsOnScene);
        CurrentActiveSkill = SelectedSkill = skillsOnScene.FirstOrDefault(activeSkill => activeSkill.ID == SaveLoadManager.GetActivatedSkillId());
        UpgradeManager.Instance.OnSkillUpgrade += SetLastCurrentSkillChanges;
    }

    void SetLastCurrentSkillChanges()
    {
        CurrentActiveSkill.DamageLevel = SaveLoadManager.GetSkillByID(CurrentActiveSkill.ID).damageLevel;
        CurrentActiveSkill.SpeedLevel = SaveLoadManager.GetSkillByID(CurrentActiveSkill.ID).speedLevel;
    }

    public void UnlockSkillCheck() //check if there is a new skill can be unlocked
    {
        Debug.Log("Unlock skill check");
        bool newSkill = false;

        foreach (ActiveSkill activeSkill in skillsOnScene)
        {
            if (SaveLoadManager.GetLevel() == activeSkill.UnlockLevel) //unlock the skill if we beat the required level
            {
                activeSkill.Unlock();

                newSkill = true; //set to true if a new skill unlocked
            }
        }

        if (newSkill) //if a new skill gets unlocked, we start the coroutine and remind the player a new skill has been unlocked
            ActivatedSkill.Instance.StartNewSkillAlarm();
    }

    public bool TryChangeActivatedSkill()
    {
        if (SelectedSkill != null && CurrentActiveSkill != SelectedSkill)
        //we change the activated skill only when you selected a active skill and it's not the same as the one currently you are using
        {
            CurrentActiveSkill = SelectedSkill;

            if (CurrentActiveSkill.IsFirstPickup)
            {
                CurrentActiveSkill.IsFirstPickup = false;
                YGManager.SendMetrica("weaponChanged", CurrentActiveSkill.ID);
            }

            HUD.Instance.UpdateSkillText();


            SaveLoadManager.SetSelectedSkillId(CurrentActiveSkill.ID);

            return true;
        }
        else
        {
            return false;
        }

    }
}
