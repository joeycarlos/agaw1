using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour {
    private static GameplayUIManager _instance;

    // Complete list of UI elements
    public Text titleText;
    public Text scoreText;
    public Text elapsedTime;
    public Text attackLevel;
    public Text speedLevel;

    public static GameplayUIManager Instance {
        get {
            if (_instance == null) {
                GameObject go = new GameObject("GameplayUIManager");
                go.AddComponent<GameplayUIManager>();
            }

            return _instance;
        }
    }

    void Awake() {
        _instance = this;
    }

    void Update() {
        UpdateTime();
    }

    public void UpdateScore() {
        scoreText.text = "SCORE: " + GameManager.Instance.Score;
    }

    public void UpdateTime() {
        elapsedTime.text = "TIME: " + GameManager.Instance.elapsedTimeThisLevel.ToString("F2");
    }

    public void UpdateAttackLevel() {
        attackLevel.text = "ATK LVL: " + GameManager.Instance.AttackLevel;
    }

    public void UpdateSpeedLevel() {
        speedLevel.text = "SPD LVL: " + GameManager.Instance.SpeedLevel;
    }
}
