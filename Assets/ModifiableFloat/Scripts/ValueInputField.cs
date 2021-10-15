using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Base;

public class ValueInputField : MonoBehaviour
{
    Mfloat MFloatModfiyableValue;
    TMP_InputField InputField;
    TextMeshProUGUI ValueNameText;
    TextMeshProUGUI CurrentValueText;

    public void SetupValueInputField(Mfloat Value)
    {
        this.MFloatModfiyableValue = Value;
        InputField = GetComponentInChildren<TMP_InputField>();
        InputField.onEndEdit.AddListener(ChangeValue);
        InputField.onSelect.AddListener(StartEditing);
        CurrentValueText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ValueNameText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        CurrentValueText.text = Value.ConnectedValue.ToString("0.0");
        ValueNameText.text = Value.ConnectedName;

    }

    void StartEditing(string value)
    {
        Time.timeScale = .1f;
    }

    void ChangeValue(string Value)
    {
        MFloatModfiyableValue.ConnectedValue = Value.IsFloat();
        CurrentValueText.text = Value;
        Time.timeScale = 1;

    }
}
