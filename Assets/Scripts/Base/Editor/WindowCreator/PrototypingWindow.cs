using Sirenix.OdinInspector.Editor;
using UnityEditor;
namespace Base {
    public class PrototypingWindow : OdinMenuEditorWindow {
        [MenuItem("Tools/Prototype Window/Prototype Window %F1")]
        private static void OpenWindow() {
            GetWindow<PrototypingWindow>().Show();
        }
        protected override OdinMenuTree BuildMenuTree() {
            var tree = new OdinMenuTree();
            //tree.Add("Save System", new SaveSystemEditor(tree));
            return tree;
        }
    }
}