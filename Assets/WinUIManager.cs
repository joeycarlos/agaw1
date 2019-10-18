using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIManager : MonoBehaviour
{
    private static WinUIManager _instance;

    public static WinUIManager Instance {
        get {
            return _instance;
        }
    }

    public Text totalScoreValue;
    public Text totalTimeValue;

    void Awake() {
        _instance = this;
    }

    void Start() {
        totalScoreValue.text = GameManager.Instance.Score.ToString();
        totalTimeValue.text = GameManager.Instance.elapsedTimeTotal.ToString("F2");
    }
}
