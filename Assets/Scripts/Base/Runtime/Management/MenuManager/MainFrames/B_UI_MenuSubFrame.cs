using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif
namespace Base.UI
{
    public abstract class B_UI_MenuSubFrame : MonoBehaviour
    {
        B_UI_ManagerMainFrame Parent;
        public Enum_MenuTypes MenuType;
        public List<B_UI_ComponentsSubframe> SubComponents;
        Dictionary<string, B_UI_CTMProGUI_Subframe> TMProDictionary;
        Dictionary<string, B_UI_CSlider_Subframe> SliderDictionary;
        Dictionary<string, B_UI_CTMProGUIButton_Subframe> ButtonDictionary;
        Dictionary<string, B_UI_CImage_Subframe> ImageDictionary;
        public virtual async Task SetupFrame(B_UI_ManagerMainFrame Mainframe)
        {
            this.Parent = Mainframe;
#if UNITY_EDITOR
            SetSubComponents();
            if (EditorApplication.isPlaying)
                SetupDictionaries();
#else
                SetupDictionaries();
#endif
        }

        public virtual Task FlushFrameData()
        {
            Parent = null;
            SubComponents = new List<B_UI_ComponentsSubframe>();
            return Task.CompletedTask;
        }

        public virtual Tween EnableUI(float Time = 0, bool Snap = true)
        {
            return MoveUI(Vector3.zero, Time, Snap);
        }

        public virtual Tween DisableUI(float Time = 0, bool Snap = true)
        {
            return MoveUI(Vector3.one * 5000, Time, Snap);
        }

        public Tween MoveUI(Vector3 Position, float Time = 0, bool Snap = true)
        {
            return GetComponent<RectTransform>().DOLocalMove(Position, Time, Snap);
        }



        void SetupDictionaries()
        {
            TMProDictionary = new Dictionary<string, B_UI_CTMProGUI_Subframe>();
            B_UI_ComponentsSubframe[] _tempTMPro = SubComponents.Where(t => t.GetComponent<B_UI_CTMProGUI_Subframe>()).ToArray();
            for (int i = 0; i < _tempTMPro.Length; i++)
            {
                if (_tempTMPro[i].GetComponent<B_UI_CTMProGUI_Subframe>())
                {
                    TMProDictionary.Add(_tempTMPro[i].GetComponent<B_UI_CTMProGUI_Subframe>().EnumName, _tempTMPro[i].GetComponent<B_UI_CTMProGUI_Subframe>());
                }
            }

            SliderDictionary = new Dictionary<string, B_UI_CSlider_Subframe>();
            B_UI_ComponentsSubframe[] _tempSlider = SubComponents.Where(t => t.GetComponent<B_UI_CSlider_Subframe>()).ToArray();
            for (int i = 0; i < _tempSlider.Length; i++)
            {
                if (_tempSlider[i].GetComponent<B_UI_CSlider_Subframe>())
                {
                    SliderDictionary.Add(_tempSlider[i].GetComponent<B_UI_CSlider_Subframe>().EnumName, _tempSlider[i].GetComponent<B_UI_CSlider_Subframe>());
                }
            }

            ButtonDictionary = new Dictionary<string, B_UI_CTMProGUIButton_Subframe>();
            B_UI_ComponentsSubframe[] _tempButton = SubComponents.Where(t => t.GetComponent<B_UI_CTMProGUIButton_Subframe>()).ToArray();
            for (int i = 0; i < _tempButton.Length; i++)
            {
                if (_tempButton[i].GetComponent<B_UI_CTMProGUIButton_Subframe>())
                {
                    ButtonDictionary.Add(_tempButton[i].GetComponent<B_UI_CTMProGUIButton_Subframe>().EnumName, _tempButton[i].GetComponent<B_UI_CTMProGUIButton_Subframe>());
                }
            }

            ImageDictionary = new Dictionary<string, B_UI_CImage_Subframe>();
            B_UI_ComponentsSubframe[] _tempImage = SubComponents.Where(t => t.GetComponent<B_UI_CImage_Subframe>()).ToArray();
            for (int i = 0; i < _tempImage.Length; i++)
            {
                if (_tempImage[i].GetComponent<B_UI_CImage_Subframe>())
                {
                    ImageDictionary.Add(_tempImage[i].GetComponent<B_UI_CImage_Subframe>().EnumName, _tempImage[i].GetComponent<B_UI_CImage_Subframe>());
                }
            }
        }

        #region Components

        public B_UI_CTMProGUI_Subframe GetText(object frameEnum)
        {
            return TMProDictionary[frameEnum.ToString()];
        }

        public B_UI_CSlider_Subframe GetSlider(object frameEnum)
        {
            return SliderDictionary[frameEnum.ToString()];
        }

        public B_UI_CTMProGUIButton_Subframe GetButton(object frameEnum)
        {
            return ButtonDictionary[frameEnum.ToString()];
        }

        public B_UI_CImage_Subframe GetImage(object frameEnum)
        {
            return ImageDictionary[frameEnum.ToString()];
        }

        #endregion

#if UNITY_EDITOR
        [Button("Set SubComponents")]
        public void SetSubComponents()
        {
            SubComponents = new List<B_UI_ComponentsSubframe>();
            GetAllSubComponents(transform);
            //foreach (Transform item in transform)
            //{
            //    if (item.GetComponent<B_UI_ComponentsSubframe>())
            //    {
            //        SubComponents.Add(item.GetComponent<B_UI_ComponentsSubframe>());
            //        item.GetComponent<B_UI_ComponentsSubframe>().SetupComponentSubframe(this);
            //    }
            //}
            foreach (var item in SubComponents)
            {
                item.SetupComponentSubframe(this);
            }
        }

        void GetAllSubComponents(Transform item)
        {
            foreach (Transform child in item)
            {
                if (child.GetComponent<B_UI_ComponentsSubframe>())
                {
                    SubComponents.Add(child.GetComponent<B_UI_ComponentsSubframe>());
                }
                if (child.childCount > 0) { GetAllSubComponents(child); }
            }
        }
#endif
    }
}