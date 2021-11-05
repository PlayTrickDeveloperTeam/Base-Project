using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
namespace Base
{
    public static class EnumCreator
    {
#if UNITY_EDITOR
        static string BasePath = "Assets/Resources/EnumStorage/";

        public static void CreateEnum(string ItemName, string[] ItemsToEnum)
        {
            string Item = ItemName + ".cs";
            string AllPath = BasePath + Item;

            string FileInside = "public enum Enum_" + ItemName + "{";
            if (ItemsToEnum.Length > 0)
                foreach (var item in ItemsToEnum)
                {
                    FileInside += " " + item;
                    if (item != ItemsToEnum.Last())
                        FileInside += ",";
                    else { FileInside += "}"; }
                }
            else { FileInside += "}"; }
            File.WriteAllText(AllPath, FileInside);
            AssetDatabase.Refresh();

        }
#endif
    }
}