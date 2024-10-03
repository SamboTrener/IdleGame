using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardedButton : MonoBehaviour
{
    public static RewardedButton Instance { get; private set; }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += GetReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= GetReward;
    }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] int rewardCoef;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowRewarded);
    }

    void GetReward(int id)
    {
        SaveLoadManager.EarnMoney(CountReward());
    }

    void ShowRewarded()
    {
        YGManager.ShowRewarded();
    }

    public int CountReward()
    {
        var skill = SkillManager.Instance.CurrentActiveSkill;
        var minCost = Mathf.Min(skill.SpeedUpgradeCost, skill.DamageUpgradeCost);
        return minCost * rewardCoef;
    }
}
