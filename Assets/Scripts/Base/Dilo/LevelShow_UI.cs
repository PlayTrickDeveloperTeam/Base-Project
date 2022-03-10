using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using TMPro;
using UnityEngine;

public class LevelShow_UI : MonoBehaviour
{
    private TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        SetLevel();
        B_CES_CentralEventSystem.OnAfterLevelLoaded.AddFunction(SetLevel,false);
    }

    public void SetLevel()
    {
        levelText.text = "LEVEL " + 
                         SaveSystem.GetDataInt(Enum_Saves.MainSave,Enum_MainSave.PreviewLevel);
    }
}
