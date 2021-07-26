using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace PlayTrick.Tools
{
    public class SC_Template
    {
        public SC_Template()
        {
            TemplateList = EnumProvider.SC_Provider.FindTemplates("Template_");
            Paths = EnumProvider.SC_Provider.GetDirectories();
        }

        [ValueDropdown("Paths")]
        public string ScriptCreatePath;
        [ValueDropdown("TemplateList")]
        public string Template;
        List<string> TemplateList = new List<string>();
        List<string> Paths = new List<string>();
        public string ClassNameAddition = "";

        [Button("Create From Template")]
        public void CreateFromTemplateFunction()
        {
            if (Template.Length <= 0) { Debug.Log("Please Enter A Template"); return; }
            if (ScriptCreatePath.Length <= 0) { Debug.Log("Please Enter A Path"); return; }
            string createdAssetPath = ScriptCreatePath + "/" + Template + ClassNameAddition + ".cs";

            string[] assetGuid = AssetDatabase.FindAssets(Template);
            string AssetLocation = AssetDatabase.GUIDToAssetPath(assetGuid[0]);
            string TemplateCopy = File.ReadAllText(AssetLocation);
            string TemplateCopy2 = "";
            string[] Lines = TemplateCopy.Split("\n"[0]);
            foreach (var item in Lines)
            {
                if (item.Contains("public class "))
                {
                    TemplateCopy2 += item.Replace("public class " + Template, "public class " + Template + ClassNameAddition);
                    continue;
                }
                TemplateCopy2 += item;
            }
            Debug.Log(TemplateCopy2);
            File.WriteAllText(createdAssetPath, TemplateCopy2);
            AssetDatabase.Refresh();

        }

    }
}
