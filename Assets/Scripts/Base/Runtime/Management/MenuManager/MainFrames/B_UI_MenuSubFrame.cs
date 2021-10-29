using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Base.UI
{
    public abstract class B_UI_MenuSubFrame : MonoBehaviour
    {
        B_UI_ManagerMainFrame Parent;
        public Enum_MenuTypes MenuType;
        public List<B_UI_ComponentsSubframe> SubComponents;

        public Task SetupFrame(B_UI_ManagerMainFrame Mainframe)
        {
            this.Parent = Mainframe;
#if UNITY_EDITOR
            SetSubComponents();
#endif
            return Task.CompletedTask;
        }

        public Task FlushFrameData()
        {
            Parent = null;
            SubComponents = new List<B_UI_ComponentsSubframe>();
            return Task.CompletedTask;
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
                }
            }
        }
#endif
    }
}