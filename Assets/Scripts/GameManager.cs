using System.Collections;
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

    public int currentEnemyCount { get; set; }
    public int levelsCompleted { get; set; }

    private EnemyArmyManager enemyArmyManager;

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        activeScene = SceneManager.GetActiveScene().buildIndex;
        currentEnemyCount = 0;
        levelsCompleted = 0;

        if (activeScene >= (int)Scene.Level1)
            enemyArmyManager = GameObject.FindGameObjectWithTag("EnemyArmyManager").GetComponent<EnemyArmyManager>();
    }

    void Update() {
        ProcessSceneLogic();

        activeScene = SceneManager.GetActiveScene().buildIndex;

        if (currentEnemyCount == 0 && activeScene >= (int)Scene.Level1) SceneManager.LoadScene((int)Scene.Win);
    }

    public void ProcessSceneLogic() {
        switch (activeScene) {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene((int)Scene.Instructions);
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene((int)Scene.Level1);
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene((int)Scene.Level1);
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene((int)Scene.Splash);
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Space)) {
                    SceneManager.LoadScene((int)Scene.Splash);
                }
                break;
            default:
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
        SceneManager.LoadScene((int)Scene.LevelComplete);
    }

    public void StartMainGame() {
        SceneManager.LoadScene((int)Scene.Level1);
    }

    public void Instructions() {
        SceneManager.LoadScene((int)Scene.Instructions);
    }

}
