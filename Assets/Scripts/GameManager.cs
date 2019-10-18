﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    enum Scene {
        Splash = 0,
        Instructions = 1,
        LevelComplete = 2,
        GameOver = 3,
        Win = 4,
        Level1 = 5,
        Level2 = 6
    };

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public int activeScene { get; private set; }

    public int levelsCompleted { get; set; }

    public bool levelHasStarted { get; set; }

    private int _score;
    public int Score {
        get {
            return _score;
        }
        set {
            _score = value;
            if (GameplayUIManager.Instance != null) {
                GameplayUIManager.Instance.UpdateScore();
            }
        }
    }

    private int _attackLevel;
    public int AttackLevel {
        get {
            return _attackLevel;
        }
        set {
            _attackLevel = value;
            if (GameplayUIManager.Instance != null) {
                GameplayUIManager.Instance.UpdateAttackLevel();
            }
            if (_attackLevel != 0)
                GameplayUIManager.Instance.AttackUpAnnouncement();
        }
    }

    private int _speedLevel;
    public int SpeedLevel {
        get {
            return _speedLevel;
        }
        set {
            _speedLevel = value;
            if (GameplayUIManager.Instance != null) {
                GameplayUIManager.Instance.UpdateSpeedLevel();
            }
            if (_speedLevel != 0)
                GameplayUIManager.Instance.SpeedUpAnnouncement();
        }
    }

    public float elapsedTimeThisLevel { get; set; }

    public float elapsedTimeTotal { get; set; }

    public float initialMoveSpeed = 5.0f;
    public float initialShotInterval = 0.7f;
    public float initialProjectileSpeed = 4.0f;

    private float expectedLevelTime;
    public int timeBonus;
    public int scoreWithBonus;

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        activeScene = SceneManager.GetActiveScene().buildIndex;
        levelsCompleted = 0;

        levelHasStarted = false;

        Score = 0;
        elapsedTimeThisLevel = 0;
        elapsedTimeTotal = 0;
        AttackLevel = 0;
        SpeedLevel = 0;
        
        InitializePlayerData();
    }

    private void Start() {
    }

    void Update() {
        ProcessSceneLogic();

        activeScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void ProcessSceneLogic() {
        switch (activeScene) {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Instructions();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartMainGame();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space) && LevelCompleteUI.Instance.doneAnimation == true) {
                    NextLevel();
                } else if (Input.GetKeyDown(KeyCode.Space) && LevelCompleteUI.Instance.doneAnimation == false) {
                    LevelCompleteUI.Instance.SkipAnimation();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    RestartGame();
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    RestartGame();
                }
                break;
            default:
                if (levelHasStarted) {
                    elapsedTimeThisLevel += Time.deltaTime;
                }
                break;

        }
    }

    public void GameOver() {
        SceneManager.LoadScene((int)Scene.GameOver);
    }

    public void Win() {
        SceneManager.LoadScene((int)Scene.Win);
    }

    public void LevelComplete() {
        EnemyArmyManager.Instance.ClearInvokes();
        SavePlayerData();
        elapsedTimeTotal += elapsedTimeThisLevel;
        Debug.Log("ELAPSED TIME THIS LEVEL: " + elapsedTimeThisLevel);
        Debug.Log("ELAPSED TIME TOTAL: " + elapsedTimeTotal);
        
        CalculateTimeBonus();
        Debug.Log("SCORE BEFORE BONUS: " + Score);
        Debug.Log("TIME BONUS: " + timeBonus);
        Debug.Log("CORRECT SCORE SUM :" + Score + " " + timeBonus);

        levelsCompleted++;
        levelHasStarted = false;
        if (levelsCompleted < SceneManager.sceneCountInBuildSettings - 5) {
            SceneManager.LoadScene((int)Scene.LevelComplete); 
        } else {
            Win();
        }

    }

    public void NextLevel() {
        Score += (int)timeBonus;

        Debug.Log("SCORE AFTER LEVEL LOAD: " + Score);
        elapsedTimeThisLevel = 0;
        SceneManager.LoadScene(levelsCompleted + 5);
    }

    public void StartMainGame() {
        SceneManager.LoadScene((int)Scene.Level1);
    }

    public void Instructions() {
        SceneManager.LoadScene((int)Scene.Instructions);
    }

    public void RestartGame() {
        SceneManager.LoadScene((int)Scene.Splash);
        levelsCompleted = 0;
        Score = 0;
        elapsedTimeTotal = 0;
        ResetPlayerData();
    }

    public PlayerData playerData;

    public void InitializePlayerData() {
        if (playerData == null)
            playerData = new PlayerData(initialMoveSpeed, initialShotInterval, initialProjectileSpeed);
    }

    public void SavePlayerData() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerData._moveSpeed = player.MoveSpeed;
        playerData._shotInterval = player.ShotInterval;
        playerData._projectileSpeed = player.ProjectileSpeed;
    }

    public void LoadPlayerData() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.MoveSpeed = playerData._moveSpeed;
        player.ShotInterval = playerData._shotInterval;
        player.ProjectileSpeed = playerData._projectileSpeed;
    }

    public void ResetPlayerData() {
        playerData = null;
    }

    public void StartGameplay() {
        levelHasStarted = true;
        GameplayUIManager.Instance.RemoveLevelStartMessage();
        expectedLevelTime = EnemyArmyManager.Instance.expectedTime;
    }

    public void CalculateTimeBonus() {
        timeBonus = (int)Mathf.Clamp((expectedLevelTime - elapsedTimeThisLevel) * 10.0f, 0, 10000.0f);
        Debug.Log("Expected time: " + expectedLevelTime);
        Debug.Log("Actual Level Time: " + elapsedTimeThisLevel);
        Debug.Log("Time Bonus: " + timeBonus);
        scoreWithBonus = Score + timeBonus;
    }

}
