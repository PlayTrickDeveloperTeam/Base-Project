namespace Base {
    public static class B_Database_String {
        
        #region PlayerPrefs

        public const string Save_Int_FirstTime = "FirstTime";

        #endregion PlayerPrefs

        #region Debug Manager

        public const string Text_Object_Debug = "DebugTextObject";

        #endregion Debug Manager

        #region Menu Manager

        public const string Panel_Loading = "panel_loading";
        public const string Panel_Start = "panel_start";
        public const string Panel_Settings = "panel_settings";
        public const string Panel_Ending = "panel_ending";
        public const string Panel_Ingame = "panel_ingame";

        public const string BG_Ending_Fail = "bg_ending_fail";
        public const string BG_Ending_Success = "bg_ending_success";

        public const string BTN_M_Start = "btn_m_start";
        public const string BTN_M_Settings = "btn_m_settings";
        public const string BTN_IG_Restart = "btn_ig_restart";
        public const string BTN_IG_Menu = "btn_ig_menu";
        public const string BTN_IG_Pause = "btn_ig_pause";
        public const string BTN_IG_WatchAdd = "btn_ig_watchadd";
        public const string BTN_IG_ClaimReward = "btn_ig_claimreward";
        public const string BTN_IG_End = "btn_ig_end";

        #endregion Menu Manager

        #region Level Manager

        //Resource management system needs a lot more reworks
        //Add necesery strings
        public const string Path_Res_Levels = "Levels";

        public const string Path_Res_MainLevels = Path_Res_Levels + "/MainLevels/";
        public const string Path_Res_TutorialLevels = Path_Res_Levels + "/TutorialLevels/";

        public const string Path_Res_ScriptableObjects = "";

        #endregion Level Manager
    }
}