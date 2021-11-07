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
namespace Base {
    public class SaveSystemEditor {

        List<SaveObject> SaveObjects;
        Dictionary<string, SaveObject> savesDic;
        public Task SaveSystemStrapping() {
            SaveObjects = new List<SaveObject>();
            SaveObjects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            return Task.CompletedTask;
        }

        public SaveObject GetSaveObject(object obj) {
            if (savesDic.ContainsKey(obj.ToString())) { return savesDic[obj.ToString()]; }
            return null;
        }

        public SaveSystemEditor() {
            SaveObjects = new List<SaveObject>();
            SaveObjects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            savesDic = new Dictionary<string, SaveObject>();
            for (int i = 0; i < SaveObjects.Count; i++) {
                SaveObjects[i].LoadThisData();
                savesDic.Add(SaveObjects[i].SaveName, SaveObjects[i]);
            }
        }

        public void SaveAllData() {
            foreach (var item in SaveObjects) {
                item.SaveThisData();
            }
        }


        #region Editor Functions
#if UNITY_EDITOR
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public SaveObject NewSaveObject;
        public SaveSystemEditor(OdinMenuTree tree) {
            SaveObjects = new List<SaveObject>();
            SaveObjects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            savesDic = new Dictionary<string, SaveObject>();
            for (int i = 0; i < SaveObjects.Count; i++) {
                savesDic.Add(SaveObjects[i].SaveName, SaveObjects[i]);
            }
            tree.AddAllAssetsAtPath("Saves", "Assets/Resources/SaveAssets");
            NewSaveObject = ScriptableObject.CreateInstance<SaveObject>();
        }

        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        [Button]
        public void CreateNewSave() {
            SaveObject obj = NewSaveObject;
            if (obj.SaveName.MakeViable() == "") {
                obj.SaveName = "Empty_Save_Name";
            }
            else obj.SaveName = obj.SaveName.MakeViable();
            obj.name = obj.SaveName;
            obj.SaveThisData();
            AssetDatabase.CreateAsset(obj, "Assets/Resources/SaveAssets/" + obj.SaveName + ".asset");
            AssetDatabase.SaveAssets();
            NewSaveObject = ScriptableObject.CreateInstance<SaveObject>();
        }
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [Button]
        public void ClearAllSaves() {
            string[] assetsPath = { "Assets/Resources/SaveAssets" };
            foreach (var item in AssetDatabase.FindAssets("", assetsPath)) {

                var path = AssetDatabase.GUIDToAssetPath(item);
                SaveObject var = AssetDatabase.LoadAssetAtPath(path, typeof(SaveObject)) as SaveObject;
                if (!var.IsPermanent) {
                    var.DeleteThisData();
                    AssetDatabase.DeleteAsset(path);
                }

            }
            AssetDatabase.SaveAssets();
        }
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        [Button]
        public void CreateEnums() {
            string[] _temp = new string[SaveObjects.Count];
            for (int i = 0; i < SaveObjects.Count; i++) {
                _temp[i] = SaveObjects[i].SaveName;
                SaveObjects[i].CreateEnums();
            }
            EnumCreator.CreateEnum("Saves", _temp);
        }
#endif
        #endregion
    }
}