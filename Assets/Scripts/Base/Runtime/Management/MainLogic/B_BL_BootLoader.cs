using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Base.UI;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace Base
{
    [DefaultExecutionOrder(-100)]
    public class B_BL_BootLoader : MonoBehaviour
    {

        #region Properties

        public bool HasTutorial = false;
        public List<B_M_ManagerBase> Managers;
        #endregion

        #region Unity Functions

        private void Awake()
        {

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
            InitiateBootLoading();
        }

        private void OnDisable()
        {
            for (int i = 0; i < Managers.Count; i++)
            {
                Managers[i].ManagerDataFlush();
            }
        }


        #endregion

        #region Spesific Functions

        private async void InitiateBootLoading()
        {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            //Debug.unityLogger.logEnabled = false;
#endif
            await B_CES_CentralEventSystem.CentralEventSystemStrapping();
            for (int i = 0; i < Managers.Count; i++)
            {
                await Managers[i].ManagerStrapping();
            }
            if (!HasTutorial) B_GM_GameManager.instance.Save.TutorialPlayed = 1;
            await Task.Delay(5000);
            B_MM_MenuManager_Project.instance.Panel_Loading.SetActive(false);

        }

        #endregion

#if UNITY_EDITOR
        #region Editor Functions
        [Button]
        public void SetupManagerEnums()
        {
            string[] names = new string[Managers.Count];
            for (int i = 0; i < Managers.Count; i++)
            {
                names[i] = Managers[i].GetType().Name;
            }
            EnumCreator.CreateEnum("Managers", names);
        }
        #endregion
#endif
    }
}