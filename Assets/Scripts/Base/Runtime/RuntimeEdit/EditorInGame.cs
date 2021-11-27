using System;
using Base;
using UnityEngine;
public class EditorInGame : MonoBehaviour {
    #if UNITY_EDITOR
    private GUIStyle myButtonStyle;
    private Rect turnButton;
    private GUIStyle mainTitleText, titleText;
    private string turnStat = "X";
    private bool time = true;
    private string timeText = "STOP GAME";
    private string gameStat = "ON START";

    private void Start() {
        Setup();
    }
    void Setup() {
        turnButton = new Rect(900, 275, 100, 100);
        mainTitleText = new GUIStyle();
        mainTitleText.fontSize = 36;
        mainTitleText.normal.textColor = Color.red;
        titleText = new GUIStyle();
        titleText.fontSize = 24;
        titleText.normal.textColor = Color.white;
    }

    private bool open = true;


    private void OnGUI() {
        if (open) {
            myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 30;

            GUI.Box(new Rect(0, 100, Screen.width, Screen.height / 8), "");

            GUI.Label(new Rect(0, 100, 200, 50), "MENU", mainTitleText);
            GUI.Label(new Rect(0, 135, 200, 50), "CHANGE GAME STAT", titleText);


            if (GUI.Button(new Rect(0, 175, 200, 50), "On Start", myButtonStyle)) {
                B_GM_GameManager.instance.CurrentGameState = GameStates.Start;
                gameStat = "ON START";
            }

            if (GUI.Button(new Rect(225, 175, 200, 50), "On Playing", myButtonStyle)) {
                B_GM_GameManager.instance.CurrentGameState = GameStates.Playing;
                gameStat = "ON PLAYING";
            }

            if (GUI.Button(new Rect(450, 175, 200, 50), "On Pause", myButtonStyle)) {
                B_GM_GameManager.instance.CurrentGameState = GameStates.Paused;
                gameStat = "ON PAUSE";
            }

            if (GUI.Button(new Rect(675, 175, 200, 50), "On Init", myButtonStyle)) {
                B_GM_GameManager.instance.CurrentGameState = GameStates.Init;
                gameStat = "ON INIT";
            }

            if (GUI.Button(new Rect(900, 175, 200, 50), "On End", myButtonStyle)) {
                B_GM_GameManager.instance.CurrentGameState = GameStates.End;
                gameStat = "ON END";
            }

            GUI.Label(new Rect(0, 235, 200, 50), "CHANGE", titleText);

            if (GUI.Button(new Rect(0, 275, 200, 50), timeText, myButtonStyle)) {
                time = !time;

                if (!time) {
                    timeText = "START GAME";
                    Time.timeScale = 0;
                }
                else {
                    Time.timeScale = 1;
                    timeText = "STOP GAME";
                }
            }

            if (GUI.Button(new Rect(225, 275, 200, 50), "RES LEVEL", myButtonStyle)) B_LC_LevelManager.instance.ReloadCurrentLevel();

            GUI.Label(new Rect(225, 235, 200, 50), "GAME STAT: " + gameStat, titleText);

            if (GUI.Button(new Rect(450, 275, 200, 50), "F. GAME(T)", myButtonStyle)) B_GM_GameManager.instance.ActivateEndgame(true);

            if (GUI.Button(new Rect(675, 275, 200, 50), "F. GAME(F)", myButtonStyle)) B_GM_GameManager.instance.ActivateEndgame(false);
        }

        if (GUI.Button(turnButton, turnStat, myButtonStyle)) {
            open = !open;

            if (!open) {
                turnButton = new Rect(0, 0, 100, 100);
                turnStat = "O";
            }
            else {
                turnButton = new Rect(900, 275, 100, 100);
                turnStat = "X";
            }
        }
    }
    
    #endif
}