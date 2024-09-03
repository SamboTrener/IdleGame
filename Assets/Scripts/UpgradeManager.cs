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

            SkillManager.Instance.CurrentActiveSkill.DamageLevel += 1;      

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

            SkillManager.Instance.CurrentActiveSkill.SpeedLevel += 1;

            HUD.Instance.UpdateSkillText();
        }
        else
        {
            ErrorManager.Instance.OnUpgradeBuyError?.Invoke();
            Debug.Log("need more money");
        }
    }
}
