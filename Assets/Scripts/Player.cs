using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

//script used to control accack
public class Player : MonoBehaviour
{

    [SerializeField] Animator playerAnim; //player animator
    [SerializeField] Cooldown cooldown;

    public void StartAttack() //function for other scripts to call the attack coroutine
    {
        StartCoroutine(nameof(Attack));
    }

    public void StopAttack() //function to stop attack
    {
        StopCoroutine(nameof(Attack));

        cooldown.StopCoroutine("WaitForCooldown");

    }

    public void GetHurt()
    {
        playerAnim.SetTrigger("Hurt");
    }

    public void StartRunning()
    {
        playerAnim.SetBool("IsRunning",true);
    }

    public void StopRunning()
    {
        playerAnim.SetBool("IsRunning", false);
    }

    private IEnumerator Attack() //function to make the attack
    {
        while (EnemyFactory.Instance.EnemiesList.Count > 0) //only attack there is at least one alvie enemy
        {
            yield return cooldown.StartCoroutine("WaitForCooldown"); //wait until cooldown finishes

            //get how many targets to attack. If more enemies than the number of max targets, n equals to max target, otherwise the amount of enemies left
            int targetCount = (SkillManager.Instance.CurrentActiveSkill.NumberOfTargets <= EnemyFactory.Instance.EnemiesList.Count) ?
                SkillManager.Instance.CurrentActiveSkill.NumberOfTargets : EnemyFactory.Instance.EnemiesList.Count;

            var possibleTargets = new List<Enemy>();
            foreach(var enemy in EnemyFactory.Instance.EnemiesList)
            {
                possibleTargets.Add(enemy);
            }
            for(int i = 0; i < targetCount; i++)
            {
                var target = possibleTargets[Random.Range(0, possibleTargets.Count)];
                target.TakeDamage(SkillManager.Instance.CurrentActiveSkill.AttackDamage);
                possibleTargets.Remove(target);
            }

            playerAnim.SetTrigger("Attack"); //trigger the player animation
            SoundManager.Instance.PlayAttackSound();
        }

        if (GameManager.Instance.IsBattling) //if we kill all enemies in time
        {
            Debug.Log("All Killed");

            GameManager.Instance.StopBattle(true); //we stop the battle with levelCompleted set to true
        }
    }
}
