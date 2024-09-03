using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script used to unpdate the skill level and cost texts
public class SkillTextInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI skillLevelText;
    [SerializeField] TextMeshProUGUI upgradeCostText;

    public void UpdateSkillText(int skillLevel, int upgradeCost)
    {
        skillLevelText.text = "Lv. " + skillLevel.ToString();

        upgradeCostText.text = "Cost: " + upgradeCost.ToString();
    }
}
