using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using System.IO;
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
        [BoxGroup("New Save Object", centerLabel: true, order: 0)]
        [InfoBox("$InfoBoxString", "IsntReadyForSave")]
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public SaveObject NewSaveObject;

        public SaveSystemEditor(OdinMenuTree tree) {
            SaveObjects = new List<SaveObject>();
            SaveObjects = Resources.LoadAll<SaveObject>("SaveAssets").ToList();
            savesDic = new Dictionary<string, SaveObject>();
            for (int i = 0; i < SaveObjects.Count; i++) {
                savesDic.Add(SaveObjects[i].SaveName, SaveObjects[i]);
            }

            NewSaveObject = ScriptableObject.CreateInstance<SaveObject>();
        }

        [ShowIf("IsReadyForSave")]
        [VerticalGroup("Main Functions")]
        [GUIColor("getGreen")]
        [Button]
        public void CreateNewSave() {
            SaveObject obj = NewSaveObject;
            if (obj.SaveName.MakeViable() == "") {
                obj.SaveName = "Empty_Save_Name";
            }
            else obj.SaveName = obj.SaveName.MakeViable();
            obj.name = obj.SaveName;
            obj.SaveThisData();
            obj.Created = true;
            AssetDatabase.CreateAsset(obj, "Assets/Resources/SaveAssets/" + obj.SaveName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            SaveObjects.Add(obj);
            CreateEnums();
            NewSaveObject = ScriptableObject.CreateInstance<SaveObject>();
        }

        [VerticalGroup("Main Functions")]
        [GUIColor("getRed")]
        [Button]
        public void ClearAllSaves() {
            string[] assetsPath = { "Assets/Resources/SaveAssets" };
            foreach (var item in AssetDatabase.FindAssets("", assetsPath)) {

                var path = AssetDatabase.GUIDToAssetPath(item);
                SaveObject _saveObject = AssetDatabase.LoadAssetAtPath(path, typeof(SaveObject)) as SaveObject;



                if (!_saveObject.IsPermanent) {
                    SaveObjects.Remove(_saveObject);
                    Debug.Log(_saveObject.SaveEnumLocations);
                    File.Delete(_saveObject.SaveEnumLocations);
                    AssetDatabase.DeleteAsset(path);
                    _saveObject.DeleteThisData();
                }

            }
            CreateEnums();
            AssetDatabase.SaveAssets();
        }
        [HideIf("IsReadyForSave")]
        [VerticalGroup("Main Functions")]
        [GUIColor("getGray")]
        [Button]
        public void CreateEnums() {
            string[] _temp = new string[SaveObjects.Count];
            for (int i = 0; i < SaveObjects.Count; i++) {
                _temp[i] = SaveObjects[i].SaveName;
                SaveObjects[i].CreateEnums();
            }
            EnumCreator.CreateEnum("Saves", _temp);
        }

        bool IsReadyForSave() {
            if (NewSaveObject == null) return false;
            if (string.IsNullOrEmpty(NewSaveObject.SaveName)) return false;
            if (NewSaveObject.SaveName.Length <= 3) return false;
            if (NewSaveObject.SaveCluster.Count < 1) return false;
            for (int i = 0; i < NewSaveObject.SaveCluster.Count; i++) {
                if (NewSaveObject.SaveCluster[i].Name.IsVaibleForSave() != B_Extention_Management.SaveNameViabilityStatus.Viable) return false;
            }
            return true;
        }

        bool IsntReadyForSave() {
            return !IsReadyForSave();
        }

        public string InfoBoxString() {
            if (NewSaveObject == null) return null;
            if (string.IsNullOrEmpty(NewSaveObject.SaveName) || NewSaveObject.SaveName.Length <= 3) return "Enter A Name";
            if (NewSaveObject.SaveCluster.Count < 1) return "Enter Atleast One Data";
            for (int i = 0; i < NewSaveObject.SaveCluster.Count; i++) {
                if (NewSaveObject.SaveCluster[i].Name.IsVaibleForSave() != B_Extention_Management.SaveNameViabilityStatus.Viable) return "Enter Atleast One Data";
            }
            return null;
        }

        private Color GetButtonColor() {
            Sirenix.Utilities.Editor.GUIHelper.RequestRepaint();
            return Color.HSVToRGB(Mathf.Cos((float)UnityEditor.EditorApplication.timeSinceStartup + 1f) * 0.225f + 0.325f, 1, 1);
        }

        #region Inspector Window Functions

        private Color getGreen() => Color.green;
        private Color getRed() => Color.red;
        private Color getYellow() => Color.yellow;
        private Color getGray() => Color.gray;
        #endregion
#endif
        #endregion
    }
}