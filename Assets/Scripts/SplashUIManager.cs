using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashUIManager : MonoBehaviour
{
    private static SplashUIManager _instance;

    public static SplashUIManager Instance {
        get {
            return _instance;
        }
    }

    public Text gameTitle;
    public Text author;
    public Text date;
    public Text description;
    public Text context;

    void Awake() {
        _instance = this;
    }
}
