using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Base
{
    public class B_BL_BootLoader : MonoBehaviour
    {
        public bool FacebookSdk = false;
        public bool HasTutorial = false;
        public GameObject GameAnaltyicsObject;
        private void Start()
        {
            B_GM_GameManager.instance.CurrentGameState = GameStates.Init;

#if UNITY_IOS

            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();

                GameAnalytics.SetActive(true);
                if (FacebookSdk)
                {
                    FB.Init();
                }
            }
            else
            {
                GameAnalytics.SetActive(false);
                //if (FacebookSdk)
                //{
                //    FB.Init();
                //}
            }
#else

            if (FacebookSdk)
            {
                GameAnaltyicsObject.SetActive(true);
                FB.Init();
            }
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
            if (!HasTutorial) B_GM_GameManager.instance.MainSaveData.SetData(B_SE_DataTypes.TutorialPlayed, 1);
            yield return new WaitUntil(() => B_LC_LevelManager.instance.StrappingLevelController() == true);
            yield return new WaitUntil(() => BMM_MenuManager_Project.instance.Strapper_MenuManager() == true);
            B_LC_LevelManager.instance.LoadInLevel(B_GM_GameManager.instance.MainSaveData.GetDataI(B_SE_DataTypes.PlayerLevel));
            BMM_MenuManager_Project.instance.Panel_Loading.SetActive(false);
            B_GM_GameManager.instance.CurrentGameState = GameStates.Start;

        }

        #region Odin Inspector

        bool IsGameObjectSet()
        {
            if (GameAnaltyicsObject == null) return true;
            return false;
        }

        #endregion
    }
}