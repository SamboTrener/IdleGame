using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Color minColor = Color.red; //color when health is low
    Color maxColor = Color.green; //color when health is high

    Image barImage;
    RectTransform rectTransform;
    float width, height;

    void Awake()
    {
        barImage = GetComponent<Image>();

        barImage.color = maxColor;

        rectTransform = GetComponent<RectTransform>();

        width = rectTransform.rect.width;

        height = rectTransform.rect.height;
    }

    public void Change(float healthRatio) //adjust the length and color of the health bar according to the health ratio (health / maxHealth)
    {
        rectTransform.sizeDelta = new Vector2(healthRatio * width, height);

        barImage.color = Color.Lerp(minColor, maxColor, healthRatio);
    }

    public void Reset() //reset health bar to full
    {
        barImage.color = maxColor;

        rectTransform.sizeDelta = new Vector2(width, height);
    }
}
