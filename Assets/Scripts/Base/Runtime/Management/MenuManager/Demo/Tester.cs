using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System.Threading.Tasks;
using UnityEngine.UI;
using Base;
public class Tester : MonoBehaviour
{
    private void Start()
    {
        B_CES_CentralEventSystem.OnGameStateChange.AddFunction(ChangeText, false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            B_GM_GameManager.instance.ActivateEndgame(true, 2f);
        }
    }

    void ChangeText()
    {
        GUIManager.GetText(Enum_Menu_MainComponent.GameStateShowcase).ChangeText(B_GM_GameManager.instance.CurrentGameState.ToString());
        GUIManager.GetText(Enum_Menu_PlayerOverlayComponent.GameStateShowcase).ChangeText(B_GM_GameManager.instance.CurrentGameState.ToString());
        GUIManager.GetText(Enum_Menu_GameOverComponent.GameStateShowcase).ChangeText(B_GM_GameManager.instance.CurrentGameState.ToString());
    }
}
