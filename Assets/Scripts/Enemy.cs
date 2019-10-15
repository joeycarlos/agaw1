using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 3.0f;
    public float minShotTimeInterval = 3.0f;
    public float maxShotTimeInterval = 20.0f;
    private float timeUntilNextShot;

    private BoxCollider2D bc;

    private EnemyArmyManager enemyArmyManager;
    private GameManager gameManager;

    public GameObject enemyShield;
    public bool hasShield;
    private GameObject iShield;

    void Awake() {
        hasShield = false;    
    }

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();

        enemyArmyManager = GetComponentInParent<EnemyArmyManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        timeUntilNextShot = GenerateShotTime();
    }

    public void Init() {
        if (hasShield == true) {
            iShield = Instantiate(enemyShield, transform.position, Quaternion.identity, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessShooting();
    }

    float GenerateShotTime() {
        return Random.Range(minShotTimeInterval, maxShotTimeInterval);
    }

    void ProcessShooting() {

        timeUntilNextShot = timeUntilNextShot - Time.deltaTime;

        if (timeUntilNextShot <= 0) {
            Shoot();
            timeUntilNextShot = GenerateShotTime();
        }
        
    }

    void Shoot() {
        GameObject iProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        iProjectile.GetComponent<EnemyProjectile>().speed = projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Boundary"))
            enemyArmyManager.moveRightSwitchPending = true;
        else if (col.gameObject.layer == LayerMask.NameToLayer("LowerBoundary"))
            gameManager.GameOver();

    }

    public void TakeDamage() {
        if (hasShield == true) {
            Destroy(iShield);
            hasShield = false;
        }
        else {
            gameManager.DecreaseEnemyCount();
            Destroy(gameObject);
            
        }
        
    }
}
