using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Main
{
    public class M_MenuManager_1 : M_Base_MenuManager
    {
        public static M_MenuManager_1 instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public override bool Strapper_MenuManager()
        {
            base.Strapper_MenuManager();
            AddFunction(Btn_M_Start, BTN_FUNC_Start);
            AddFunction(Btn_Ig_End, BTN_FUNC_Endgame);
            AddFunction(Btn_Ig_Restart, BTN_FUNC_Restart);
            return true;
        }

        #region Button Functions

        void BTN_FUNC_Start()
        {
            M_levelController.instance.CurrentLevelFunctions.OnLevelCommand();
            M_GameManager.instance.CurrentGameState = GameStates.Playing;
            Panel_Start.SetActive(false);
            Panel_Ingame.SetActive(true);
        }

        void BTN_FUNC_Restart()
        {
            M_levelController.instance.ReloadCurrentLevel();
            DeactivateAllPanels();
            Panel_Start.SetActive(true);
        }

        void BTN_FUNC_Endgame()
        {
            DeactivateAllPanels();
            M_GameManager.instance.CurrentGameState = GameStates.Start;
            Panel_Start.SetActive(true);
            M_levelController.instance.LoadInNextLevel();
        }

        #endregion

        private void OnDisable()
        {
            instance = null;
        }
    }

}