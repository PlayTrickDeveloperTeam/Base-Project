using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;

namespace EnumProvider
{
    public static class SC_Provider
    {
        public static List<string> P_NameSpaces()
        {
            List<string> TempList = new List<string>();
            TempList.Add("System");
            TempList.Add("System.IO");
            TempList.Add("System.Collections");
            TempList.Add("System.Collections.Generic");
            TempList.Add("System.Linq");
            TempList.Add("Unity");
            TempList.Add("Unity.Mathematics");
            TempList.Add("UnityEngine");
            TempList.Add("UnityEngine.UI");
            TempList.Add("UnityEngine.AI");
            TempList.Add("UnityEditor");
            TempList.Add("Sirenix");
            TempList.Add("Sirenix.OdinInspector");
            TempList.Add("Sirenix.OdinInspector.Editor");
            TempList.Add("Sirenix.Utilities.Editor");

            return TempList;
        }

        public static List<string> FindNames(string ToFindObjects)
        {
            string[] NamesWithLocations = AssetDatabase.FindAssets(ToFindObjects, new[] { "Assets/Scripts/Editor" });
            List<string> newNamesWithLocation = new List<string>();
            newNamesWithLocation.Add("MonoBehaviour");
            newNamesWithLocation.Add("ScriptableObject");
            foreach (var item in NamesWithLocations)
            {
                newNamesWithLocation.Add(System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(item)));
            }
            return newNamesWithLocation;
        }

        public static List<string> FindTemplates(string ToFindObjects)
        {
            string[] NamesWithLocations = AssetDatabase.FindAssets(ToFindObjects, new[] { "Assets/Scripts/Editor" });
            List<string> newNamesWithLocation = new List<string>();
            foreach (var item in NamesWithLocations)
            {
                newNamesWithLocation.Add(System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(item)));
            }
            return newNamesWithLocation;
        }

        public static List<string> GetDirectories()
        {
            string[] BasePaths = AssetDatabase.GetSubFolders("Assets/Scripts");
            List<string> Paths = new List<string>();
            for (int i = 0; i < BasePaths.Length; i++)
            {
                Paths.Add(BasePaths[i]);
                Recursive(BasePaths[i], Paths);
            }
            return Paths;
        }

        static void Recursive(string folder, List<string> Parent)
        {
            var folders = AssetDatabase.GetSubFolders(folder);
            for (int i = 0; i < folders.Length; i++)
            {
                Parent.Add(folders[i]);
                Recursive(folders[i], Parent);
            }
        }

    }
}