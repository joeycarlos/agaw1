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

    public int levelsCompleted { get; set; }

    public bool levelHasStarted { get; set; }

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        activeScene = SceneManager.GetActiveScene().buildIndex;
        levelsCompleted = 0;

        levelHasStarted = false;
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
                if (Input.GetKeyDown(KeyCode.Space)) {
                    NextLevel();
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
                if (Input.GetKeyUp(KeyCode.Space)) {
                    levelHasStarted = true;
                    EnemyArmyManager.Instance.StartBehaviour();
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
        levelsCompleted++;
        levelHasStarted = false;
        if (levelsCompleted < SceneManager.sceneCountInBuildSettings - 5) {
            SceneManager.LoadScene((int)Scene.LevelComplete); 
        } else {
            Win();
        }

    }

    public void NextLevel() {
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
    }

}
