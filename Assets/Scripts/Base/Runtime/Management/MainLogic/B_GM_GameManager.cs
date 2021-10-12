using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Base
{
    public enum GameStates { Init, Start, Paused, Playing, End }
    public enum B_SE_DataTypes { GameFinished, PlayerLevel, TutorialPlayed, PreviewLevel }
    public class B_GM_GameManager : MonoBehaviour
    {
        public static B_GM_GameManager instance;
        public GameStates CurrentGameState;
        public SaveData Save;

        TextMeshProUGUI temp_showcase_index;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
        }

        public bool GameManagerStrapping()
        {
            Save = new SaveData();
            Save.PrepareSaveSystem();
            temp_showcase_index = GameObject.Find("temp_showcase_index").GetComponent<TextMeshProUGUI>();
            temp_showcase_index.text = "Current Level Showcase Index is : " + Save.PreviewLevel.ToString();
            B_LC_LevelManager.instance.OnLevelChangedAction += ChangeText;
            return true;
        }

        public bool IsGamePlaying()
        {
            if (CurrentGameState == GameStates.Playing) return true;
            return false;
        }

        #region Function Testing

        void ChangeText(int t)
        {
            temp_showcase_index.text = "Current Level Showcase Index is " + t.ToString();
        }

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