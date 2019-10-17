using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public bool upDirection = false;
    public float speed = 3.0f;
    public float lifetime = 4.0f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        if (upDirection == true) rb.velocity = new Vector2(0, speed);
        else rb.velocity = new Vector2(0, -speed);

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            GameManager.Instance.GameOver();
        }
    }
}
