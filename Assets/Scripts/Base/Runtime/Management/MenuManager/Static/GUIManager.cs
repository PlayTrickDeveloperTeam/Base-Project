using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using DG.Tweening;
using System.Threading.Tasks;
using System.Linq;

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
            GameOver = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_GameOver).ToArray()[0].GetComponent<UI_Gameover>();
            Loading = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Loading).ToArray()[0].GetComponent<UI_Loading>();
            Main = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Main).ToArray()[0].GetComponent<UI_Main>();
            Paused = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Paused).ToArray()[0].GetComponent<UI_Paused>();
            PlayerOverlay = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_PlayerOverlay).ToArray()[0].GetComponent<UI_PlayerOverlay>();
        }

        #region PanelActions

        public static void ActivateAllPanels(float time = 0)
        {
            GameOver.EnableUI(time);
            Loading.EnableUI(time);
            Main.EnableUI(time);
            Paused.EnableUI(time);
            PlayerOverlay.EnableUI(time);
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

        public static void ActivateOnePanel(Enum_MenuTypes menu, float time = 0)
        {
            DeactivateAllPanels();
            switch (menu)
            {
                case Enum_MenuTypes.Menu_Main:
                    Main.EnableUI(time);
                    break;
                case Enum_MenuTypes.Menu_PlayerOverlay:
                    PlayerOverlay.EnableUI(time);
                    break;
                case Enum_MenuTypes.Menu_Paused:
                    Paused.EnableUI(time);
                    break;
                case Enum_MenuTypes.Menu_GameOver:
                    GameOver.EnableUI(time);
                    break;
                case Enum_MenuTypes.Menu_Loading:
                    Loading.EnableUI(time);
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

        public static UI_CTMProGUISubframe GetText(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetText(enumToPull.ToString());
        public static UI_CTMProGUISubframe GetText(Enum_Menu_LoadingComponent enumToPull) => Loading.GetText(enumToPull.ToString());
        public static UI_CTMProGUISubframe GetText(Enum_Menu_MainComponent enumToPull) => Main.GetText(enumToPull.ToString());
        public static UI_CTMProGUISubframe GetText(Enum_Menu_PausedComponent enumToPull) => Paused.GetText(enumToPull.ToString());
        public static UI_CTMProGUISubframe GetText(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetText(enumToPull.ToString());


        #endregion

        #region Slider

        public static UI_CSliderSubframe GetSlider(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetSlider(enumToPull.ToString());
        public static UI_CSliderSubframe GetSlider(Enum_Menu_LoadingComponent enumToPull) => Loading.GetSlider(enumToPull.ToString());
        public static UI_CSliderSubframe GetSlider(Enum_Menu_MainComponent enumToPull) => Main.GetSlider(enumToPull.ToString());
        public static UI_CSliderSubframe GetSlider(Enum_Menu_PausedComponent enumToPull) => Paused.GetSlider(enumToPull.ToString());
        public static UI_CSliderSubframe GetSlider(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetSlider(enumToPull.ToString());


        #endregion

        #region Button

        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetButton(enumToPull.ToString());
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_LoadingComponent enumToPull) => Loading.GetButton(enumToPull.ToString());
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_MainComponent enumToPull) => Main.GetButton(enumToPull.ToString());
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_PausedComponent enumToPull) => Paused.GetButton(enumToPull.ToString());
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetButton(enumToPull.ToString());


        #endregion

        #region Image
        public static UI_CImageSubframe GetImage(Enum_Menu_GameOverComponent enumToPull) => GameOver.GetImage(enumToPull.ToString());
        public static UI_CImageSubframe GetImage(Enum_Menu_LoadingComponent enumToPull) => Loading.GetImage(enumToPull.ToString());
        public static UI_CImageSubframe GetImage(Enum_Menu_MainComponent enumToPull) => Main.GetImage(enumToPull.ToString());
        public static UI_CImageSubframe GetImage(Enum_Menu_PausedComponent enumToPull) => Paused.GetImage(enumToPull.ToString());
        public static UI_CImageSubframe GetImage(Enum_Menu_PlayerOverlayComponent enumToPull) => PlayerOverlay.GetImage(enumToPull.ToString());

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