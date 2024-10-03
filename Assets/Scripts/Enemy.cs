using UnityEngine;

//script controls the enemy
public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject damagePopup; //a reference to the damagePopup prefab
    [SerializeField] Transform popupSpawnPoint; //the parent object
    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] ParticleSystem deathParticle; //particle will play after death
    [SerializeField] GameObject canvas; //the canvas used to show health bar and damage popup text
    [SerializeField] HealthBar healthBar; //reference to the healthBar script on this enemy

    int maxHealth; //the max health
    int loot; //how much money the player will get after killing this enemy
    int health; //the current health
    Animator anim;

    public void TakeDamage(int damage) //function get called if the player attacks this enemy
    {
        if (!anim) //see if ther is no monster prefab under this enemy object. this should never happen
        {
            Debug.Log("Need to reset monster");

            return;
        }

        health -= damage;

        ShowDamage(damage);

        if (health <= 0) //if the current health is equal to or smaller than 0, this enmey should die
        {
            health = 0; //change the value to 0, make sure it's not negative. we will used this value to calculate health ratio

            Destroy(anim.gameObject);

            deathParticle.Play(); //play death partivle

            canvas.SetActive(false); //hide the canvas

            SaveLoadManager.EarnMoney(loot); //give the player the reward

            EnemyFactory.Instance.EnemiesList.Remove(this); //remove this enmey from the enemy list

            SoundManager.Instance.PlayGoblinDieSound();
        }

        anim.SetTrigger("Damage"); //trigger the animation when the enemy gets attacked

        healthBar.Change((float)health / maxHealth); //call the function in HealthBar script to update the health bar
    }

    void ShowDamage(int damage)
    {
        GameObject popupInstance = Instantiate(damagePopup); //create the damage popup text

        popupInstance.transform.SetParent(popupSpawnPoint, false);

        popupInstance.GetComponent<DamagePopup>().Show(damage); //call the function in DamagePupup script

        Destroy(popupInstance, 1f);
    }

    public void SpawnEnemy(int level) //function used to spawn monster prefab
    {
        if (anim) //if this enmey doesn't get killed in the last round, we destroy the old one
            Destroy(anim.gameObject); //destroy the old monster prefab

        maxHealth = (int)(100 * Mathf.Pow(1.2f, level)); //formula to determine the health of emeny

        loot = level; //formula to determine the amount of the reward

        health = maxHealth; //set current health to full

        healthBar.Reset(); //reset the health bar

        GameObject monster = Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)]); //randomly create a monster prefab

        monster.transform.SetParent(transform);

        monster.transform.localPosition = Vector2.zero;

        canvas.SetActive(true); //show canvas again

        anim = monster.GetComponent<Animator>(); //set the reference to the animator on the newly created monster prefab
    }
}
