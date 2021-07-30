using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace PlayTrick.Tools
{
    public class SC_CreateWindow : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Script Creation/Template Editor %F2")]
        private static void OpenWindow()
        {
            GetWindow<SC_CreateWindow>().Show();

        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Add("Create new script", new SC_Create());
            tree.Add("Create from template", new SC_Template());

            return tree;
        }


    }
}