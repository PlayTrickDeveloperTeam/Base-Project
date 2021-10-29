using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
namespace Base.UI
{
    public static class B_UI_SMF_MainFrame
    {
        public static B_UI_MSF_GameOver GameOver;
        public static B_UI_MSF_Loading Loading;
        public static B_UI_MSF_Main Main;
        public static B_UI_MSF_Paused Paused;
        public static B_UI_MSF_PlayerOverlay PlayerOverlay;

        public static void SetupStaticFrame()
        {
            GameOver = B_UI_ManagerMainFrame.instance.GameOverMenu();
            Loading = B_UI_ManagerMainFrame.instance.MenuLoading();
            Main = B_UI_ManagerMainFrame.instance.MenuMain();
            Paused = B_UI_ManagerMainFrame.instance.MenuPaused();
            PlayerOverlay = B_UI_ManagerMainFrame.instance.MenuPlayerOverlay();
        }

        public static void FlushData()
        {
            GameOver = null;
            Loading = null;
            Paused = null;
            PlayerOverlay = null;
        }
    }
}