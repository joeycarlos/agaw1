using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : Pickup
{
    public float shotIntervalDecrease = 0.5f;
    public float projectileSpeedIncrease = 1.0f;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            col.GetComponent<Player>().shotInterval -= shotIntervalDecrease;
            col.GetComponent<Player>().projectileSpeed += projectileSpeedIncrease;
            Destroy(gameObject);
        }
    }
}
