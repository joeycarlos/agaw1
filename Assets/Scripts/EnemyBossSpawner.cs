using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSpawner : MonoBehaviour
{
    private static EnemyBossSpawner _instance;

    public static EnemyBossSpawner Instance {
        get {
            if (_instance == null) {
                GameObject go = new GameObject("EnemyBossSpawner");
                go.AddComponent<EnemyArmyManager>();
            }

            return _instance;
        }
    }

    public GameObject enemyBoss;
    public float spawnMinInterval = 7.0f;
    public float spawnMaxInterval = 15.0f;

    private float timeUntilSpawn;
    public bool currentlySpawning { get; set; }

    private float horizontalSpawnDistance;
    private int boundaryLayer;

    public float speed;
    public float projectileSpeed;
    public int burstShotSize = 5;
    public float burstShotInterval = 0.1f;
    public float minTimeBetweenBursts = 3.0f;
    public float maxTimeBetweenBursts = 5.0f;

    public int scoreValue = 30;

    public int maxNumOfBosses = 3;
    public int bossesLeft;

    void Awake() {
        _instance = this;
    }

        // Start is called before the first frame update
   void Start() {
        bossesLeft = maxNumOfBosses;
        boundaryLayer = LayerMask.GetMask("Boundary");
        timeUntilSpawn = Random.Range(spawnMinInterval, spawnMaxInterval);
        currentlySpawning = true;
        transform.position = new Vector3(0, 8.0f, 0);
        CalculateSpawnDistance();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlySpawning && GameManager.Instance.levelHasStarted == true) {
            if (timeUntilSpawn <= 0 && bossesLeft > 0)
                SpawnEnemyBoss();
            timeUntilSpawn -= Time.deltaTime;
        }
    }

    void CalculateSpawnDistance() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 20.0f, boundaryLayer);
        if (hit.collider != null) {
            horizontalSpawnDistance = hit.distance;
            
        }
    }

    void SpawnEnemyBoss() {
        Vector3 spawnLocation;
        bool leftToRight = (Random.value > 0.5f);

        if (leftToRight)
            spawnLocation = new Vector3(-horizontalSpawnDistance + 2.0f, transform.position.y, 0);
        else
            spawnLocation = new Vector3(horizontalSpawnDistance - 2.0f, transform.position.y, 0);

        GameObject iEnemyBoss = Instantiate(enemyBoss, spawnLocation, Quaternion.identity);
        EnemyBoss iEnemyBossComponent = iEnemyBoss.GetComponent<EnemyBoss>();
        iEnemyBossComponent.leftToRight = leftToRight;
        iEnemyBossComponent.speed = speed;
        iEnemyBossComponent.projectileSpeed = projectileSpeed;
        iEnemyBossComponent.burstShotSize = burstShotSize;
        iEnemyBossComponent.burstShotInterval = burstShotInterval;
        iEnemyBossComponent.minTimeBetweenBursts = minTimeBetweenBursts;
        iEnemyBossComponent.maxTimeBetweenBursts = maxTimeBetweenBursts;

    timeUntilSpawn = Random.Range(spawnMinInterval, spawnMaxInterval);
        currentlySpawning = false;
    }
}
