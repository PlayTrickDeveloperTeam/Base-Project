using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Base.UI {
    public abstract class UI_TComponentsSubframe : MonoBehaviour {
        
        [HideInInspector] public string ComponentParticularName;
        [HideInInspector] public string EnumName;
        [SerializeField] private B_UI_MenuSubFrame Parent;

        private bool Moved;
        private Vector3 OriginalPosition;

        public virtual Task SetupComponentSubframe(B_UI_MenuSubFrame Manager) {
            Parent = Manager;
            ComponentParticularName = gameObject.name;
            OriginalPosition = GetComponent<RectTransform>().localPosition;
#if UNITY_EDITOR
            EditorFunctions();
#endif
            return Task.CompletedTask;
        }

        public virtual void ActivatePart() {
            MoveSubframe(OriginalPosition);
        }

        public virtual void DeactivatePart() {
            MoveSubframe(new Vector3(3000, 0, 0));
        }

        public virtual void MoveSubframe(Vector3 Position) {
            GetComponent<RectTransform>().DOLocalMove(Position, 0);
        }

        public virtual Task FlushData() {
            Parent = null;
            return Task.CompletedTask;
        }

#if UNITY_EDITOR
        private void EditorFunctions() {

            if (PrefabUtility.GetPrefabType(gameObject) == PrefabType.PrefabInstance)
                PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        }
#endif
    }
}