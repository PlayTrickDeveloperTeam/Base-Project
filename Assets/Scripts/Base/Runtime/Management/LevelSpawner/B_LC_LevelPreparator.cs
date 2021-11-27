using UnityEngine;
namespace Base {
    public class B_LC_LevelPreparator : MonoBehaviour {
        private int levelCount;

        private void Awake() {
            B_CES_CentralEventSystem.OnAfterLevelLoaded.AddFunction(OnLevelInitate, false);
            B_CES_CentralEventSystem.OnLevelActivation.AddFunction(OnLevelCommand, false);
        }

        private void OnDisable() {
            B_CES_CentralEventSystem.OnLevelDisable.InvokeEvent();
        }

        public void OnLevelInitate() {
            //B_GM_GameManager.instance.Save.PlayerLevel = levelCount;
            SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PlayerLevel, levelCount);
            Debug.Log("Level Loaded");
        }

        public void OnLevelCommand() {
            Debug.Log("Level Started");
        }
    }
}