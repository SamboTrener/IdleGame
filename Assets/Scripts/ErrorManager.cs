using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager Instance { get; private set; }

    public Action<int> OnWeaponPickError;
    public Action OnUpgradeBuyError;

    TextMeshProUGUI errorText;
    [SerializeField] GameObject errorPrefab;
    [SerializeField] Transform UI;

    private void Awake()
    {
        Instance = this;

        OnWeaponPickError += ShowWeaponError;
        OnUpgradeBuyError += ShowUpgradeError;

        errorText = errorPrefab.GetComponentInChildren<TextMeshProUGUI>();
    }

    void ShowUpgradeError()
    {
        errorText.text = $"Not enough money to buy";
        var er = Instantiate(errorPrefab, UI);
        Destroy(er, 1f);
    }

    void ShowWeaponError(int levelToUnlock)
    {
        errorText.text = $"This weapon can be selected after the enemies reach level {levelToUnlock}";
        var er = Instantiate(errorPrefab, UI);
        Destroy(er, 1f);
    }
}
