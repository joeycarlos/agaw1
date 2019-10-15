using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmyManager : MonoBehaviour
{
    public int enemiesPerRow = 10;
    public int numberOfRows = 5;
    public float horizontalSpacing = 2.0f;
    public float verticalSpacing = 2.0f;
    public GameObject enemy1;

    public float movementTimeInterval = 0.3f;
    public float horizontalMoveDistance = 0.5f;
    public float verticalMoveDistance = 1f;
    private float previousMovementTimeInterval;

    private bool moveRight;
    public bool moveRightSwitchPending;

    [Range(0.0f, 1.0f)]
    public float shieldSpawnPercentage = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = true;
        previousMovementTimeInterval = movementTimeInterval;

        SpawnEnemies();

        InvokeRepeating("Move", movementTimeInterval, movementTimeInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemies() {
        for (int i = 0; i < enemiesPerRow; i++) {
            for (int j = 0; j < numberOfRows; j++) {
                Vector3 offset = new Vector3(i * horizontalSpacing, -j * verticalSpacing, 0);
                GameObject iEnemy = Instantiate(enemy1, transform.position + offset, Quaternion.identity, this.transform);
                if (Random.Range(0f, 1f) < shieldSpawnPercentage) {
                    iEnemy.GetComponent<Enemy>().hasShield = true;
                }
                iEnemy.GetComponent<Enemy>().Init();
                GameManager.Instance.currentEnemyCount++;
            }
        }
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

        } else {
            if (moveRight == true)
                moveVector = new Vector3(horizontalMoveDistance, 0, 0);
            else
                moveVector = new Vector3(-horizontalMoveDistance, 0, 0);      
        }

        transform.Translate(moveVector);

    }
}
