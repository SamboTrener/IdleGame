using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//script controls when to attack and display the colldown visual effect
public class Cooldown : MonoBehaviour
{
    [SerializeField] Image cooldown; //the image of visual effect

    float timer; //when this number is bigger than attackInterval, the character attacks
    bool isCooldown = false;

    public IEnumerator WaitForCooldown()
    {
        cooldown.fillAmount = 1f;

        timer = 0f;

        isCooldown = true;

        while (isCooldown)
        {
            timer += Time.deltaTime;

            if (timer >= SkillManager.Instance.CurrentActiveSkill.AttackInterval)
            {
                isCooldown = false;

                cooldown.fillAmount = 0f;

                break;
            }

            cooldown.fillAmount = 1f - timer / SkillManager.Instance.CurrentActiveSkill.AttackInterval;

            yield return null;
        }
    }
}
