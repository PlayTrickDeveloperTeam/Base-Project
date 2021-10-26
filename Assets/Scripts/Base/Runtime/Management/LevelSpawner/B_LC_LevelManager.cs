using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base
{
    public enum LevelType { Tutorial, Main }

    public class B_LC_LevelManager : MonoBehaviour
    {
        public static B_LC_LevelManager instance;

        public Action<int> OnLevelChangedAction;

        [HideInInspector] public List<GameObject> MainLevels;
        [HideInInspector] public List<GameObject> TutorialLevels;

        [HideInInspector] public GameObject CurrentLevel;
        private GameObject currentLevel;

        [HideInInspector] public int CurrentLevelIndex;
        [HideInInspector] public int PreviewLevelIndex;

        [HideInInspector] public Transform LevelHolder { get; private set; }
        public static Transform ObjectSpawnParent;

        private int tutorialPlayed
        {
            get
            {
                return B_GM_GameManager.instance.Save.TutorialPlayed;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public bool StrappingLevelController()
        {
            LevelHolder = GameObject.Find("LevelHolder").GetComponent<Transform>();
            MainLevels = new List<GameObject>();
            TutorialLevels = new List<GameObject>();
            MainLevels = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_MainLevels).ToList();
            TutorialLevels = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_TutorialLevels).ToList();
            MainLevels = MainLevels.OrderBy(t => t.name).ToList();
            TutorialLevels = TutorialLevels.OrderBy(t => t.name).ToList();
            PreviewLevelIndex = B_GM_GameManager.instance.Save.PreviewLevel;

            B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.AddFunction(SaveOnNextLevel, true);

            ObjectSpawnParent = LevelHolder.GetChild(0);


            return true;
        }

        public void LoadInLevel(int levelNumber)
        {
            switch (tutorialPlayed)
            {
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

        public void LoadInNextLevel()
        {
            switch (B_GM_GameManager.instance.Save.GameFinished)
            {
                case 0:
                    InitateNewLevel(LevelToLoad());
                    break;

                case 1:
                    InitateNewLevel(RandomSelectedLevel());
                    break;
            }
        }

        public void ReloadCurrentLevel()
        {
            InitateNewLevel(currentLevel);
        }

        private void InitateNewLevel(GameObject levelToInit)
        {
            B_CES_CentralEventSystem.OnBeforeLevelLoaded.InvokeEvent();
            if (CurrentLevel != null) { Destroy(CurrentLevel); CurrentLevel = null; currentLevel = null; }
            CurrentLevel = GameObject.Instantiate(levelToInit, LevelHolder);
            currentLevel = levelToInit;
            switch (tutorialPlayed)
            {
                case 0:
                    CurrentLevelIndex = Array.IndexOf(TutorialLevels.ToArray(), levelToInit);
                    B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
                    B_GM_GameManager.instance.Save.PreviewLevel = CurrentLevelIndex;
                    OnLevelChangedAction?.Invoke(CurrentLevelIndex);
                    break;

                case 1:
                    CurrentLevelIndex = Array.IndexOf(MainLevels.ToArray(), levelToInit);
                    B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
                    OnLevelChangedAction?.Invoke(PreviewLevelIndex);
                    break;
            }
            B_CES_CentralEventSystem.OnAfterLevelLoaded.InvokeEvent();
            B_GM_GameManager.instance.Save.PlayerLevel = CurrentLevelIndex;
        }

        private GameObject LevelToLoad()
        {
            switch (tutorialPlayed)
            {
                case 0:
                    if (CurrentLevelIndex + 1 >= TutorialLevels.Count)
                    {
                        CurrentLevelIndex = 0;
                        B_GM_GameManager.instance.Save.TutorialPlayed = 1;
                        return MainLevels[0];
                    }
                    return TutorialLevels[CurrentLevelIndex + 1];

                case 1:
                    if (CurrentLevelIndex + 1 >= MainLevels.Count)
                    {
                        B_GM_GameManager.instance.Save.GameFinished = 1;
                        return RandomSelectedLevel();
                    }
                    return MainLevels[CurrentLevelIndex + 1];
            }
            return null;
        }

        private void CheckLevels()
        {
            for (int i = 0; i < MainLevels.Count; i++)
            {
                Debug.Log(MainLevels[i].name);
            }
            Debug.Log("//----------//");
            for (int i = 0; i < TutorialLevels.Count; i++)
            {
                Debug.Log(TutorialLevels[i].name);
            }
        }

        private GameObject RandomSelectedLevel()
        {
            if (MainLevels.Count <= 1) { return MainLevels[0]; }
            GameObject obj = MainLevels[UnityEngine.Random.Range(0, MainLevels.Count)];
            if (currentLevel == obj) return RandomSelectedLevel();
            return obj;
        }

        private void SaveOnNextLevel()
        {
            B_GM_GameManager.instance.Save.PreviewLevel = PreviewLevelIndex + 1;
        }

        private void OnDestroy()
        {
            instance = null;
            ObjectSpawnParent = null;
        }
    }
}