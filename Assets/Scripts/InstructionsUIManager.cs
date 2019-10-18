using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsUIManager : MonoBehaviour
{
    private static InstructionsUIManager _instance;

    public static InstructionsUIManager Instance {
        get {
            return _instance;
        }
    }

    void Awake() {
        _instance = this;
    }
}
