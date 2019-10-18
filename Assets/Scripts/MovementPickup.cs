using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPickup : Pickup {

    public float movementBonus = 4.0f;
    public int scoreValue = 30;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            col.GetComponent<Player>().MoveSpeed += movementBonus;
            GameManager.Instance.Score += scoreValue;
            GameManager.Instance.SpeedLevel++;
            GameplayUIManager.Instance.ScoreNotification(scoreValue, transform.position, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }
}