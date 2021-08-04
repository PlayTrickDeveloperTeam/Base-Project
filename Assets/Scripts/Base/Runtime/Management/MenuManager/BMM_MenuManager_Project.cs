using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Base
{
    public class BMM_MenuManager_Project : B_MM_MenuManager_Base
    {
        public static BMM_MenuManager_Project instance;
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
            //Inits all the base panels, buttons etc
            base.StrapperInit();
            AddFunction(Btn_M_Start, BTN_FUNC_Start);
            AddFunction(Btn_Ig_End, BTN_FUNC_Endgame);
            AddFunction(Btn_Ig_Restart, BTN_FUNC_Restart);
            //Closes all but start panel
            base.StrappingFinal();
            return true;
        }

        #region Button Functions

        void BTN_FUNC_Start()
        {
            B_LC_LevelManager.instance.CurrentLevelFunctions.OnLevelCommand();
            B_GM_GameManager.instance.CurrentGameState = GameStates.Playing;
            Panel_Start.SetActive(false);
            Panel_Ingame.SetActive(true);
            ActivatePanel(Database_String.Panel_Ingame, .5f);
            DeactivatePanel(Database_String.Panel_Start, .5f);
        }

        void BTN_FUNC_Restart()
        {
            B_LC_LevelManager.instance.ReloadCurrentLevel();
            DeactivateAllPanels();
            ActivatePanel(Database_String.Panel_Start, .5f);
        }

        void BTN_FUNC_Endgame()
        {
            DeactivateAllPanels();
            B_GM_GameManager.instance.CurrentGameState = GameStates.Start;
            ActivatePanel(Database_String.Panel_Start, .5f);
            B_LC_LevelManager.instance.LoadInNextLevel();
        }


        #endregion

        private void OnDisable()
        {
            instance = null;
        }
    }

}