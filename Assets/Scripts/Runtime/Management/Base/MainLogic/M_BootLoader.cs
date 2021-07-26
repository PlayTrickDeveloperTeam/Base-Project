using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Main
{
    public class M_BootLoader : MonoBehaviour
    {
        public bool FacebookSdk = false;
        public GameObject GameAnaltyicsObject;
        private void Start()
        {
            M_GameManager.instance.CurrentGameState = GameStates.Init;

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
            yield return new WaitUntil(() => M_GameManager.instance.GameManagerStrapping() == true);
            yield return new WaitUntil(() => M_levelController.instance.StrappingLevelController() == true);
            yield return new WaitUntil(() => M_MenuManager_1.instance.Strapper_MenuManager() == true);
            M_levelController.instance.LoadInLevel(M_GameManager.instance.MainSaveData.GetDataI(SE_DataTypes.PlayerLevel));
            M_MenuManager_1.instance.Panel_Loading.SetActive(false);
            M_GameManager.instance.CurrentGameState = GameStates.Start;
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