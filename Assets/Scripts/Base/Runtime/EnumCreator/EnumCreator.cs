using System.IO;
using System.Linq;
using UnityEditor;
namespace Base {
    public static class EnumCreator {
#if UNITY_EDITOR
        public static string BasePath = "Assets/Resources/EnumStorage/";

        public static void CreateEnum(string ItemName, string[] ItemsToEnum) {
            var Item = ItemName + ".cs";
            var AllPath = BasePath + Item;

            var FileInside = "public enum Enum_" + ItemName + "{";
            if (ItemsToEnum.Length > 0)
                foreach (var item in ItemsToEnum) {
                    FileInside += " " + item;
                    if (item != ItemsToEnum.Last())
                        FileInside += ",";
                    else FileInside += "}";
                }
            else FileInside += "}";
            File.WriteAllText(AllPath, FileInside);
            AssetDatabase.Refresh();

        }
#endif
    }
}