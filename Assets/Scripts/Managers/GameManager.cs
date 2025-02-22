﻿using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Bson;
using YG;

//script to control the game
public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("GameManager").GetComponent<GameManager>();

            return instance;
        }
    }
    public bool IsBattling { get; private set; }

    [SerializeField] Player player; //reference to the Player script
    [SerializeField] float nextRoundDelay; //seconds delay before starting next round
    [SerializeField] int timeLimit; //the time limit of each round

    int level; //the monster level
    int countdown; //the number shows on top-left

    private void OnEnable()
    {
        YandexGame.StopTheGameEvent += StopTheGame;
        YandexGame.StartTheGameEvent += StartTheGame;
        YandexGame.CloseFullAdEvent += StartTheGame;
        YandexGame.CloseVideoEvent += StartTheGame;
    }

    private void OnDisable()
    {
        YandexGame.StopTheGameEvent -= StopTheGame;
        YandexGame.StartTheGameEvent -= StartTheGame;
        YandexGame.CloseFullAdEvent -= StartTheGame;
        YandexGame.CloseVideoEvent -= StartTheGame;
    }

    void StopTheGame()
    {
        Time.timeScale = 0f;
        VolumeManager.Instance.PauseSounds();
    }

    void StartTheGame()
    {
        Time.timeScale = SaveLoadManager.GetGameSpeed();
        VolumeManager.Instance.UnPauseSounds();
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;

            HUD.Instance.OnLevelVisualChanged?.Invoke(level);
        }
    }

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

        if (Menu.newGame) //if it's a new game we get a new instance of PlayerData
        {
            SaveLoadManager.Reset();
        }
        StartTheGame();
    }

    void Start()
    {
        EnemyFactory.Instance.SpawnEnemyBones();

        StartBattle(); //when everything is ready, start the fight
    }

    private void StartBattle()
    {
        player.StopRunning();
        Level = SaveLoadManager.GetLevel();

        EnemyFactory.Instance.RespawnEnemies(level);

        IsBattling = true;

        StartCoroutine(nameof(StartCountDown)); //start the time limit count down

        player.StartAttack();
    }

    public void StopBattle(bool currentLevelCompleted = false)
    {
        player.StopAttack();

        StopCoroutine(nameof(StartCountDown));

        IsBattling = false;

        if (currentLevelCompleted) //if the battle is stopped because all enemies have been killed, we increase the monster level by 1 and check if there is any new skills to unlock
        {
            SaveLoadManager.AchiveNewLevel();

            SkillManager.Instance.UnlockSkillCheck(); //unlock new skills if reaching a required level
            BgMover.Instance.OnLevelCompleted?.Invoke();
            player.StartRunning();
            SoundManager.Instance.PlayWinSound();
            YGManager.SaveNewScore(SaveLoadManager.GetLevel());
            if(SaveLoadManager.GetLevel() % 5 == 0 || SaveLoadManager.GetLevel() < 5)
            {
                YGManager.SendMetrica("levelPassed", SaveLoadManager.GetLevel());
            }
        }
        else
        {
            player.GetHurt();
            SoundManager.Instance.PlayLooseSound();
            YGManager.ShowFullAd();
        }

        EnemyFactory.Instance.EnemiesList.Clear(); //remove everything in the alive enemy list

        Invoke(nameof(StartBattle), nextRoundDelay); //start a new battle after seconds delay
    }

    private IEnumerator StartCountDown()
    {
        HUD.Instance.OnCountDownTextChanged?.Invoke(timeLimit.ToString());

        countdown = timeLimit;

        while (countdown > 0 && IsBattling)
        {
            yield return new WaitForSeconds(1f); //run the loop per second

            countdown--;

            HUD.Instance.OnCountDownTextChanged?.Invoke(countdown.ToString());
        }

        StopBattle(); //stop the battle if running out of time

        Debug.Log("time out");
    }
}
