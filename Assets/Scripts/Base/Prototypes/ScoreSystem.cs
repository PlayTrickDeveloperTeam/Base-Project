using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
public class ScoreSystem : MonoBehaviour {

    public static ScoreSystem instance;

    [OnValueChanged("Test")]
    [SerializeField] private bool combo;

    private ComboSystem comboSystem;

    public Action<int> ScoreAdd;
    public int TotalScore { get; }

    public int Score { get; private set; }



    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable() {
        ScoreAdd += AddScore;
    }
    private void OnDisable() {
        ScoreAdd -= AddScore;
    }


    #if UNITY_EDITOR

    private void Test() {
        if (combo) {
            comboSystem = gameObject.AddComponent<ComboSystem>();
        }
        else {
            var get = GetComponent<ComboSystem>();
            if (get != null) DestroyImmediate(get);
        }
    }

    #endif
    private void AddScore(int value) {
        Score += value;
        if (combo) comboSystem.ComboAdd.Invoke(1);
        //Add text change
    }
}

public class ComboSystem : MonoBehaviour {
    public static ComboSystem instance;
    [SerializeField] private float addingComboTime;
    [SerializeField] private bool perfectCombo;
    [ShowIf("perfectCombo")]
    [SerializeField] private int perfectCount;
    [ShowIf("perfectCombo")]
    [SerializeField] private int perfectTime;

    public Action<int> ComboAdd;

    private Coroutine ComboCounterRoutine;

    private float comboTime;
    public Action OverrideStopCombo;
    public int TotalCombo { get; private set; }

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable() {
        OverrideStopCombo += () => StopCoroutine(ComboCounterRoutine);
    }

    private void OnDisable() {
        OverrideStopCombo -= () => StopCoroutine(ComboCounterRoutine);
    }

    private void AddCombo(int value) {
        if (TotalCombo == 0) ComboCounterRoutine = StartCoroutine(ComboCounter());
        else if (comboTime > 0) comboTime += addingComboTime;

        TotalCombo += value;

        if (perfectCombo)
            if ((TotalCombo % perfectCount == 0) & (TotalCombo != 0)) {
                comboTime += perfectTime;
                TotalCombo += 2;
            }
    }

    private IEnumerator ComboCounter() {
        while (comboTime > 0) {
            comboTime -= 0.1f;
            yield return new WaitForFixedUpdate();
        }

        comboTime = 0;
        TotalCombo = 0;
    }
}