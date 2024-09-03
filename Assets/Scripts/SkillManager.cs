using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    [SerializeField] ActiveSkill[] skillsOnScene;

    public ActiveSkill CurrentActiveSkill { get; set; }
    public ActiveSkill SelectedSkill { get; set; }

    public ActiveSkill[] GetSkillsOnScene() => skillsOnScene;

    private void Awake()
    {
        Instance = this;
        SaveLoadManager.AssignSkills(skillsOnScene);
        CurrentActiveSkill = SelectedSkill = skillsOnScene.FirstOrDefault(activeSkill => activeSkill.ID == SaveLoadManager.GetActivatedSkillId());
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
}
