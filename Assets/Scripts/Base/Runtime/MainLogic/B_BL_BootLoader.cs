using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.UI;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif
using Unity.Advertisement.IosSupport;
#if UNITY_IOS
#endif

namespace Base {
    [DefaultExecutionOrder(-100)]
    public class B_BL_BootLoader : MonoBehaviour {

        #region Spesific Functions

        private async void InitiateBootLoading() {
#if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
            await B_CES_CentralEventSystem.CentralEventSystemStrapping();
            for (var i = 0; i < Managers.Count; i++) await Managers[i].ManagerStrapping();
            if (!HasTutorial) SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.TutorialPlayed, 1);
            await VfmEffectsManager.VFXManagerStrapping();
            await EffectsManager.EffectsManagerStrapping();

            B_GM_GameManager.instance.CurrentGameState = GameStates.Start;

            B_LC_LevelManager.instance.LoadInLevel(SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.PlayerLevel));
            B_GM_GameManager.instance.Save.SaveAllData();
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main, .2f);
            
            IngameEditor = GetComponent<EditorInGame>();
            IngameEditor.enabled = RuntimeEditor;
        }

        IEnumerator Fucker() {
#if UNITY_IOS
                if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED || ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.RESTRICTED || ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.DENIED) {
                yield return new WaitForSeconds(1.2f);
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
#endif
            yield return new WaitForSeconds(.1f);
            InitiateBootLoading();
        }

        #endregion

#if UNITY_EDITOR
        #region Editor Functions

        [Button]
        public void SetupManagerEnums() {
            var names = new string[Managers.Count];
            for (var i = 0; i < Managers.Count; i++) names[i] = Managers[i].GetType().Name;
            EnumCreator.CreateEnum("Managers", names);
        }

        #endregion
#endif

        #region Properties

        [SerializeField] private bool HasTutorial;
        [SerializeField] private List<B_M_ManagerBase> Managers;
        [SerializeField] private B_VFM_EffectsManager VfmEffectsManager; 
        private EditorInGame IngameEditor;
        [SerializeField] private bool RuntimeEditor;

        #endregion

        #region Unity Functions

        private void Awake() {
            StartCoroutine(Fucker());
            
        }

        private void OnDisable() {
            for (var i = 0; i < Managers.Count; i++) Managers[i].ManagerDataFlush();
        }

        #endregion
    }
}