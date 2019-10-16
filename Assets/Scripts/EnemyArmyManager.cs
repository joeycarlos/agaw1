using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmyManager : MonoBehaviour
{
    private static EnemyArmyManager _instance;

    public static EnemyArmyManager Instance {
        get {
            if (_instance == null) {
                GameObject go = new GameObject("EnemyArmyManager");
                go.AddComponent<GameManager>();
                go.transform.position = new Vector3(-8, 5, 0);
            }

            return _instance;
        }
    }

    public GameObject enemy1;
    public int enemiesPerRow = 10;
    public int numberOfRows = 5;
    public float horizontalSpacing = 2.0f;
    public float verticalSpacing = 2.0f;

    public float movementTimeInterval = 0.3f;
    public float horizontalMoveDistance = 0.5f;
    public float verticalMoveDistance = 1f;
    private float previousMovementTimeInterval;

    public int numberOfShieldedRows = 1;

    private bool moveRight;
    public bool moveRightSwitchPending;

    /*
    [Range(0.0f, 1.0f)]
    public float shieldSpawnPercentage = 0.5f;
    */

    public float currentEnemyCount { get; set; }
    private bool enemiesHaveSpawned;

    void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        moveRight = true;
        enemiesHaveSpawned = false;
        previousMovementTimeInterval = movementTimeInterval;
        currentEnemyCount = 0;

        SpawnEnemies();

        InvokeRepeating("Move", movementTimeInterval, movementTimeInterval);
    }

    void Update() {
        if (enemiesHaveSpawned == true && currentEnemyCount == 0)
            GameManager.Instance.LevelComplete();
    }

    void SpawnEnemies() {
        for (int i = 0; i < enemiesPerRow; i++) {
            for (int j = 0; j < numberOfRows; j++) {
                Vector3 offset = new Vector3(i * horizontalSpacing, -j * verticalSpacing, 0);
                GameObject iEnemy = Instantiate(enemy1, transform.position + offset, Quaternion.identity, this.transform);
                if (j >= numberOfRows-numberOfShieldedRows)
                    iEnemy.GetComponent<Enemy>().hasShield = true;
                iEnemy.GetComponent<Enemy>().Init();
                currentEnemyCount++;
            }
        }
        enemiesHaveSpawned = true;
    }

    void Move() {

        if (movementTimeInterval != previousMovementTimeInterval) {
            CancelInvoke("Move");
            InvokeRepeating("Move", movementTimeInterval, movementTimeInterval);
        }

        Vector3 moveVector;

        if (moveRightSwitchPending == true) {
            moveRight = !moveRight;
            moveRightSwitchPending = false;

            moveVector = new Vector3(0, -verticalMoveDistance, 0);

        }
        else {
            if (moveRight == true)
                moveVector = new Vector3(horizontalMoveDistance, 0, 0);
            else
                moveVector = new Vector3(-horizontalMoveDistance, 0, 0);
        }

        transform.Translate(moveVector);
    }
}
