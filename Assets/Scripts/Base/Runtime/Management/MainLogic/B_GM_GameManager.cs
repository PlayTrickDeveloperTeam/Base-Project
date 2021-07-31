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
        public B_SE_SaveDataObject MainSaveData;

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
            if (!PlayerPrefs.HasKey(Database_String.Save_Int_FirstTime)) MainSaveData.FirstTimeSaveSetup();
            else MainSaveData.DataReady();
            temp_showcase_index = GameObject.Find("temp_showcase_index").GetComponent<TextMeshProUGUI>();
            temp_showcase_index.text = "Current Level Showcase Index is : " + MainSaveData.GetDataI(B_SE_DataTypes.PreviewLevel).ToString();
            temp_showcase_name = GameObject.Find("temp_showcase_name").GetComponent<TextMeshProUGUI>();
            ClearSavesButton = GameObject.Find("btn_clearsaves").GetComponent<Button>();
            ClearSavesButton.onClick.AddListener(ClearSavesAndReload);
            B_LC_LevelManager.instance.OnLevelChangedAction += ChangeText;
            return true;
        }

        #region Function Testing

        void ClearSavesAndReload()
        {
            for (int i = 0; i < MainSaveData.DataContainer.DataCluster.Count; i++)
            {
                MainSaveData.SetData((B_SE_DataTypes)i, 0);
            }
            MainSaveData.SaveGameData();
            B_LC_LevelManager.instance.LoadInLevel(B_GM_GameManager.instance.MainSaveData.GetDataI(B_SE_DataTypes.PlayerLevel));
            temp_showcase_index.text = "Current Level Showcase Index is : " + 0;
            temp_showcase_name.text = "Current Level Name is : " + B_LC_LevelManager.instance.CurrentLevel.name;
            B_LC_LevelManager.instance.PreviewLevelIndex = 0;
            MainSaveData.LoadGameData();
        }

        void ChangeText(int t)
        {
            temp_showcase_index.text = "Current Level Showcase Index is " + t.ToString();
            temp_showcase_name.text = "Current Level Name is " + B_LC_LevelManager.instance.CurrentLevel.name;
        }

#if UNITY_EDITOR

        [ContextMenu("Load")]
        public void LoadData()
        {
            MainSaveData.LoadGameData();
            for (int i = 0; i < Enum.GetNames(typeof(B_SE_DataTypes)).Length; i++)
            {
                Debug.Log(MainSaveData.GetData((B_SE_DataTypes)i));
            }
        }
        [ContextMenu("Save")]
        public void SaveData()
        {
            foreach (var item in MainSaveData.DataContainer.DataCluster)
            {
                Debug.Log(item.Value);
            }
            MainSaveData.SaveGameData();
        }

#endif
        #endregion


        private void OnDestroy()
        {
            instance = null;
        }

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            this.MainSaveData.SaveGameData();
        }
#else
        private void OnApplicationPause(bool pause)
        {
            this.MainSaveData.SaveGameData();
        }
#endif
    }
}