using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script controls the HUD object
public class HUD : MonoBehaviour
{
    //Singleton
    private static HUD instance;

    public static HUD Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("HUD").GetComponent<HUD>();

            return instance;
        }
    }

    [SerializeField] TextMeshProUGUI moneyText; //text to display the money the player has
    [SerializeField] TextMeshProUGUI levelText; //text to display the current monster level
    [SerializeField] TextMeshProUGUI countDownText; //text to show how much time left in this round
    [SerializeField] Color highlightColor; //color when a skill is selected
    [SerializeField] SkillTextInfo damageText;
    [SerializeField] SkillTextInfo speedText;

    [SerializeField] GameObject abilityWindow; //the active skill selecting window

    public Action<int> OnLevelVisualChanged;
    public Action<int> OnMoneyVisualChanged;
    public Action<string> OnCountDownTextChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    void Start() 
    {
        OnMoneyVisualChanged += ChangeMoneyVisual;
        OnLevelVisualChanged += ChangeLevelVisual;
        OnCountDownTextChanged += ChangeCountDownText;
        UpdateSkillText(); //call the function to update the text with corresponding damage and speed level to the actived skill
        ChangeMoneyVisual(SaveLoadManager.GetMoney());
    }

    public Color GetHighlightColor() => highlightColor;

    void ChangeMoneyVisual(int money)
    {
        moneyText.text = "Money: " + money.ToString(); //when change the value of money, update the text
    }
    void ChangeLevelVisual(int level)
    {
        levelText.text = "Enemy Level: " + level.ToString(); //update the Level text on bottom-right
    }
    void ChangeCountDownText(string timeBeforeTheEnd)
    {
        countDownText.text = timeBeforeTheEnd;
    }

    public void ShowAbilityWindow() //show the active skill selecting window
    {
        SkillManager.Instance.CurrentActiveSkill.SetColor(highlightColor); //highlight the skill currently using

        abilityWindow.SetActive(true);
    }

    public void HideAbilityWindow() //hide the window
    {
        SkillManager.Instance.SelectedSkill.SetColor(Color.white);

        SkillManager.Instance.SelectedSkill = SkillManager.Instance.CurrentActiveSkill;

        abilityWindow.SetActive(false);

        ActivatedSkill.Instance.StopNewSkillAlarm(); //stop the new skill alarm
    }

    public void UpdateSkillText() //function to update the text of Lv. and costs for both damage and speed
    {
        damageText.UpdateSkillText(SkillManager.Instance.CurrentActiveSkill.DamageLevel, SkillManager.Instance.CurrentActiveSkill.DamageUpgradeCost);

        speedText.UpdateSkillText(SkillManager.Instance.CurrentActiveSkill.SpeedLevel, SkillManager.Instance.CurrentActiveSkill.SpeedUpgradeCost);
    }
}
