using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float moveSpeed = 5.0f;

    public float MoveSpeed {
        get {
            return moveSpeed; }
        set {
            moveSpeed = value;
            moveSpeed = Mathf.Clamp(moveSpeed, 1.0f, 30.0f);
        }
    }

    public float horizontalDistanceLimit = 10.0f;
    private float horizontalInput;
    private Rigidbody2D rb;

    public GameObject projectile;

    private float shotInterval = 0.7f;
    public float ShotInterval {
        get {
            return shotInterval;
        }
        set {
            shotInterval = value;
            shotInterval = Mathf.Clamp(shotInterval, 0.05f, 30.0f);
        }
    }

    private float projectileSpeed = 4.0f;
    public float ProjectileSpeed {
        get {
            return projectileSpeed;
        }
        set {
            projectileSpeed = value;
            projectileSpeed = Mathf.Clamp(projectileSpeed, 1.0f, 30.0f);
        }
    }

    private float timeSinceShot;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timeSinceShot = Mathf.Infinity;

        GameManager.Instance.LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.levelHasStarted == true) {
            ProcessMovementInput();
            ProcessShooting();
        }
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