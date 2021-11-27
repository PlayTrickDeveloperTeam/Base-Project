using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal;
using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif
namespace Base.UI {
    public abstract class B_UI_MenuSubFrame : MonoBehaviour {
        [SerializeField] private bool SafeArea;
        public Enum_MenuTypes MenuType;
        public List<UI_TComponentsSubframe> SubComponents;
        private Dictionary<string, UI_CButtonTMProSubframe> ButtonDictionary;
        private Dictionary<string, UI_CImageSubframe> ImageDictionary;
        private Dictionary<string, UI_CPanelSubframe> PanelDictionary;
        private B_UI_ManagerMainFrame Parent;
        private Dictionary<string, UI_CSliderSubframe> SliderDictionary;
        private Dictionary<string, UI_CTMProGUISubframe> TMProDictionary;
        private Dictionary<string, UI_CToggleSubframe> ToggleDictionary;

        public virtual async Task SetupFrame(B_UI_ManagerMainFrame Mainframe) {
            Parent = Mainframe;
#if UNITY_EDITOR
            SetSubComponents();
            if (EditorApplication.isPlaying)
                SetupDictionaries();
#else
                SetupDictionaries();
#endif
        }

        public virtual Task FlushFrameData() {
            Parent = null;
            SubComponents = new List<UI_TComponentsSubframe>();
            return Task.CompletedTask;
        }

        public virtual Tween EnableUI(float Time = 0, bool Snap = true) {
            return MoveUI(Vector3.zero, Time, Snap);
        }

        public virtual Tween DisableUI(float Time = 0, bool Snap = true) {
            return MoveUI(Vector3.one * 5000, Time, Snap);
        }

        public Tween MoveUI(Vector3 Position, float Time = 0, bool Snap = true) {
            return GetComponent<RectTransform>().DOLocalMove(Position, Time, Snap);
        }

        public Task MoveUIRect(Vector3 Position) {
            GetComponent<RectTransform>().localPosition = Position;
            return Task.CompletedTask;
        }



        private void SetupDictionaries() {
            if (SafeArea) {
                this.AddSafeArea();
            }
            TMProDictionary = new Dictionary<string, UI_CTMProGUISubframe>();
            var _tempTMPro = SubComponents.Where(t => t.GetComponent<UI_CTMProGUISubframe>()).ToArray();
            for (var i = 0; i < _tempTMPro.Length; i++)
                if (_tempTMPro[i].GetComponent<UI_CTMProGUISubframe>())
                    TMProDictionary.Add(_tempTMPro[i].GetComponent<UI_CTMProGUISubframe>().EnumName, _tempTMPro[i].GetComponent<UI_CTMProGUISubframe>());

            SliderDictionary = new Dictionary<string, UI_CSliderSubframe>();
            var _tempSlider = SubComponents.Where(t => t.GetComponent<UI_CSliderSubframe>()).ToArray();
            for (var i = 0; i < _tempSlider.Length; i++)
                if (_tempSlider[i].GetComponent<UI_CSliderSubframe>())
                    SliderDictionary.Add(_tempSlider[i].GetComponent<UI_CSliderSubframe>().EnumName, _tempSlider[i].GetComponent<UI_CSliderSubframe>());

            ButtonDictionary = new Dictionary<string, UI_CButtonTMProSubframe>();
            var _tempButton = SubComponents.Where(t => t.GetComponent<UI_CButtonTMProSubframe>()).ToArray();
            for (var i = 0; i < _tempButton.Length; i++)
                if (_tempButton[i].GetComponent<UI_CButtonTMProSubframe>())
                    ButtonDictionary.Add(_tempButton[i].GetComponent<UI_CButtonTMProSubframe>().EnumName, _tempButton[i].GetComponent<UI_CButtonTMProSubframe>());

            ImageDictionary = new Dictionary<string, UI_CImageSubframe>();
            var _tempImage = SubComponents.Where(t => t.GetComponent<UI_CImageSubframe>()).ToArray();
            for (var i = 0; i < _tempImage.Length; i++)
                if (_tempImage[i].GetComponent<UI_CImageSubframe>())
                    ImageDictionary.Add(_tempImage[i].GetComponent<UI_CImageSubframe>().EnumName, _tempImage[i].GetComponent<UI_CImageSubframe>());

            PanelDictionary = new Dictionary<string, UI_CPanelSubframe>();
            var _tempPanel = SubComponents.Where(t => t.GetComponent<UI_CPanelSubframe>()).ToArray();
            for (var i = 0; i < _tempPanel.Length; i++)
                if (_tempPanel[i].GetComponent<UI_CPanelSubframe>())
                    PanelDictionary.Add(_tempPanel[i].GetComponent<UI_CPanelSubframe>().EnumName, _tempPanel[i].GetComponent<UI_CPanelSubframe>());

            ToggleDictionary = new Dictionary<string, UI_CToggleSubframe>();
            var _tempToggle = SubComponents.Where(t => t.GetComponent<UI_CToggleSubframe>()).ToArray();
            for (var i = 0; i < _tempToggle.Length; i++)
                if (_tempToggle[i].GetComponent<UI_CToggleSubframe>())
                    ToggleDictionary.Add(_tempToggle[i].GetComponent<UI_CToggleSubframe>().EnumName, _tempToggle[i].GetComponent<UI_CToggleSubframe>());

        }


        #region Components

        public UI_CTMProGUISubframe GetText(object frameEnum) {
            if (!TMProDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return TMProDictionary[frameEnum.ToString()];
        }

        public UI_CSliderSubframe GetSlider(object frameEnum) {
            if (!SliderDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return SliderDictionary[frameEnum.ToString()];
        }

        public UI_CButtonTMProSubframe GetButton(object frameEnum) {
            if (!ButtonDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return ButtonDictionary[frameEnum.ToString()];
        }

        public UI_CImageSubframe GetImage(object frameEnum) {
            if (!ImageDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return ImageDictionary[frameEnum.ToString()];
        }

        public UI_CPanelSubframe GetPanel(object frameEnum) {
            if (!PanelDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return PanelDictionary[frameEnum.ToString()];
        }

        public UI_CToggleSubframe GetToggle(object frameEnum) {
            if (!ToggleDictionary.ContainsKey(frameEnum.ToString())) Debug.LogError("The " + frameEnum + " Component Cound't be found");
            return ToggleDictionary[frameEnum.ToString()];
        }

        #endregion

#if UNITY_EDITOR
        [Button("Set SubComponents")]
        public void SetSubComponents() {
            SubComponents = new List<UI_TComponentsSubframe>();
            GetAllSubComponents(transform);
            foreach (var item in SubComponents) item.SetupComponentSubframe(this);
        }

        private void GetAllSubComponents(Transform item) {
            foreach (Transform child in item) {
                if (child.GetComponent<UI_TComponentsSubframe>()) SubComponents.Add(child.GetComponent<UI_TComponentsSubframe>());
                if (child.childCount > 0) GetAllSubComponents(child);
            }
        }
#endif
    }
}