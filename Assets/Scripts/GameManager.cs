using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    /*
    private int _currentEnemyCount;

    public int currentEnemyCount {
        get {
            return _currentEnemyCount;
        }
        set {
            _currentEnemyCount = value;
            if (_currentEnemyCount == 0) SceneManager.LoadScene(1);
        }
    }
    */
    public int currentEnemyCount { get; set; }

    private EnemyArmyManager enemyArmyManager;

    private void Awake() {
        _instance = this;

        activeScene = SceneManager.GetActiveScene().buildIndex;
        currentEnemyCount = 0;

        if (activeScene >= 2)
            enemyArmyManager = GameObject.FindGameObjectWithTag("EnemyArmyManager").GetComponent<EnemyArmyManager>();
    }

    void Update() {
        if (activeScene == 0 || activeScene == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(2);
            }
        }
    }

    public void GameOver() {
        SceneManager.LoadScene(0);
    }

    public void DecreaseEnemyCount() {
        currentEnemyCount--;
        if (currentEnemyCount == 0) {
            SceneManager.LoadScene(1);
        }
    }

}
