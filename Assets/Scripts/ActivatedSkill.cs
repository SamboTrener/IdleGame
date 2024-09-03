using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField] Color newSkillAlarmColor; //the color of the image
    [SerializeField] Image activatedSkillIcon;
    [SerializeField] Button changeActivatedSkillButton;

    Canvas canvas;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }

        canvas = GetComponent<Canvas>();

        changeActivatedSkillButton.onClick.AddListener(ChangeActiveSkill);
    }

    void Start()
    {
        activatedSkillIcon.sprite = SkillManager.Instance.CurrentActiveSkill.SkillIcon; //display the actived skill icon
    }

    void ChangeActiveSkill() //function called when you click the "Accept" button
    {
        if (SkillManager.Instance.SelectedSkill != null && SkillManager.Instance.CurrentActiveSkill != SkillManager.Instance.SelectedSkill) 
            //we change the activated skill only when you selected a active skill and it's not the same as the one currently you are using
        {
            SkillManager.Instance.CurrentActiveSkill = SkillManager.Instance.SelectedSkill;

            HUD.Instance.UpdateSkillText();

            activatedSkillIcon.sprite = SkillManager.Instance.CurrentActiveSkill.SkillIcon;
        }

        HUD.Instance.HideAbilityWindow(); //hide the active skill selecting window
    }

    public void StartNewSkillAlarm() //start new skill alarm
    {
        newSkillAlarm.enabled = true; //enable the image

        StartCoroutine(nameof(NewSkillAlarm));
    }

    public void StopNewSkillAlarm()
    {
        newSkillAlarm.enabled = false; //disable the image

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

            yield return null;
        }
    }
}
