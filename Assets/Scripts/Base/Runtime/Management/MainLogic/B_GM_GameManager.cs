using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Base
{
    public enum GameStates { Init, Start, Paused, Playing, End }
    public class B_GM_GameManager : MonoBehaviour
    {
        public static B_GM_GameManager instance;
        public GameStates CurrentGameState;
        //public B_SE_SaveDataObject MainSaveData;
        public SaveData Save;

        TextMeshProUGUI temp_showcase_index;
        TextMeshProUGUI temp_showcase_name;

        Button ClearSavesButton;

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

        public bool GameManagerStrapping()
        {
            //if (!PlayerPrefs.HasKey(Database_String.Save_Int_FirstTime)) MainSaveData.FirstTimeSaveSetup();
            //else MainSaveData.DataReady();
            temp_showcase_index = GameObject.Find("temp_showcase_index").GetComponent<TextMeshProUGUI>();
            temp_showcase_index.text = "Current Level Showcase Index is : " + Save.PreviewLevel.ToString();
            temp_showcase_name = GameObject.Find("temp_showcase_name").GetComponent<TextMeshProUGUI>();
            ClearSavesButton = GameObject.Find("btn_clearsaves").GetComponent<Button>();
            B_LC_LevelManager.instance.OnLevelChangedAction += ChangeText;
            return true;
        }

        public bool IsGamePlaying()
        {
            if (CurrentGameState == GameStates.Playing) return true;
            return false;
        }

        #region Function Testing

        //void ClearSavesAndReload()
        //{
        //    for (int i = 0; i < MainSaveData.DataContainer.DataCluster.Count; i++)
        //    {
        //        MainSaveData.SetData((B_SE_DataTypes)i, 0);
        //    }
        //    MainSaveData.SaveGameData();
        //    B_LC_LevelManager.instance.LoadInLevel(B_GM_GameManager.instance.MainSaveData.GetDataI(B_SE_DataTypes.PlayerLevel));
        //    temp_showcase_index.text = "Current Level Showcase Index is : " + 0;
        //    temp_showcase_name.text = "Current Level Name is : " + B_LC_LevelManager.instance.CurrentLevel.name;
        //    B_LC_LevelManager.instance.PreviewLevelIndex = 0;
        //    MainSaveData.LoadGameData();
        //}

        void ChangeText(int t)
        {
            temp_showcase_index.text = "Current Level Showcase Index is " + t.ToString();
            temp_showcase_name.text = "Current Level Name is " + B_LC_LevelManager.instance.CurrentLevel.name;
        }

#if UNITY_EDITOR

        //[ContextMenu("Load")]
        //public void LoadData()
        //{
        //    MainSaveData.LoadGameData();
        //    for (int i = 0; i < Enum.GetNames(typeof(B_SE_DataTypes)).Length; i++)
        //    {
        //        Debug.Log(MainSaveData.GetData((B_SE_DataTypes)i));
        //    }
        //}
        //[ContextMenu("Save")]
        //public void SaveData()
        //{
        //    foreach (var item in MainSaveData.DataContainer.DataCluster)
        //    {
        //        Debug.Log(item.Value);
        //    }
        //    MainSaveData.SaveGameData();
        //}

#endif
        #endregion


        private void OnDestroy()
        {
            instance = null;
        }

    }

    public class SaveData
    {
        public int GameFinished
        {
            get { return PlayerPrefs.GetInt(B_SE_DataTypes.GameFinished.ToString()); }
            set { PlayerPrefs.SetInt(B_SE_DataTypes.GameFinished.ToString(), value); }
        }
        public int PlayerLevel
        {
            get { return PlayerPrefs.GetInt(B_SE_DataTypes.PlayerLevel.ToString()); }
            set { PlayerPrefs.SetInt(B_SE_DataTypes.PlayerLevel.ToString(), value); }
        }
        public int TutorialPlayed
        {
            get { return PlayerPrefs.GetInt(B_SE_DataTypes.TutorialPlayed.ToString()); }
            set { PlayerPrefs.SetInt(B_SE_DataTypes.TutorialPlayed.ToString(), value); }
        }
        public int PreviewLevel
        {
            get { return PlayerPrefs.GetInt(B_SE_DataTypes.PreviewLevel.ToString()); }
            set { PlayerPrefs.SetInt(B_SE_DataTypes.PreviewLevel.ToString(), value); }
        }
        //public int PlayerCoin
        //{
        //    get { return PlayerPrefs.GetInt(B_SE_DataTypes.PlayerCoin.ToString()); }
        //    set { PlayerPrefs.SetInt(B_SE_DataTypes.PlayerCoin.ToString(), value); }
        //}

        public void PrepareSaveSystem()
        {
            CheckExist(B_SE_DataTypes.GameFinished.ToString());
            CheckExist(B_SE_DataTypes.PlayerLevel.ToString());
            CheckExist(B_SE_DataTypes.TutorialPlayed.ToString());
            CheckExist(B_SE_DataTypes.PreviewLevel.ToString());
            //CheckExist(B_SE_DataTypes.PlayerCoin.ToString());
        }

        void CheckExist(string name)
        {
            if (PlayerPrefs.HasKey(name)) return;
            PlayerPrefs.SetInt(name, 0);
        }
    }
}