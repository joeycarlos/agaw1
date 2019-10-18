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

    public Text attackUpAnnouncement;
    public Text speedUpAnnouncement;

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

    public void AttackUpAnnouncement() {
        Text iText = Instantiate(attackUpAnnouncement, transform, false);
        iText.rectTransform.anchoredPosition = new Vector2(0, -200.0f);
        StartCoroutine(AnnouncementAnimation(iText, 2.0f, 5.0f));
        Destroy(iText, 1.0f);
    }

    public void SpeedUpAnnouncement() {
        Text iText = Instantiate(speedUpAnnouncement, transform, false);
        iText.rectTransform.anchoredPosition = new Vector2(0, -200.0f);
        StartCoroutine(AnnouncementAnimation(iText, 2.0f, 5.0f));
        Destroy(iText, 1.0f);
    }

    IEnumerator AnnouncementAnimation(Text iText, float lifetime, float speed) {
        for (float t = 0; t < lifetime; t += Time.deltaTime) {
            iText.rectTransform.Translate(new Vector3(0, speed * Time.deltaTime, 0));

            yield return null;
        }
    }
}
