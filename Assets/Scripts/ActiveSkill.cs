using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

//script for each active skill
public class ActiveSkill : MonoBehaviour
{
    [SerializeField] ActiveSkillSO activeSkillSO;

    [SerializeField] Image skillImage; //the image to show the skill icon
    [SerializeField] TextMeshProUGUI skillDescription;
    int damageLevel;
    int speedLevel;
    Button selectButton;

    public int AttackDamage { get; private set; }
    public int DamageUpgradeCost { get; private set; }
    public float AttackInterval { get; private set; }
    public int SpeedUpgradeCost { get; private set; }
    public bool Unlocked { get; private set; } = false;

    public Sprite SkillIcon { get; private set; }
    public int NumberOfTargets { get; private set; }
    public int ID { get; private set; }
    public int UnlockLevel { get;private set; }
    public bool IsFirstPickup { get; set; } = true;

    public int SpeedLevel
    {
        get
        {
            return speedLevel;
        }
        set
        {
            speedLevel = value;

            SpeedUpgradeCost = (int)(10 * Mathf.Pow(1.1f, speedLevel)); //formula to calculate the upgrade cost

            AttackInterval = activeSkillSO.BaseAttackInterval * Mathf.Pow(0.95f, speedLevel); //formula to calculate the cooldown period
        }
    }
    public int DamageLevel
    {
        get
        {
            return damageLevel;
        }
        set
        {
            damageLevel = value;

            DamageUpgradeCost = (int)(10 * Mathf.Pow(1.1f, damageLevel)); //formula to calculate the upgrade cost

            AttackDamage = (int)(activeSkillSO.BaseAttackDamage * Mathf.Pow(1.05f, damageLevel)); //formula to calculate the damage
        }
    }
    void Awake()
    {
        UnlockLevel = activeSkillSO.UnlockLevel;
        ID = activeSkillSO.ID;
        NumberOfTargets = activeSkillSO.NumberOfTargets;
        SkillIcon = activeSkillSO.SkillIcon;
        if(SaveLoadManager.GetSkillByID(activeSkillSO.ID) == null)
        {
            DamageLevel = 0;
            SpeedLevel = 0;
        }
        else
        {
            DamageLevel = SaveLoadManager.GetSkillByID(activeSkillSO.ID).damageLevel;
            SpeedLevel = SaveLoadManager.GetSkillByID(activeSkillSO.ID).speedLevel;
            if (SaveLoadManager.GetSkillByID(activeSkillSO.ID).unlocked) //according to the saved game file, unlock the skill if it has been unlocked
                Unlock();
        }


        if (activeSkillSO.Description == "") //if the description is omitted, set up the default description
        {
            if (activeSkillSO.NumberOfTargets == 1)
            {
                activeSkillSO.Description = "This skill can attack 1 enemy.";
            }
            else
            {
                activeSkillSO.Description = "This skill can attack up to " + activeSkillSO.NumberOfTargets.ToString() + " enemies.";
            }
        }

        if(YGManager.GetLanguageString() == "ru")
        {
            skillDescription.text = activeSkillSO.DescriptionRU;
        }
        else
        {
            skillDescription.text = activeSkillSO.Description;
        }

        skillImage.sprite = activeSkillSO.SkillIcon; //set up the image to show the skill icon

        selectButton = GetComponent<Button>();
        selectButton.onClick.AddListener(Select);
    }

    private void Start()
    {
        if (activeSkillSO.ID == 0) //unlock the skill if it's a starting skill
            Unlock();
    }

    public void Unlock() //unlock this active skill
    {
        if (!Unlocked)
        {
            SaveLoadManager.UnlockSkillByID(ID);
            Unlocked = true;

            Color color = skillImage.color; //this and following two lines will change the alpha value to 255

            color.a = 255;

            skillImage.color = color;
        }
    }

    public void Select() //function called when you select this skill in the avtive skill selecting window
    {
        if (Unlocked) //only unlocked skills can be selected
        {
            SkillManager.Instance.SelectedSkill.SetColor(Color.white); //change the color of the former selected skill to normal

            skillImage.color = HUD.Instance.GetHighlightColor(); //change the color of this skill when it's selected

            SkillManager.Instance.SelectedSkill = this; //update the selectedActiveSkill varialbe in PlayerStats script

        }
        else
        {
            ErrorManager.Instance.OnWeaponPickError?.Invoke(UnlockLevel);
        }
    }

    public void SetColor(Color color) //this function allows us to change the color of the icon image in other script
    {
        skillImage.color = color;
    }
}
