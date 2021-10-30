using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using DG.Tweening;
using System.Threading.Tasks;

namespace Base.UI
{
    public static class GUIManager
    {
        public static UI_Gameover GameOver;
        public static UI_Loading Loading;
        public static UI_Main Main;
        public static UI_Paused Paused;
        public static UI_PlayerOverlay PlayerOverlay;

        public static void SetupStaticFrame()
        {
            GameOver = B_UI_ManagerMainFrame.instance.GameOverMenu();
            Loading = B_UI_ManagerMainFrame.instance.MenuLoading();
            Main = B_UI_ManagerMainFrame.instance.MenuMain();
            Paused = B_UI_ManagerMainFrame.instance.MenuPaused();
            PlayerOverlay = B_UI_ManagerMainFrame.instance.MenuPlayerOverlay();
        }

        #region PanelActions

        public static void ActivateAllPanels()
        {
            GameOver.EnableUI();
            Loading.EnableUI();
            Main.EnableUI();
            Paused.EnableUI();
            PlayerOverlay.EnableUI();
        }

        public static void DeactivateAllPanels()
        {
            Vector3 movePos = Vector3.zero;
            movePos.x += 1500;
            GameOver.MoveUI(movePos);
            movePos.x += 1500;
            Loading.MoveUI(movePos);
            movePos.x += 1500;
            Main.MoveUI(movePos);
            movePos.x += 1500;
            Paused.MoveUI(movePos);
            movePos.x += 1500;
            PlayerOverlay.MoveUI(movePos);
        }

        public static void ActivateOnePanel(Enum_MenuTypes menu)
        {
            DeactivateAllPanels();
            switch (menu)
            {
                case Enum_MenuTypes.Menu_Main:
                    Main.EnableUI();
                    break;
                case Enum_MenuTypes.Menu_PlayerOverlay:
                    PlayerOverlay.EnableUI();
                    break;
                case Enum_MenuTypes.Menu_Paused:
                    Paused.EnableUI();
                    break;
                case Enum_MenuTypes.Menu_GameOver:
                    GameOver.EnableUI();
                    break;
                case Enum_MenuTypes.Menu_Loading:
                    Loading.EnableUI();
                    break;
            }
        }

        public static Tween DeactivateAllPanelsWithAnim()
        {
            Vector3 movePos = Vector3.zero;
            movePos.x += 1500;
            GameOver.MoveUI(movePos, 2);
            movePos.x += 1500;
            Loading.MoveUI(movePos, 2);
            movePos.x += 1500;
            Main.MoveUI(movePos, 2);
            movePos.x += 1500;
            Paused.MoveUI(movePos, 2);
            movePos.x += 1500;
            return PlayerOverlay.MoveUI(movePos, 2);
        }

        #endregion

        #region Text

        public static B_UI_CTMProGUI_Subframe GetText(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetText(enumToPull.ToString());
        public static B_UI_CTMProGUI_Subframe GetText(Enum_Menu_LoadingComponent enumToPull) => Loading.GetText(enumToPull.ToString());
        public static B_UI_CTMProGUI_Subframe GetText(Enum_Menu_MainComponent enumToPull) => Main.GetText(enumToPull.ToString());
        public static B_UI_CTMProGUI_Subframe GetText(Enum_Menu_PausedComponent enumToPull) => Paused.GetText(enumToPull.ToString());
        public static B_UI_CTMProGUI_Subframe GetText(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetText(enumToPull.ToString());


        #endregion

        #region Slider

        public static B_UI_CSlider_Subframe GetSlider(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetSlider(enumToPull.ToString());
        public static B_UI_CSlider_Subframe GetSlider(Enum_Menu_LoadingComponent enumToPull) => Loading.GetSlider(enumToPull.ToString());
        public static B_UI_CSlider_Subframe GetSlider(Enum_Menu_MainComponent enumToPull) => Main.GetSlider(enumToPull.ToString());
        public static B_UI_CSlider_Subframe GetSlider(Enum_Menu_PausedComponent enumToPull) => Paused.GetSlider(enumToPull.ToString());
        public static B_UI_CSlider_Subframe GetSlider(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetSlider(enumToPull.ToString());


        #endregion

        #region Button

        public static B_UI_CTMProGUIButton_Subframe GetButton(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetButton(enumToPull.ToString());
        public static B_UI_CTMProGUIButton_Subframe GetButton(Enum_Menu_LoadingComponent enumToPull) => Loading.GetButton(enumToPull.ToString());
        public static B_UI_CTMProGUIButton_Subframe GetButton(Enum_Menu_MainComponent enumToPull) => Main.GetButton(enumToPull.ToString());
        public static B_UI_CTMProGUIButton_Subframe GetButton(Enum_Menu_PausedComponent enumToPull) => Paused.GetButton(enumToPull.ToString());
        public static B_UI_CTMProGUIButton_Subframe GetButton(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetButton(enumToPull.ToString());


        #endregion

        public static B_UI_CImage_Subframe GetImage(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetImage(enumToPull.ToString());
        public static B_UI_CImage_Subframe GetImage(Enum_Menu_LoadingComponent enumToPull) => Loading.GetImage(enumToPull.ToString());
        public static B_UI_CImage_Subframe GetImage(Enum_Menu_MainComponent enumToPull) => Main.GetImage(enumToPull.ToString());
        public static B_UI_CImage_Subframe GetImage(Enum_Menu_PausedComponent enumToPull) => Paused.GetImage(enumToPull.ToString());
        public static B_UI_CImage_Subframe GetImage(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetImage(enumToPull.ToString());


        #region Image

        #endregion
        public static void FlushData()
        {
            GameOver = null;
            Loading = null;
            Paused = null;
            PlayerOverlay = null;
        }
    }
}