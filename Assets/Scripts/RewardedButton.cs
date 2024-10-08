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
        YandexGame.RewardVideoEvent += TryGetReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= TryGetReward;
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
        button.onClick.AddListener(() => ShowRewarded(1));
    }

    void TryGetReward(int id)
    {
        if(id == 1)
        {
            SaveLoadManager.EarnMoney(CountReward());
        }
    }

    void ShowRewarded(int id)
    {
        YGManager.ShowRewarded(id);
    }

    public int CountReward()
    {
        var skill = SkillManager.Instance.CurrentActiveSkill;
        var minCost = Mathf.Min(skill.SpeedUpgradeCost, skill.DamageUpgradeCost);
        return minCost * rewardCoef;
    }
}
