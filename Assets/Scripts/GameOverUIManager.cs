using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    private static GameOverUIManager _instance;

    public static GameOverUIManager Instance {
        get {
            return _instance;
        }
    }

    public Text totalScoreValue;
    public Text totalTimeValue;
    public Text levelReached;

    void Awake() {
        _instance = this;
    }

    void Start() {
        totalScoreValue.text = GameManager.Instance.Score.ToString();
        totalTimeValue.text = GameManager.Instance.elapsedTimeTotal.ToString("F2");
        levelReached.text = "Well, at least you reached level " + (GameManager.Instance.levelsCompleted + 1).ToString() + "... ";
    }
}
