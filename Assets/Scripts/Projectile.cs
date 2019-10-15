using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool upDirection = true;
    public float speed = 3.0f;
    public float lifetime = 3.0f;

    private Rigidbody2D rb;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (upDirection == true) rb.velocity = new Vector2(0, speed);
        else rb.velocity = new Vector2(0, -speed);

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            col.gameObject.GetComponent<Enemy>().TakeDamage();
            Destroy(gameObject);
        }
            
        

    }
}
