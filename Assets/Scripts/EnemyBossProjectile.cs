using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossProjectile : MonoBehaviour
{
    public bool upDirection = false;
    public float speed = 3.0f;
    public float lifetime = 5.0f;

    private Rigidbody2D rb;
    private float timeAlive;

    public GameObject landHitEffect;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update() {
        timeAlive += Time.deltaTime;
        Move();
    }

    private void Move() {
        Vector3 moveVector;
        moveVector = new Vector3(0.05f*Mathf.Sin(timeAlive*10.0f), -speed * Time.deltaTime, 0);
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            GameManager.Instance.GameOver();
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("PickupDestroy")) {
            GameObject iLandHitEffect = Instantiate(landHitEffect, transform.position, Quaternion.identity);
            Destroy(iLandHitEffect, 1.0f);
            Destroy(gameObject);
        }
    }
}
