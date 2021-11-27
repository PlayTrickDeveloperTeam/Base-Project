using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
namespace Base {
    public class SaveSystemWindow : OdinMenuEditorWindow {
        [MenuItem("Tools/Save System/Save Editor %F2")]
        private static void OpenWindow() {
            GetWindow<SaveSystemWindow>().Show();
        }
        protected override OdinMenuTree BuildMenuTree() {
            var tree = new OdinMenuTree();
            tree.Add("Save System", new SaveSystemEditor(tree), EditorIcons.DayCalendar);
            tree.AddAllAssetsAtPath("Saves", "Assets/Resources/SaveAssets");
            return tree;
        }
    }
}