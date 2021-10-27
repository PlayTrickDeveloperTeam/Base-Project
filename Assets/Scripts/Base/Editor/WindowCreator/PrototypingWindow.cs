using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Base
{
    public class PrototypingWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Prototype Window/Prototype Window %F1")]
        private static void OpenWindow()
        {
            GetWindow<PrototypingWindow>().Show();
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            //tree.Add("Enum Creator", new EnumCreator());
            return tree;
        }
    }
}