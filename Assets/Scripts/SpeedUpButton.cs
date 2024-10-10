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
        if (SaveLoadManager.Is2XPurchased)
        {
            PreparePurchasedButton();
        }
        else
        {
            button.onClick.AddListener(PurchaseX2Mode);
        }
    }

    void PurchaseX2Mode()
    {
        YGManager.ShowRewarded(2);
    }

    void PreparePurchasedButton()
    {
        button.onClick.RemoveAllListeners();
        textBeforeRewarded.SetActive(false);
        adIcon.SetActive(false);
        if(SaveLoadManager.GetGameSpeed() == 1)
        {
            xText.text = "X1";
            button.onClick.AddListener(SpeedUpGame);
        }
        else
        {
            xText.text = "X2";
            button.onClick.AddListener(SlowDownGame);
        }
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
        SaveLoadManager.SetGameSpeed(2f);
        Debug.Log($"Game speed is {SaveLoadManager.GetGameSpeed()}");
        Time.timeScale = 2f;
        xText.text = "X2";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(SlowDownGame);
    }

    void SlowDownGame()
    {
        SaveLoadManager.SetGameSpeed(1f);
        Debug.Log($"Game speed is {SaveLoadManager.GetGameSpeed()}");
        Time.timeScale = 1f;
        xText.text = "X1";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(SpeedUpGame);
    }
}
