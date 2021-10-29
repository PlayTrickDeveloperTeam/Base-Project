using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Base
{
    public enum GameStates { Init, Start, Paused, Playing, End }

    public enum B_SE_DataTypes { GameFinished, PlayerLevel, TutorialPlayed, PreviewLevel }

    public class B_GM_GameManager : B_M_ManagerBase
    {
        public static B_GM_GameManager instance;

        public GameStates CurrentGameState;
        public SaveData Save;

        public override Task ManagerStrapping()
        {
            if (instance == null) instance = this; else Destroy(this.gameObject);
            Save = new SaveData();
            Save.PrepareSaveSystem();
            return base.ManagerStrapping();
        }
        public override Task ManagerDataFlush()
        {
            instance = null;
            return base.ManagerDataFlush();
        }

        public bool IsGamePlaying()
        {
            if (CurrentGameState == GameStates.Playing) return true;
            return false;
        }

        #region Function Testing


        #endregion Function Testing
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

        private void CheckExist(string name)
        {
            if (PlayerPrefs.HasKey(name)) return;
            PlayerPrefs.SetInt(name, 0);
        }
    }

}