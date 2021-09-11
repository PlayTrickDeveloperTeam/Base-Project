using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Base
{
    public class B_BL_BootLoader : MonoBehaviour
    {
        public bool HasTutorial = false;
        private void Start()
        {
            B_GM_GameManager.instance.CurrentGameState = GameStates.Init;

#if UNITY_IOS

            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();


            }
            else
            {
                Application.Quit();
            }
#else

#endif
            StartCoroutine(InitiateBootLoading());
        }

        IEnumerator InitiateBootLoading()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            //Debug.unityLogger.logEnabled = false;
#endif
            B_CES_CentralEventSystem.CentralEventSystemStrapping();

            yield return new WaitUntil(() => B_GM_GameManager.instance.GameManagerStrapping() == true);

            if (!HasTutorial) B_GM_GameManager.instance.Save.TutorialPlayed = 1;

            B_CR_CoroutineRunner.instance.CoroutineRunnerStrapping();

            yield return new WaitUntil(() => B_LC_LevelManager.instance.StrappingLevelController() == true);

            yield return new WaitUntil(() => B_MM_MenuManager_Project.instance.Strapper_MenuManager() == true);

            B_LC_LevelManager.instance.LoadInLevel(B_GM_GameManager.instance.Save.PlayerLevel);

            B_MM_MenuManager_Project.instance.Panel_Loading.SetActive(false);

            B_GM_GameManager.instance.CurrentGameState = GameStates.Start;


        }

        #region Odin Inspector


        #endregion
    }
}