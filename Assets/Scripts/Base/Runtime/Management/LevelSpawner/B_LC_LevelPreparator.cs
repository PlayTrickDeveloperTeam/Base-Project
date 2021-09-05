using System.Collections;
using UnityEngine;

namespace Base
{
    public class B_LC_LevelPreparator : MonoBehaviour
    {
        private int levelCount;
        CoroutineQueue CQ;

        private void Awake()
        {
            CQ = new CoroutineQueue(this);
            CQ.StartLoop();
            B_CES_CentralEventSystem.OnAfterLevelLoaded.AddFunction(OnLevelInitate, false);
            B_CES_CentralEventSystem.OnLevelActivation.AddFunction(OnLevelCommand, false);

        }

        public void OnLevelInitate()
        {
            B_GM_GameManager.instance.Save.PlayerLevel = levelCount;
            Debug.Log("Level Loaded");
        }

        public void OnLevelCommand()
        {
            Debug.Log("Level Started");
        }

        //Delete this on project start
        private void Update()
        {
            if (!B_GM_GameManager.instance.IsGamePlaying()) return;
            if (Input.GetMouseButtonDown(0))
            {
                BMM_MenuManager_Project.instance.ActivateEndGame(.5f, true);
            }
        }


        private void OnDisable()
        {
            B_CES_CentralEventSystem.OnLevelDisable.InvokeEvent();
        }


    }
}