using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.UI;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Base {
    public enum LevelType { Tutorial, Main }

    public class B_LC_LevelManager : B_M_ManagerBase {
        public static B_LC_LevelManager instance;
        public static Transform ObjectSpawnParent;

        [HideInInspector] public List<GameObject> MainLevels;
        [HideInInspector] public List<GameObject> TutorialLevels;

        [HideInInspector] public GameObject CurrentLevel;

        [HideInInspector] public int CurrentLevelIndex;
        [HideInInspector] public int PreviewLevelIndex;
        private GameObject currentLevel;

        public Action<int> OnLevelChangedAction;

        [HideInInspector] public Transform LevelHolder { get; private set; }

        private int tutorialPlayed => SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.TutorialPlayed);

        private void OnDestroy() {
            instance = null;
            ObjectSpawnParent = null;
        }

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            LevelHolder = GameObject.Find("LevelHolder").GetComponent<Transform>();

            MainLevels = new List<GameObject>();
            TutorialLevels = new List<GameObject>();

            MainLevels = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_MainLevels).ToList();
            TutorialLevels = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_TutorialLevels).ToList();

            MainLevels = MainLevels.OrderBy(t => t.name).ToList();
            TutorialLevels = TutorialLevels.OrderBy(t => t.name).ToList();

            PreviewLevelIndex = SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.PreviewLevel);



            B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.AddFunction(SaveOnNextLevel, true);

            return base.ManagerStrapping();
        }

        public override Task ManagerDataFlush() {
            return base.ManagerDataFlush();
        }

        public void LoadInLevel(int levelNumber) {
            switch (tutorialPlayed) {
                case 0:
                    if (levelNumber >= TutorialLevels.Count) levelNumber = 0;
                    InitateNewLevel(TutorialLevels[levelNumber]);
                    break;

                case 1:
                    if (levelNumber >= MainLevels.Count) levelNumber = 0;
                    InitateNewLevel(MainLevels[levelNumber]);
                    break;
            }
        }

        public void LoadInNextLevel() {
            switch (SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.GameFinished)) {
                case 0:
                    InitateNewLevel(LevelToLoad());
                    break;

                case 1:
                    InitateNewLevel(RandomSelectedLevel());
                    break;
            }
        }

        public void ReloadCurrentLevel() {
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main);
            InitateNewLevel(currentLevel);
        }

        private void InitateNewLevel(GameObject levelToInit) {
            B_CES_CentralEventSystem.OnBeforeLevelLoaded.InvokeEvent();
            if (CurrentLevel != null) {
                Destroy(CurrentLevel);
                CurrentLevel = null;
                currentLevel = null;
            }
            CurrentLevel = Instantiate(levelToInit, LevelHolder);
            currentLevel = levelToInit;
            switch (tutorialPlayed) {
                case 0:
                    CurrentLevelIndex = Array.IndexOf(TutorialLevels.ToArray(), levelToInit);
                    SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PlayerLevel, CurrentLevelIndex);
                    SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PreviewLevel, CurrentLevelIndex);
                    //B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
                    //B_GM_GameManager.instance.Save.PreviewLevel = CurrentLevelIndex;
                    OnLevelChangedAction?.Invoke(CurrentLevelIndex);
                    break;

                case 1:
                    CurrentLevelIndex = Array.IndexOf(MainLevels.ToArray(), levelToInit);
                    SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PlayerLevel, CurrentLevelIndex);
                    //B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
                    OnLevelChangedAction?.Invoke(PreviewLevelIndex);
                    break;
            }
            B_CES_CentralEventSystem.OnAfterLevelLoaded.InvokeEvent();
            SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PlayerLevel, CurrentLevelIndex);
            //B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
            ObjectSpawnParent = LevelHolder.GetChild(0);
        }

        private GameObject LevelToLoad() {
            switch (tutorialPlayed) {
                case 0:
                    if (CurrentLevelIndex + 1 >= TutorialLevels.Count) {
                        CurrentLevelIndex = 0;
                        SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.TutorialPlayed, 1);
                        //SaveSystem.SetData(Enum_Saves.Save_1, Enum_Save_1.TutorialPlayed, );
                        //B_GM_GameManager.instance.Save.TutorialPlayed = 1;
                        return MainLevels[0];
                    }
                    return TutorialLevels[CurrentLevelIndex + 1];

                case 1:
                    if (CurrentLevelIndex + 1 >= MainLevels.Count) {
                        SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.GameFinished, 1);
                        //B_GM_GameManager.instance.Save.GameFinished = 1;
                        return RandomSelectedLevel();
                    }
                    return MainLevels[CurrentLevelIndex + 1];
            }
            return null;
        }

        private void CheckLevels() {
            for (var i = 0; i < MainLevels.Count; i++) Debug.Log(MainLevels[i].name);
            Debug.Log("//----------//");
            for (var i = 0; i < TutorialLevels.Count; i++) Debug.Log(TutorialLevels[i].name);
        }

        private GameObject RandomSelectedLevel() {
            if (MainLevels.Count <= 1) return MainLevels[0];
            var obj = MainLevels[Random.Range(0, MainLevels.Count)];
            if (currentLevel == obj) return RandomSelectedLevel();
            return obj;
        }

        private void SaveOnNextLevel() {
            SaveSystem.SetData(Enum_Saves.MainSave, Enum_MainSave.PreviewLevel, PreviewLevelIndex + 1);
            //B_GM_GameManager.instance.Save.PreviewLevel = PreviewLevelIndex + 1;
        }
    }
}