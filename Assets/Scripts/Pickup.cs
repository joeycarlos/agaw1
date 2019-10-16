using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float speed = 3.0f;
    public float lifetime = 8.0f;

    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    void Move() {
        Vector3 moveVector = new Vector3(0, -speed * Time.deltaTime, 0);
        transform.Translate(moveVector);
    }

}
