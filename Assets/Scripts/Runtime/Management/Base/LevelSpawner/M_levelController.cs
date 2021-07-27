using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Main
{
    public enum LevelType { Tutorial, Main }
    public class M_levelController : MonoBehaviour
    {
        public static M_levelController instance;

        public Action<int> OnLevelChangedAction;

        public List<GameObject> MainLevels;
        public List<GameObject> TutorialLevels;

        public GameObject CurrentLevel;
        private GameObject currentLevel;
        public M_LevelPrep CurrentLevelFunctions = null;

        public int CurrentLevelIndex;
        public int PreviewLevelIndex;

        public Transform LevelHolder { get; private set; }

        private int tutorialPlayed
        {
            get
            {
                return M_GameManager.instance.MainSaveData.GetDataI(SE_DataTypes.TutorialPlayed);
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
            MainLevels = Resources.LoadAll<GameObject>(Database_String.Path_Res_MainLevels).ToList();
            TutorialLevels = Resources.LoadAll<GameObject>(Database_String.Path_Res_TutorialLevels).ToList();
            MainLevels = MainLevels.OrderBy(t => t.name).ToList();
            TutorialLevels = TutorialLevels.OrderBy(t => t.name).ToList();
            PreviewLevelIndex = M_GameManager.instance.MainSaveData.GetDataI(SE_DataTypes.PreviewLevel);
            return true;
        }

        public void LoadInLevel(int levelNumber)
        {
            switch (tutorialPlayed)
            {
                case 0:
                    InitateNewLevel(TutorialLevels[levelNumber]);
                    break;
                case 1:
                    InitateNewLevel(MainLevels[levelNumber]);
                    break;
            }
        }

        public void LoadInNextLevel()
        {
            switch (M_GameManager.instance.MainSaveData.GetDataI(SE_DataTypes.GameFinished))
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
            if (CurrentLevel != null) { Destroy(CurrentLevel); CurrentLevel = null; currentLevel = null; }
            CurrentLevel = GameObject.Instantiate(levelToInit, LevelHolder);
            currentLevel = levelToInit;
            CurrentLevelFunctions = CurrentLevel.GetComponent<M_LevelPrep>();
            switch (tutorialPlayed)
            {
                case 0:
                    CurrentLevelIndex = Array.IndexOf(TutorialLevels.ToArray(), levelToInit);
                    M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PlayerLevel, CurrentLevelIndex);
                    M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PreviewLevel, CurrentLevelIndex);
                    OnLevelChangedAction?.Invoke(CurrentLevelIndex);
                    break;
                case 1:
                    CurrentLevelIndex = Array.IndexOf(MainLevels.ToArray(), levelToInit);
                    M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PlayerLevel, CurrentLevelIndex);
                    M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PreviewLevel, PreviewLevelIndex += 1);
                    OnLevelChangedAction?.Invoke(PreviewLevelIndex);
                    break;
            }
            if (CurrentLevelFunctions == null) throw new Exception("Didn't load the correct level");
            CurrentLevelFunctions.OnLevelReady();
            M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PlayerLevel, CurrentLevelIndex);
        }



        private GameObject LevelToLoad()
        {
            switch (tutorialPlayed)
            {
                case 0:
                    if (CurrentLevelIndex + 1 >= TutorialLevels.Count)
                    {
                        CurrentLevelIndex = 0;
                        M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.TutorialPlayed, 1);
                        return MainLevels[0];
                    }
                    return TutorialLevels[CurrentLevelIndex + 1];
                case 1:
                    if (CurrentLevelIndex + 1 >= MainLevels.Count)
                    {
                        M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.GameFinished, 1);
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

        private void OnDestroy()
        {
            instance = null;
        }
    }
}