using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : MonoBehaviour
{
    private static LevelCompleteUI _instance;

    public static LevelCompleteUI Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        _instance = this;
    }

    void Start() {
        levelCompleteText.text = "LEVEL " + GameManager.Instance.levelsCompleted.ToString() + " COMPLETE!";
        totalScoreValue.text = GameManager.Instance.Score.ToString();
        levelTimeValue.text = GameManager.Instance.elapsedTimeThisLevel.ToString("F2");
        totalTimeValue.text = GameManager.Instance.elapsedTimeTotal.ToString("F2");
        timeBonusValue.text = "+ " + GameManager.Instance.timeBonus.ToString("F0");
        StartCoroutine("AddTimeBonusAnimation");
    }

    public Text levelCompleteText;
    public Text totalScoreValue;
    public Text levelTimeValue;
    public Text totalTimeValue;
    public Text timeBonusValue;

    public bool doneAnimation = false;

    IEnumerator AddTimeBonusAnimation() {
        int startingValue = GameManager.Instance.Score;
        int endingValue = GameManager.Instance.scoreWithBonus;
        int diff = endingValue - startingValue;
        for (int i = 0; i <= diff; i++) {
            totalScoreValue.text = (startingValue + i).ToString();
            timeBonusValue.text = "+ " + (diff - i).ToString();
            yield return new WaitForSeconds(.025f);
        }

        doneAnimation = true;
        yield return null;
    }

    public void SkipAnimation() {
        StopCoroutine("AddTimeBonusAnimation");
        totalScoreValue.text = GameManager.Instance.scoreWithBonus.ToString();
        timeBonusValue.text = "+ 0";
        doneAnimation = true;
    }

}
