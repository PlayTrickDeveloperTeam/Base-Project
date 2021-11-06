using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
#endif
namespace Base
{
    public class SaveSystemEditor
    {
        List<SaveObject> Objects;
        Dictionary<string, SaveObject> savesDic;
        public Task SaveSystemStrapping()
        {
            Objects = new List<SaveObject>();
            Objects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            return Task.CompletedTask;
        }

        public SaveObject GetSaveObject(object obj)
        {
            if (savesDic.ContainsKey(obj.ToString())) { return savesDic[obj.ToString()]; }
            return null;
        }

        public SaveSystemEditor()
        {
            Objects = new List<SaveObject>();
            Objects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            savesDic = new Dictionary<string, SaveObject>();
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].LoadThisData();
                savesDic.Add(Objects[i].SaveName, Objects[i]);
            }
        }

        public void SaveAllData()
        {
            foreach (var item in Objects)
            {
                item.SaveThisData();
            }
        }


        #region Editor Functions
#if UNITY_EDITOR
        public SaveSystemEditor(OdinMenuTree tree)
        {
            Objects = new List<SaveObject>();
            Objects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            savesDic = new Dictionary<string, SaveObject>();
            for (int i = 0; i < Objects.Count; i++)
            {
                savesDic.Add(Objects[i].SaveName, Objects[i]);
            }
            tree.AddAllAssetsAtPath("Saves", "Assets/Resources/SaveAssets");
        }

        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        [Button]
        public void CreateNewSave()
        {
            SaveObject obj = ScriptableObject.CreateInstance<SaveObject>();
            obj.name = "Save_" + (Objects.Count + 1);
            obj.SaveName = obj.name;
            AssetDatabase.CreateAsset(obj, "Assets/Resources/SaveAssets/" + obj.name + ".asset");
            AssetDatabase.SaveAssets();
        }
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [Button]
        public void ClearAllSaves()
        {
            string[] assetsPath = { "Assets/Resources/SaveAssets" };
            foreach (var item in AssetDatabase.FindAssets("", assetsPath))
            {
                var path = AssetDatabase.GUIDToAssetPath(item);
                AssetDatabase.DeleteAsset(path);
            }
            AssetDatabase.SaveAssets();
        }
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [Button]
        public void CreateEnums()
        {
            string[] _temp = new string[Objects.Count];
            for (int i = 0; i < Objects.Count; i++)
            {
                _temp[i] = Objects[i].name;
            }
            EnumCreator.CreateEnum("Saves", _temp);
        }
#endif
        #endregion
    }
}