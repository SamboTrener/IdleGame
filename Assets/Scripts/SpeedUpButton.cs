using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SpeedUpButton : MonoBehaviour
{
    Button button;
    [SerializeField] TextMeshProUGUI xText;
    [SerializeField] GameObject adIcon;
    [SerializeField] GameObject textBeforeRewarded;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += TryGetReward;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= TryGetReward;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SpeedUpGame);
        if (SaveLoadManager.Is2XPurchased)
        {
            PreparePurchasedButton();
        }
    }

    void PreparePurchasedButton()
    {
        textBeforeRewarded.SetActive(false);
        adIcon.SetActive(false);
        xText.text = "X1";
    }

    void TryGetReward(int id)
    {
        if(id == 2)
        {
            SaveLoadManager.PurchaseX2Mode();
            PreparePurchasedButton();
        }
    }

    void SpeedUpGame()
    {
        if (!SaveLoadManager.Is2XPurchased)
        {
            ShowRewarded(2);
            return;
        }
        Time.timeScale = 2f;
        xText.text = "X2";
        button.onClick.AddListener(SlowDownGame);
    }

    void SlowDownGame()
    {
        Time.timeScale = 1f;
        xText.text = "X1";
        button.onClick.AddListener(SpeedUpGame);
    }

    void ShowRewarded(int id)
    {
        YGManager.ShowRewarded(id);
    }
}
