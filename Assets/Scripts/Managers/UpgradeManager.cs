using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }

    [SerializeField] Button damageUpgradeButton;
    [SerializeField] Button speedUpgradeButton;

    public Action OnSkillUpgrade;

    private void Start()
    {
        damageUpgradeButton.onClick.AddListener(UpgradeSkillDamage);
        speedUpgradeButton.onClick.AddListener(UpgradeSkillSpeed);
    }

    void UpgradeSkillDamage() //function get called when the player click the damage upgrade button
    {
        if (SaveLoadManager.GetMoney() >= SkillManager.Instance.CurrentActiveSkill.DamageUpgradeCost) //if the player has enough money
        {
            SaveLoadManager.LooseMoney(SkillManager.Instance.CurrentActiveSkill.DamageUpgradeCost);

            SaveLoadManager.UpgradeSkillDamageByID(SkillManager.Instance.CurrentActiveSkill.ID);
            OnSkillUpgrade?.Invoke();

            HUD.Instance.UpdateSkillText();
        }
        else
        {
            ErrorManager.Instance.OnUpgradeBuyError?.Invoke();
            Debug.Log("need more money");
        }
    }

    void UpgradeSkillSpeed() //function to upgrade speed, similar to UpgradeDamage()
    {
        if (SaveLoadManager.GetMoney() >= SkillManager.Instance.CurrentActiveSkill.SpeedUpgradeCost)
        {
            SaveLoadManager.LooseMoney(SkillManager.Instance.CurrentActiveSkill.SpeedUpgradeCost);

            Debug.Log(SkillManager.Instance.CurrentActiveSkill.ID);
            SaveLoadManager.UpgradeSkillSpeedByID(SkillManager.Instance.CurrentActiveSkill.ID);
            OnSkillUpgrade?.Invoke();

            HUD.Instance.UpdateSkillText();
        }
        else
        {
            ErrorManager.Instance.OnUpgradeBuyError?.Invoke();
            Debug.Log("need more money");
        }
    }
}
