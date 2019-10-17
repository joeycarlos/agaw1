using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : Pickup
{
    public float shotIntervalDecrease = 0.5f;
    public float projectileSpeedIncrease = 1.0f;

    public int scoreValue = 30;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            col.GetComponent<Player>().ShotInterval -= shotIntervalDecrease;
            col.GetComponent<Player>().ProjectileSpeed += projectileSpeedIncrease;
            GameManager.Instance.Score += scoreValue;
            GameManager.Instance.AttackLevel++;
            Destroy(gameObject);
        }
    }
}
