using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
namespace Base
{
    public class SE_Window : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Save Editor/Save Editor Window %F1")]
        private static void OpenWindow()
        {
            GetWindow<SE_Window>().Show();
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Add("Save Editor", new SE_Editor());

            return tree;
        }
    }
}