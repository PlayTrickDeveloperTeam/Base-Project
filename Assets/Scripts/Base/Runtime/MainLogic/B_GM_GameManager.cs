using System;
using System.Threading.Tasks;
using Base.UI;
using UnityEngine;
namespace Base {
    public enum GameStates { Init, Start, Paused, Playing, End }

    public enum B_SE_DataTypes { GameFinished, PlayerLevel, TutorialPlayed, PreviewLevel }

    public class B_GM_GameManager : B_M_ManagerBase {
        public static B_GM_GameManager instance;
        public static Action OnGameStateChange;
        private GameStates _currentGameState;

        public SaveSystemEditor Save;
        public GameStates CurrentGameState {
            get => _currentGameState;
            set {
                if (_currentGameState == value) return;
                _currentGameState = value;
                B_CES_CentralEventSystem.OnGameStateChange.InvokeEvent();
            }
        }

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            Save = new SaveSystemEditor();
            GUIManager.GetButton(Enum_Menu_MainComponent.BTN_Start).AddFunction(StartGame);
            GUIManager.GetButton(Enum_Menu_GameOverComponent.BTN_Sucess).AddFunction(EndLevel);
            GUIManager.GetButton(Enum_Menu_GameOverComponent.BTN_Fail).AddFunction(RestartLevel);

            return base.ManagerStrapping();
        }
        public override Task ManagerDataFlush() {
            instance = null;
            return base.ManagerDataFlush();
        }

        public bool IsGamePlaying() {
            if (CurrentGameState == GameStates.Playing) return true;
            return false;
        }

        #region Game Management Functions

        private void StartGame() {
            B_CES_CentralEventSystem.BTN_OnStartPressed.InvokeEvent();
            instance.CurrentGameState = GameStates.Playing;
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_PlayerOverlay);
        }

        private void RestartLevel() {
            B_CES_CentralEventSystem.BTN_OnRestartPressed.InvokeEvent();
            B_LC_LevelManager.instance.ReloadCurrentLevel();
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main, .3f);
        }

        private void EndLevel() {
            B_CES_CentralEventSystem.BTN_OnEndGamePressed.InvokeEvent();
            instance.CurrentGameState = GameStates.Start;
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_Main);
            B_LC_LevelManager.instance.LoadInNextLevel();
        }

        public async void ActivateEndgame(bool Success, float Delay = 0) {
            if (CurrentGameState == GameStates.End || CurrentGameState == GameStates.Start) return;
            instance.CurrentGameState = GameStates.End;
            switch (Success) {
                case true:
                    B_CES_CentralEventSystem.OnBeforeLevelDisablePositive.InvokeEvent();
                    break;
                case false:
                    B_CES_CentralEventSystem.OnBeforeLevelDisableNegative.InvokeEvent();
                    break;
            }
            await Task.Delay((int)Delay * 1000);
            GUIManager.ActivateOnePanel(Enum_MenuTypes.Menu_GameOver);
            GUIManager.GameOver.EnableOverUI(Success);
            Save.SaveAllData();
        }

#if UNITY_IOS
                private void OnApplicationPause(bool pause) {
            
            Save.SaveAllData();
        }

#endif

        private void OnApplicationQuit() {
            Save.SaveAllData();
        }

        #endregion


        #region Function Testing

        #endregion Function Testing
    }

    //Will Keep it as an example
    public class SaveData {
        public int GameFinished {
            get => PlayerPrefs.GetInt(B_SE_DataTypes.GameFinished.ToString());
            set => PlayerPrefs.SetInt(B_SE_DataTypes.GameFinished.ToString(), value);
        }

        public int PlayerLevel {
            get => PlayerPrefs.GetInt(B_SE_DataTypes.PlayerLevel.ToString());
            set => PlayerPrefs.SetInt(B_SE_DataTypes.PlayerLevel.ToString(), value);
        }

        public int TutorialPlayed {
            get => PlayerPrefs.GetInt(B_SE_DataTypes.TutorialPlayed.ToString());
            set => PlayerPrefs.SetInt(B_SE_DataTypes.TutorialPlayed.ToString(), value);
        }

        public int PreviewLevel {
            get => PlayerPrefs.GetInt(B_SE_DataTypes.PreviewLevel.ToString());
            set => PlayerPrefs.SetInt(B_SE_DataTypes.PreviewLevel.ToString(), value);
        }

        //public int PlayerCoin
        //{
        //    get { return PlayerPrefs.GetInt(B_SE_DataTypes.PlayerCoin.ToString()); }
        //    set { PlayerPrefs.SetInt(B_SE_DataTypes.PlayerCoin.ToString(), value); }
        //}

        public void PrepareSaveSystem() {
            CheckExist(B_SE_DataTypes.GameFinished.ToString());
            CheckExist(B_SE_DataTypes.PlayerLevel.ToString());
            CheckExist(B_SE_DataTypes.TutorialPlayed.ToString());
            CheckExist(B_SE_DataTypes.PreviewLevel.ToString());
            //CheckExist(B_SE_DataTypes.PlayerCoin.ToString());
        }

        private void CheckExist(string name) {
            if (PlayerPrefs.HasKey(name)) return;
            PlayerPrefs.SetInt(name, 0);
        }
    }

}