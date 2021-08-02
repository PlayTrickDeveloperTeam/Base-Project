using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace Base
{
    public class B_MM_MenuManager_Base : MonoBehaviour
    {
        [HideInInspector] public GameObject Panel_Loading;
        [HideInInspector] public GameObject Panel_Start;
        [HideInInspector] public GameObject Panel_Settings;
        [HideInInspector] public GameObject Panel_Ending;
        [HideInInspector] public GameObject Panel_Ingame;

        [HideInInspector] public GameObject BG_Ending_Fail;
        [HideInInspector] public GameObject BG_Ending_Success;

        [HideInInspector] public Button Btn_M_Start;
        [HideInInspector] public Button Btn_M_Settings;
        [HideInInspector] public Button Btn_Ig_Restart;
        [HideInInspector] public Button Btn_Ig_Menu;
        [HideInInspector] public Button Btn_Ig_Pause;
        [HideInInspector] public Button Btn_Ig_WatchAdd;
        [HideInInspector] public Button Btn_Ig_ClaimReward;
        [HideInInspector] public Button Btn_Ig_End;

        List<GameObject> allPanels;
        List<GameObject> activatedPanels()
        {
            List<GameObject> _temp = new List<GameObject>();
            foreach (var item in allPanels)
            {
                if (!item.activeInHierarchy) continue;
                _temp.Add(item);
            }
            return _temp;
        }

        List<GameObject> deactivatedPanels()
        {
            List<GameObject> _temp = new List<GameObject>();
            foreach (var item in allPanels)
            {
                if (item.activeInHierarchy) continue;
                _temp.Add(item);
            }
            return _temp;
        }

        public virtual bool Strapper_MenuManager()
        {
            return true;
        }

        protected void StrapperInit()
        {
            allPanels = new List<GameObject>();

            Panel_Loading = GetPanel(Database_String.Panel_Loading);
            Panel_Start = GetPanel(Database_String.Panel_Start);
            Panel_Settings = GetPanel(Database_String.Panel_Settings);
            Panel_Ending = GetPanel(Database_String.Panel_Ending);
            Panel_Ingame = GetPanel(Database_String.Panel_Ingame);

            allPanels.Add(Panel_Loading);
            allPanels.Add(Panel_Start);
            allPanels.Add(Panel_Settings);
            allPanels.Add(Panel_Ending);
            allPanels.Add(Panel_Ingame);

            BG_Ending_Fail = GetPanel(Database_String.BG_Ending_Fail);
            BG_Ending_Success = GetPanel(Database_String.BG_Ending_Success);

            Btn_M_Start = GetButton(Database_String.BTN_M_Start);
            Btn_M_Settings = GetButton(Database_String.BTN_M_Settings);
            Btn_Ig_Restart = GetButton(Database_String.BTN_IG_Restart);
            Btn_Ig_Menu = GetButton(Database_String.BTN_IG_Menu);
            Btn_Ig_Pause = GetButton(Database_String.BTN_IG_Pause);
            Btn_Ig_WatchAdd = GetButton(Database_String.BTN_IG_WatchAdd);
            Btn_Ig_ClaimReward = GetButton(Database_String.BTN_IG_ClaimReward);
            Btn_Ig_End = GetButton(Database_String.BTN_IG_End);
        }

        protected void StrappingFinal()
        {
            Panel_Settings.SetActive(false);
            Panel_Ending.SetActive(false);
            Panel_Ingame.SetActive(false);
            Panel_Start.SetActive(true);
        }


        public Button GetButton(string buttonName)
        {
            if (GameObject.Find(buttonName) == null) return null;
            return GameObject.Find(buttonName).GetComponent<Button>();
        }

        public GameObject GetPanel(string panelName)
        {
            if (GameObject.Find(panelName) == null) return null;
            return GameObject.Find(panelName);
        }

        public void AddFunction(Button btnToAdd, UnityAction Function)
        {
            if (btnToAdd != null)
                btnToAdd.onClick.AddListener(Function);
        }

        public void ActivateAllPanels()
        {
            foreach (var item in deactivatedPanels())
            {
                item.SetActive(true);
            }
        }

        public void DeactivateAllPanels()
        {
            foreach (var item in activatedPanels())
            {
                item.SetActive(false);
            }
        }

        public void ActivateEndGame(float secondsToWait, bool success)
        {
            B_GM_GameManager.instance.CurrentGameState = GameStates.End;
            DeactivateAllPanels();
            StartCoroutine(Ienum_EndGameActivation(secondsToWait, success));
        }

        IEnumerator Ienum_EndGameActivation(float secondsToWait, bool success)
        {
            yield return new WaitForSeconds(secondsToWait);
            Panel_Ending.SetActive(true);
            switch (success)
            {
                case true:
                    BG_Ending_Fail.SetActive(false);
                    BG_Ending_Success.SetActive(true);
                    break;
                case false:
                    BG_Ending_Success.SetActive(false);
                    BG_Ending_Fail.SetActive(true);
                    break;
            }
        }

    }

    [System.Serializable]
    public class BMM_Panel
    {

    }
}
