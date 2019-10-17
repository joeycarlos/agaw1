using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public GameObject movementPickup;
    public GameObject attackPickup;

    public float speed = 3.0f;
    public bool leftToRight { get; set; }

    public GameObject projectile;
    public float projectileSpeed = 3.0f;
    private float timeUntilNextShot;

    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start() {
        timeUntilNextShot = 3.0f;
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        ProcessShooting();
    }

    void Move() {
        Vector3 moveVector;
        if (leftToRight == true)
            moveVector = new Vector3(speed * Time.deltaTime, 0, 0);
        else {
            moveVector = new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        transform.Translate(moveVector);
    }

    void ProcessShooting() {
        timeUntilNextShot = timeUntilNextShot - Time.deltaTime;

        if (timeUntilNextShot <= 0) {
            Shoot();
            timeUntilNextShot = 3.0f;
        }
    }

    void Shoot() {
        Vector3 shotOriginOffset = new Vector3(0, -(bc.size.y / 2 + 0.5f), 0);
        GameObject iProjectile = Instantiate(projectile, transform.position + shotOriginOffset, Quaternion.identity);
        iProjectile.GetComponent<EnemyBossProjectile>().speed = projectileSpeed;
    }

    public void TakeDamage() {
        EnemyBossSpawner.Instance.currentlySpawning = true;
        if (Random.value > 0.5f) {
            Instantiate(movementPickup, transform.position, Quaternion.identity);
        }
        else {
            Instantiate(attackPickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Boundary")) {
            EnemyBossSpawner.Instance.currentlySpawning = true;
            Destroy(gameObject);
        }
    }


}
