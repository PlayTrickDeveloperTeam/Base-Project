using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Base.UI
{
    public abstract class UI_TComponentsSubframe : MonoBehaviour
    {
        [HideInInspector] public string ComponentParticularName;
        [HideInInspector] public string EnumName;
        [SerializeField] B_UI_MenuSubFrame Parent;
        public virtual Task SetupComponentSubframe(B_UI_MenuSubFrame Manager)
        {
            this.Parent = Manager;
            this.ComponentParticularName = this.gameObject.name;
#if UNITY_EDITOR
            EditorFunctions();
#endif
            return Task.CompletedTask;
        }

        public virtual Task FlushData()
        {
            Parent = null;
            return Task.CompletedTask;
        }


        void EditorFunctions()
        {

            if (PrefabUtility.GetPrefabType(this.gameObject) == PrefabType.PrefabInstance)
                PrefabUtility.UnpackPrefabInstance(this.gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }
    }
}