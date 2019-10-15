using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float horizontalDistanceLimit = 10.0f;
    private float horizontalInput;
    private Rigidbody2D rb;

    public GameObject projectile;
    public float shotInterval = 0.7f;
    public float projectileSpeed = 4.0f;
    private float timeSinceShot;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timeSinceShot = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovementInput();
        ProcessShooting();
    }

    void ProcessMovementInput()
    {
        float moveValue;
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            moveValue = horizontalInput * moveSpeed * Time.deltaTime;

            if (!(transform.position.x + moveValue >= horizontalDistanceLimit || transform.position.x + moveValue <= -horizontalDistanceLimit))
            {
                transform.Translate(new Vector3(moveValue, 0, 0));
            }
        } else
        {

        }
        
    }

    void ProcessShooting()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (timeSinceShot > shotInterval)
            {
                Shoot();
                timeSinceShot = 0;
            }
        }
        timeSinceShot += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject iProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        iProjectile.GetComponent<Projectile>().speed = projectileSpeed;

    }

}

/*
 *  - Player movement easing
 *  - Enemy group movement algorithm + implementation
 *  - 3 enemy types (basic, tank, shooter)
 */
