using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace PlayTrick.Tools
{
    public class SC_Create
    {
        public SC_Create()
        {
            SelectNameSpaces = new List<string>();
            BaseExtentions = new List<string>();
            BaseExtentions = EnumProvider.SC_Provider.FindNames("Base");
            NameSpaceList = new List<string>();
            NameSpaceList = EnumProvider.SC_Provider.P_NameSpaces();
            Paths = EnumProvider.SC_Provider.GetDirectories();
        }
        [ValueDropdown("Paths")]
        public string ScriptCreatePath;
        List<string> Paths;
        [ValueDropdown("NameSpaceList")]
        public List<string> SelectNameSpaces;
        List<string> NameSpaceList;
        public string NameSpace = "";
        public string ClassName = "";

        public bool HasExtention = true;
        [ValueDropdown("BaseExtentions")]
        public string Extention = "";
        private List<string> BaseExtentions;

        [Button("Create New Script")]
        public void GenerateScript()
        {
            string FilePath = ScriptCreatePath + "/" + ClassName + ".cs";
            string FileFormat = "";
            if (SelectNameSpaces.Count <= 0) SelectNameSpaces.Add("UnityEngine");
            if (HasExtention && Extention == "") Extention = "MonoBehaviour";
            List<string> Temp_NameSpaces = new List<string>();
            for (int i = 0; i < SelectNameSpaces.Count; i++)
            {
                string X = string.Concat("using ", SelectNameSpaces[i].ToString(), ";");
                Temp_NameSpaces.Add(X);

            }
            for (int i = 0; i < Temp_NameSpaces.Count; i++)
            {
                FileFormat += Temp_NameSpaces[i];
            }

            FileFormat += "\n" + "namespace " + NameSpace + "\n{";
            if (!HasExtention)
                FileFormat += "public class " + ClassName + "\n" + "{" + "\n" + "\n" + "}" + "\n}";
            else
                FileFormat += "public class " + ClassName + " : " + Extention + "\n" + "{" + "\n" + "\n" + "\n" + "}" + "\n}";

            File.WriteAllText(FilePath, FileFormat);
            AssetDatabase.Refresh();

        }
    }
}
