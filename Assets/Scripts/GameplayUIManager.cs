using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour {
    private static GameplayUIManager _instance;

    // Complete list of UI elements
    public Text titleText;
    public Text levelText;
    public Text scoreText;
    public Text elapsedTime;
    public Text attackLevel;
    public Text speedLevel;

    public Text attackUpAnnouncement;
    public Text speedUpAnnouncement;

    public Canvas scoreNotification;

    public Text levelStartText;

    public static GameplayUIManager Instance {
        get {
            /*
            if (_instance == null) {
                GameObject go = new GameObject("GameplayUIManager");
                go.AddComponent<GameplayUIManager>();
            }
            */

            return _instance;
        }
    }

    void Awake() {
        _instance = this;
    }

    void Update() {
        UpdateLevel();
        UpdateTime();
    }

    public void UpdateLevel() {
        levelText.text = "LEVEL: " + (GameManager.Instance.levelsCompleted + 1).ToString();
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

    public void AttackUpAnnouncement() {
        Text iText = Instantiate(attackUpAnnouncement, transform, false);
        iText.rectTransform.anchoredPosition = new Vector2(0, -200.0f);
        StartCoroutine(AnnouncementAnimation(iText, 2.0f, 10.0f));
        Destroy(iText.gameObject, 2.0f);
    }

    public void SpeedUpAnnouncement() {
        Text iText = Instantiate(speedUpAnnouncement, transform, false);
        iText.rectTransform.anchoredPosition = new Vector2(0, -200.0f);
        StartCoroutine(AnnouncementAnimation(iText, 2.0f, 10.0f));
        Destroy(iText.gameObject, 2.0f);
    }

    public void ScoreNotification(int score, Vector3 location, Vector3 offset) {
        Canvas iCanvas = Instantiate(scoreNotification, location, Quaternion.identity);
        iCanvas.GetComponentInChildren<Text>().text = "+" + score.ToString();
        StartCoroutine(NotificationAnimation(iCanvas, 0.75f, 3.0f));
        Destroy(iCanvas.gameObject, 0.75f);
    }

    IEnumerator AnnouncementAnimation(Text iText, float lifetime, float speed) {
        for (float t = 0; t < lifetime - 0.01f; t += Time.deltaTime) {
            iText.rectTransform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            Color color = iText.color;
            color.a -= Time.deltaTime / lifetime;
            iText.color = color;

            yield return null;
        }
    }

    IEnumerator NotificationAnimation(Canvas iCanvas, float lifetime, float speed) {
        for (float t = 0; t < lifetime - 0.05f; t += Time.deltaTime) {
            iCanvas.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

            Text iText = iCanvas.GetComponentInChildren<Text>();

            Color color = iText.color;
            color.a -= Time.deltaTime / lifetime;
            iText.color = color;

            yield return null;
        }
    }

    public void RemoveLevelStartMessage() {
        Destroy(levelStartText);
    }
}
