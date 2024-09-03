using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script set the damagepopup text
public class DamagePopup : MonoBehaviour
{
    public void Show(int damage)
    {
        GetComponent<TextMeshProUGUI>().text = damage.ToString(); //set the text to the number of the damage you made
    }
}
