using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int activeScene;
    private int currentEnemyCount;

    private EnemyArmyManager enemyArmyManager;

    // Start is called before the first frame update
    void Awake()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        currentEnemyCount = 0;

        if (activeScene >= 2)
        enemyArmyManager = GameObject.FindGameObjectWithTag("EnemyArmyManager").GetComponent<EnemyArmyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeScene == 0 || activeScene == 1) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(2);
            }
        }
    }
    
    public void GameOver() {
        SceneManager.LoadScene(0);
    }

    public void DecreaseEnemyCount () {
        currentEnemyCount--;
        IncreaseArmySpeed();
        if (currentEnemyCount == 0) {
            SceneManager.LoadScene(1);
        }
    }

    public void IncreaseEnemyCount() {
        currentEnemyCount++;
    }

    public void IncreaseArmySpeed() {
        enemyArmyManager.movementTimeInterval = Mathf.Clamp(enemyArmyManager.movementTimeInterval - 0.005f, 0.001f, 10f);
    }
}
