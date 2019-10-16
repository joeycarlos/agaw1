using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float speed = 3.0f;
    public bool leftToRight { get; set; }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Move();
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

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Boundary")) {
            EnemyBossSpawner.Instance.currentlySpawning = true;
            Destroy(gameObject);
        }
    }

    public void TakeDamage() {
        EnemyBossSpawner.Instance.currentlySpawning = true;
        Destroy(gameObject);
    }
}
