using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Base
{
    public class B_MM_MenuManager_Base : MonoBehaviour
    {
        public Dictionary<string, BMM_Panel> PanelDictionary;

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

        private List<GameObject> allPanels;

        private List<GameObject> activatedPanels()
        {
            List<GameObject> _temp = new List<GameObject>();
            foreach (var item in allPanels)
            {
                if (!item.activeInHierarchy) continue;
                _temp.Add(item);
            }
            return _temp;
        }

        private List<GameObject> deactivatedPanels()
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
            PanelDictionary = new Dictionary<string, BMM_Panel>();

            allPanels = new List<GameObject>();

            Panel_Loading = GetPanel(B_Database_String.Panel_Loading);
            Panel_Start = GetPanel(B_Database_String.Panel_Start);
            Panel_Settings = GetPanel(B_Database_String.Panel_Settings);
            Panel_Ending = GetPanel(B_Database_String.Panel_Ending);
            Panel_Ingame = GetPanel(B_Database_String.Panel_Ingame);

            allPanels.Add(Panel_Loading);
            allPanels.Add(Panel_Start);
            allPanels.Add(Panel_Settings);
            allPanels.Add(Panel_Ending);
            allPanels.Add(Panel_Ingame);

            BG_Ending_Fail = GetPanel(B_Database_String.BG_Ending_Fail);
            BG_Ending_Success = GetPanel(B_Database_String.BG_Ending_Success);

            Btn_M_Start = GetButton(B_Database_String.BTN_M_Start);
            Btn_M_Settings = GetButton(B_Database_String.BTN_M_Settings);
            Btn_Ig_Restart = GetButton(B_Database_String.BTN_IG_Restart);
            Btn_Ig_Menu = GetButton(B_Database_String.BTN_IG_Menu);
            Btn_Ig_Pause = GetButton(B_Database_String.BTN_IG_Pause);
            Btn_Ig_WatchAdd = GetButton(B_Database_String.BTN_IG_WatchAdd);
            Btn_Ig_ClaimReward = GetButton(B_Database_String.BTN_IG_ClaimReward);
            Btn_Ig_End = GetButton(B_Database_String.BTN_IG_End);
        }

        protected void StrappingFinal()
        {
            DeactivateAllPanels();
            ActivatePanel(B_Database_String.Panel_Start);
        }

        public Button GetButton(string buttonName)
        {
            if (GameObject.Find(buttonName) == null) return null;
            return GameObject.Find(buttonName).GetComponent<Button>();
        }

        public GameObject GetPanel(string panelName)
        {
            if (GameObject.Find(panelName) == null) return null;
            GameObject obj = GameObject.Find(panelName);
            PanelDictionary.Add(panelName, new BMM_Panel(obj, false));
            return obj;
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
                ActivatePanel(item.name);
            }
        }

        public void DeactivateAllPanels()
        {
            foreach (var item in activatedPanels())
            {
                DeactivatePanel(item.name);
            }
        }

        public void ActivateEndGame(float secondsToWait, bool success)
        {
            B_GM_GameManager.instance.CurrentGameState = GameStates.End;
            DeactivateAllPanels();
            StartCoroutine(Ienum_EndGameActivation(secondsToWait, success));
        }

        private IEnumerator Ienum_EndGameActivation(float secondsToWait, bool success)
        {
            yield return new WaitForSeconds(secondsToWait);
            Panel_Ending.SetActive(true);
            switch (success)
            {
                case true:
                    BG_Ending_Fail.SetActive(false);
                    BG_Ending_Success.SetActive(true);
                    B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.InvokeEvent();
                    break;

                case false:
                    BG_Ending_Success.SetActive(false);
                    BG_Ending_Fail.SetActive(true);
                    B_CES_CentralEventSystem.OnBeforeLevelDisableNegative.InvokeEvent();
                    break;
            }
        }

        public void ActivatePanel(string panelName)
        {
            PanelDictionary[panelName].Panel.SetActive(true);
            PanelDictionary[panelName].IsActive = true;
        }

        public void DeactivatePanel(string panelName)
        {
            PanelDictionary[panelName].IsActive = false;
            PanelDictionary[panelName].Panel.SetActive(false);
        }
    }

    [System.Serializable]
    public class BMM_Panel
    {
        public GameObject Panel;
        public bool IsActive;

        public BMM_Panel(GameObject panel, bool isactive)
        {
            this.Panel = panel;
            this.IsActive = isactive;
        }
    }
}