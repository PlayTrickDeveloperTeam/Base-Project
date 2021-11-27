using Base;
using TMPro;
using UnityEngine;
public class ValueInputField : MonoBehaviour {
    private TextMeshProUGUI CurrentValueText;
    private TMP_InputField InputField;
    private Mfloat MFloatModfiyableValue;
    private TextMeshProUGUI ValueNameText;

    public void SetupValueInputField(Mfloat Value) {
        MFloatModfiyableValue = Value;
        InputField = GetComponentInChildren<TMP_InputField>();
        InputField.onEndEdit.AddListener(ChangeValue);
        InputField.onSelect.AddListener(StartEditing);
        CurrentValueText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ValueNameText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        CurrentValueText.text = Value.ConnectedValue.ToString("0.0");
        ValueNameText.text = Value.ConnectedName;

    }

    private void StartEditing(string value) {
        Time.timeScale = .1f;
    }

    private void ChangeValue(string Value) {
        MFloatModfiyableValue.ConnectedValue = Value.IsFloat();
        CurrentValueText.text = Value;
        Time.timeScale = 1;

    }
}