using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    private float timeUntilNextShot;

    private BoxCollider2D bc;

    public GameObject enemyShield;
    public bool hasShield;
    private GameObject iShield;

    public GameObject enemyDeathEffect;

    public GameObject enemyShieldBreakEffect;

    private int enemyLayer;

    private SpriteRenderer sr;
    public Sprite defaultSprite;
    public Sprite shieldedSprite;

    void Awake() {
        hasShield = false;
        enemyLayer = LayerMask.GetMask("Enemy");
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() {
        bc = GetComponent<BoxCollider2D>();

        timeUntilNextShot = GenerateShotTime();
    }

    public void Init() {
        if (hasShield == true) {
            iShield = Instantiate(enemyShield, transform.position, Quaternion.identity, this.transform);
            sr.sprite = shieldedSprite;
        }
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.levelHasStarted)
            ProcessShooting();
    }

    float GenerateShotTime() {
        return Random.Range(EnemyArmyManager.Instance.minShotTimeInterval, EnemyArmyManager.Instance.maxShotTimeInterval);
    }

    bool IsShooter() {
        Vector3 raycastOriginOffset = new Vector3(0, -(bc.size.y/2), 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastOriginOffset, -Vector2.up, 20.0f, enemyLayer);
        if (hit.collider != null) {
            return false;
        } else {
            return true;
        }
    }

    void ProcessShooting() {
        timeUntilNextShot = timeUntilNextShot - Time.deltaTime;

        if (timeUntilNextShot <= 0) {
            if (IsShooter()) Shoot();
            timeUntilNextShot = GenerateShotTime();
        }
    }

    void Shoot() {
        Vector3 shotOriginOffset = new Vector3(0, -(bc.size.y/2 + 0.5f), 0);
        GameObject iProjectile = Instantiate(projectile, transform.position + shotOriginOffset, Quaternion.identity);
        iProjectile.GetComponent<EnemyProjectile>().speed = EnemyArmyManager.Instance.projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Boundary"))
            EnemyArmyManager.Instance.moveRightSwitchPending = true;
        else if (col.gameObject.layer == LayerMask.NameToLayer("LowerBoundary"))
            GameManager.Instance.GameOver();
    }

    public void TakeDamage() {
        if (hasShield == true) {
            Destroy(iShield);
            sr.sprite = defaultSprite;
            GameObject iShieldBreakEffect = Instantiate(enemyShieldBreakEffect, transform.position, Quaternion.identity);
            Destroy(iShieldBreakEffect, 1.0f);
            hasShield = false;
        }
        else {
            EnemyArmyManager.Instance.currentEnemyCount--;
            GameManager.Instance.Score += EnemyArmyManager.Instance.scoreValue;
            GameplayUIManager.Instance.ScoreNotification(EnemyArmyManager.Instance.scoreValue, transform.position, new Vector3(0, 0, 0));
            GameObject iDeathEffect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
            Destroy(iDeathEffect, 1.0f);
            Destroy(gameObject);
        }
    }
}
