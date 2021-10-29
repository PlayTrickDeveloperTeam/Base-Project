using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
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
        Dictionary<string, B_UI_CTMProGUISubframe> TMProDictionary;
        Dictionary<string, B_UI_CSlider_Subframe> SliderDictionary;

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
            //return Task.CompletedTask;
        }

        public virtual Task FlushFrameData()
        {
            Parent = null;
            SubComponents = new List<B_UI_ComponentsSubframe>();
            return Task.CompletedTask;
        }



        void SetupDictionaries()
        {
            TMProDictionary = new Dictionary<string, B_UI_CTMProGUISubframe>();
            B_UI_ComponentsSubframe[] _tempTMPro = SubComponents.Where(t => t.GetComponent<B_UI_CTMProGUISubframe>()).ToArray();
            for (int i = 0; i < _tempTMPro.Length; i++)
            {
                if (_tempTMPro[i].GetComponent<B_UI_CTMProGUISubframe>())
                {
                    TMProDictionary.Add(_tempTMPro[i].GetComponent<B_UI_CTMProGUISubframe>().EnumName, _tempTMPro[i].GetComponent<B_UI_CTMProGUISubframe>());
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
        }

        #region Components

        public B_UI_CTMProGUISubframe GetText(object frameEnum)
        {
            return TMProDictionary[frameEnum.ToString()];
        }

        public B_UI_CSlider_Subframe GetSlider(object frameEnum)
        {
            Debug.Log(frameEnum.ToString());
            return SliderDictionary[frameEnum.ToString()];
        }

        #endregion

#if UNITY_EDITOR
        [Button("Set SubComponents")]
        public void SetSubComponents()
        {
            SubComponents = new List<B_UI_ComponentsSubframe>();
            foreach (Transform item in transform)
            {
                if (item.GetComponent<B_UI_ComponentsSubframe>())
                {
                    SubComponents.Add(item.GetComponent<B_UI_ComponentsSubframe>());
                    item.GetComponent<B_UI_ComponentsSubframe>().SetupComponentSubframe(this);
                }
            }
        }
#endif
    }
}