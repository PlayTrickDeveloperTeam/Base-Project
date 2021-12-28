using System.Linq;
using Crystal;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
namespace Base.UI {
    public static class GUIManager {
        public static UI_Gameover GameOver;
        public static UI_Loading Loading;
        public static UI_Main Main;
        public static UI_Paused Paused;
        public static UI_PlayerOverlay PlayerOverlay;

        public static void SetupStaticFrame() {
            GameOver = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_GameOver).ToArray()[0].GetComponent<UI_Gameover>();
            Loading = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Loading).ToArray()[0].GetComponent<UI_Loading>();
            Main = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Main).ToArray()[0].GetComponent<UI_Main>();
            Paused = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_Paused).ToArray()[0].GetComponent<UI_Paused>();
            PlayerOverlay = B_UI_ManagerMainFrame.instance.Subframes.Where(t => t.MenuType == Enum_MenuTypes.Menu_PlayerOverlay).ToArray()[0].GetComponent<UI_PlayerOverlay>();
        }
        public static void FlushData() {
            GameOver = null;
            Loading = null;
            Paused = null;
            PlayerOverlay = null;
        }

        public static void AddSafeArea(this B_UI_MenuSubFrame subFrame) {
            subFrame.gameObject.AddComponent<SafeArea>();
        }
        
        public static void AddSafeArea(this UI_CPanelSubframe subPanel) {
            if(!subPanel.gameObject.GetComponent<SafeArea>())
                subPanel.gameObject.AddComponent<SafeArea>();
        }

        #region PanelActions

        public static void ActivateAllPanels(float time = 0) {
            GameOver.EnableUI(time);
            Loading.EnableUI(time);
            Main.EnableUI(time);
            Paused.EnableUI(time);
            PlayerOverlay.EnableUI(time);
        }

        public static void DeactivateAllPanels() {
            var movePos = Vector3.zero;
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

        public static void ActivateOnePanel(Enum_MenuTypes menu, float time = 0) {
            DeactivateAllPanels();
            switch (menu) {
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

        public static Tween DeactivateAllPanelsWithAnim() {
            var movePos = Vector3.zero;
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

        public static UI_CTMProGUISubframe GetText(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetText(enumToPull.ToString());
        }
        public static UI_CTMProGUISubframe GetText(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetText(enumToPull.ToString());
        }
        public static UI_CTMProGUISubframe GetText(Enum_Menu_MainComponent enumToPull) {
            return Main.GetText(enumToPull.ToString());
        }
        public static UI_CTMProGUISubframe GetText(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetText(enumToPull.ToString());
        }
        public static UI_CTMProGUISubframe GetText(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetText(enumToPull.ToString());
        }

        #endregion

        #region Slider

        public static UI_CSliderSubframe GetSlider(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetSlider(enumToPull.ToString());
        }
        public static UI_CSliderSubframe GetSlider(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetSlider(enumToPull.ToString());
        }
        public static UI_CSliderSubframe GetSlider(Enum_Menu_MainComponent enumToPull) {
            return Main.GetSlider(enumToPull.ToString());
        }
        public static UI_CSliderSubframe GetSlider(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetSlider(enumToPull.ToString());
        }
        public static UI_CSliderSubframe GetSlider(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetSlider(enumToPull.ToString());
        }

        #endregion

        #region Button

        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetButton(enumToPull.ToString());
        }
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetButton(enumToPull.ToString());
        }
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_MainComponent enumToPull) {
            return Main.GetButton(enumToPull.ToString());
        }
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetButton(enumToPull.ToString());
        }
        public static UI_CButtonTMProSubframe GetButton(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetButton(enumToPull.ToString());
        }

        #endregion

        #region Image

        public static UI_CImageSubframe GetImage(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetImage(enumToPull.ToString());
        }
        public static UI_CImageSubframe GetImage(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetImage(enumToPull.ToString());
        }
        public static UI_CImageSubframe GetImage(Enum_Menu_MainComponent enumToPull) {
            return Main.GetImage(enumToPull.ToString());
        }
        public static UI_CImageSubframe GetImage(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetImage(enumToPull.ToString());
        }
        public static UI_CImageSubframe GetImage(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetImage(enumToPull.ToString());
        }

        #endregion

        #region Panel

        public static UI_CPanelSubframe GetPanel(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetPanel(enumToPull.ToString());
        }
        public static UI_CPanelSubframe GetPanel(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetPanel(enumToPull.ToString());
        }
        public static UI_CPanelSubframe GetPanel(Enum_Menu_MainComponent enumToPull) {
            return Main.GetPanel(enumToPull.ToString());
        }
        public static UI_CPanelSubframe GetPanel(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetPanel(enumToPull.ToString());
        }
        public static UI_CPanelSubframe GetPanel(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetPanel(enumToPull.ToString());
        }

        #endregion

        #region Toggle

        public static UI_CToggleSubframe GetToggle(Enum_Menu_GameOverComponent enumToPull) {
            return GameOver.GetToggle(enumToPull.ToString());
        }
        public static UI_CToggleSubframe GetToggle(Enum_Menu_LoadingComponent enumToPull) {
            return Loading.GetToggle(enumToPull.ToString());
        }
        public static UI_CToggleSubframe GetToggle(Enum_Menu_MainComponent enumToPull) {
            return Main.GetToggle(enumToPull.ToString());
        }
        public static UI_CToggleSubframe GetToggle(Enum_Menu_PausedComponent enumToPull) {
            return Paused.GetToggle(enumToPull.ToString());
        }
        public static UI_CToggleSubframe GetToggle(Enum_Menu_PlayerOverlayComponent enumToPull) {
            return PlayerOverlay.GetToggle(enumToPull.ToString());
        }

        #endregion

        #region Editor Functions

#if UNITY_EDITOR
        [MenuItem("GameObject/Create Other/GUI System/Panel")]
        public static void CreateGUIPanel() {
            var obj = Resources.Load<GameObject>("GUIComponents/PanelSubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }

        [MenuItem("GameObject/Create Other/GUI System/Slider")]
        public static void CreateGUISlider() {
            var obj = Resources.Load<GameObject>("GUIComponents/SliderSubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }

        [MenuItem("GameObject/Create Other/GUI System/Image")]
        public static void CreateGUIImage() {
            var obj = Resources.Load<GameObject>("GUIComponents/ImageSubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }

        [MenuItem("GameObject/Create Other/GUI System/TMProText")]
        public static void CreateGUITMProText() {
            var obj = Resources.Load<GameObject>("GUIComponents/TMProGUISubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }

        [MenuItem("GameObject/Create Other/GUI System/TMProButton")]
        public static void CreateGUITMProButton() {
            var obj = Resources.Load<GameObject>("GUIComponents/ButtonGUISubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }

        [MenuItem("GameObject/Create Other/GUI System/Toggle")]
        public static void CreateGUIToggle() {
            var obj = Resources.Load<GameObject>("GUIComponents/ToggleSubframe");
            var SelectedObject = Selection.activeGameObject.transform;

            var createdobj = Object.Instantiate(obj);
            if (SelectedObject == null) {
                createdobj.GetComponent<RectTransform>().position = Vector3.zero;
            }
            else {
                createdobj.transform.SetParent(SelectedObject);
                createdobj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            }

            Selection.activeGameObject = createdobj;
        }
        #endif

        #endregion
    }
}