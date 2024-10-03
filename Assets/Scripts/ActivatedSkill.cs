using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

//Script used to controlled the activated skill
public class ActivatedSkill : MonoBehaviour
{
    //Singleton
    private static ActivatedSkill instance;
    public static ActivatedSkill Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("Player/ActivatedSkill").GetComponent<ActivatedSkill>();

            return instance;
        }
    }

    [SerializeField] Image newSkillAlarm; //when you get a new active skill, this image will pop out
    [SerializeField] TextMeshProUGUI newSkillAlarmText;
    [SerializeField] Color newSkillAlarmColor; //the color of the image
    [SerializeField] Image activatedSkillIcon;
    [SerializeField] Button changeActivatedSkillButton;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
                Destroy(gameObject);
        }

        changeActivatedSkillButton.onClick.AddListener(ChangeActivatedSkill);
    }

    void Start()
    {
        activatedSkillIcon.sprite = SkillManager.Instance.CurrentActiveSkill.SkillIcon; 
    }

    void ChangeActivatedSkill() 
    {
        if (SkillManager.Instance.TryChangeActivatedSkill())
        {
            activatedSkillIcon.sprite = SkillManager.Instance.CurrentActiveSkill.SkillIcon;
        }

        HUD.Instance.HideAbilityWindow(); 
    }

    public void StartNewSkillAlarm() //start new skill alarm
    {
        newSkillAlarm.enabled = true; //enable the image
        newSkillAlarmText.gameObject.SetActive(true);

        StartCoroutine(nameof(NewSkillAlarm));
    }

    public void StopNewSkillAlarm()
    {
        newSkillAlarm.enabled = false; //disable the image
        newSkillAlarmText.gameObject.SetActive(false);

        StopCoroutine(nameof(NewSkillAlarm));
    }

    IEnumerator NewSkillAlarm()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * 1.5f, 1f); //a float will move back and forth between 0 and 1

            Color c = newSkillAlarmColor; //create an identical color

            c.a = 0; //set the alpha value of the new color to 0

            newSkillAlarm.color = Color.Lerp(newSkillAlarmColor, c, t); //loop the color of the alarm image between these two colors
            newSkillAlarmText.color = Color.Lerp(newSkillAlarmColor, c, t); //loop the color of the alarm image between these two colors

            yield return null;
        }
    }
}
