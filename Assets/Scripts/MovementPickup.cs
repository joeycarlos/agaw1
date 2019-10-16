using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPickup : Pickup {

    public float movementBonus = 4.0f;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            col.GetComponent<Player>().moveSpeed += movementBonus;
            Destroy(gameObject);
        }
    }
}