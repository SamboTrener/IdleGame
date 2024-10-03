using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    [SerializeField] Enemy enemyPrefab; //the enemy prefab
    [SerializeField] Transform enemiesParent; //the parent object of enemy
    [SerializeField] int paddingBetweenEnemiesCoef;

    public List<Enemy> EnemyBoneList { get; } = new();
    public List<Enemy> EnemiesList { get; } = new(); //a list of alive enmeies

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEnemyBones()
    {
        for (int i = 0; i < 4; i++) //create 3*3 enemy prefabs
        {
            for (int j = 0; j < 2; j++)
            {
                var enemy = Instantiate(enemyPrefab);

                EnemyBoneList.Add(enemy);

                enemy.transform.SetParent(enemiesParent);

                enemy.transform.localPosition = new Vector2(i * paddingBetweenEnemiesCoef, j * paddingBetweenEnemiesCoef);

                enemy.transform.name = "Monster (" + i + ", " + j + ") ";
            }
        }
    }

    public void RespawnEnemies(int level)
    {
        foreach (Enemy enemyBone in EnemyBoneList)
        {
            enemyBone.SpawnEnemy(level);

            EnemiesList.Add(enemyBone);
        }
    }
}
