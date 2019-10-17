using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float _moveSpeed { get; set; }
    public float _shotInterval { get; set; }
    public float _projectileSpeed { get; set; }

    public PlayerData(float moveSpeed, float shotInterval, float projectileSpeed) {
        _moveSpeed = moveSpeed;
        _shotInterval = shotInterval;
        _projectileSpeed = projectileSpeed;
    }
}
