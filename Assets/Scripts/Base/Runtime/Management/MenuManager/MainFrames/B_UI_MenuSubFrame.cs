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

        public B_UI_CTMProGUISubframe GetText(Enum_UIComponentFrames frameEnum)
        {
            return TMProDictionary[frameEnum.ToString()];
        }

        void SetupDictionaries()
        {
            TMProDictionary = new Dictionary<string, B_UI_CTMProGUISubframe>();
            B_UI_ComponentsSubframe[] _temp = SubComponents.Where(t => t.GetComponent<B_UI_CTMProGUISubframe>()).ToArray();
            for (int i = 0; i < _temp.Length; i++)
            {
                if (_temp[i].GetComponent<B_UI_CTMProGUISubframe>())
                {
                    TMProDictionary.Add(_temp[i].GetComponent<B_UI_CTMProGUISubframe>().EnumName, _temp[i].GetComponent<B_UI_CTMProGUISubframe>());
                }
            }
        }

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