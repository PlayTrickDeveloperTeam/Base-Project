using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Base {
    [Serializable]
    public class SaveObject : ScriptableObject {

        [DisableIf("Created")]
        public string SaveName;
        public bool IsPermanent;
        [HideInInspector] public bool Created;
        public List<DataHolder> SaveCluster;
        [HideInInspector] public string SaveEnumLocations;
        public Dictionary<string, object> saveDic;

        public object GetData(object name) {
            if (saveDic.ContainsKey(name.ToString())) return saveDic[name.ToString()];
            return null;
        }

        public void SetData(object name, object value) {
            if (saveDic.ContainsKey(name.ToString())) saveDic[name.ToString()] = value.ToString();
        }
        
        public void CreateEnums() {
            var _temp = new string[SaveCluster.Count];
            for (var i = 0; i < SaveCluster.Count; i++) {
                if (SaveCluster[i].Name.Length <= 0) continue;
                _temp[i] = SaveCluster[i].Name;
            }
#if UNITY_EDITOR
            SaveEnumLocations = EnumCreator.BasePath + SaveName + ".cs";
            EnumCreator.CreateEnum(SaveName, _temp);
#endif
        }
        [ShowIf("Created")]
        [Button]
        public void SaveThisData() {
            if (!Application.isPlaying) {
                saveDic = new Dictionary<string, object>();
                for (var i = 0; i < SaveCluster.Count; i++) {
                    if (SaveCluster[i].Name.MakeViable() == "") SaveCluster[i].Name = "EmptyData_" + i;
                    else SaveCluster[i].Name = SaveCluster[i].Name.MakeViable();
                    saveDic.Add(SaveCluster[i].Name, SaveCluster[i].Value);
                }
            }
            else {
                for (var i = 0; i < SaveCluster.Count; i++) SaveCluster[i].Value = saveDic[SaveCluster[i].Name].ToString();
            }
            CreateEnums();
            SaveObjectData();
        }
        [ShowIf("Created")]
        [Button]
        public void LoadThisData() {
            LoadObjectData();
            saveDic = new Dictionary<string, object>();
            for (var i = 0; i < SaveCluster.Count; i++) saveDic.Add(SaveCluster[i].Name, SaveCluster[i].Value);
        }

        [ShowIf("Created")]
        [Button("Delete Save Data")]
        public void DeleteThisData() {
            DeleteObjectData();
        }


        #region Save and Load Functions

        public Task SaveObjectData() {
            var obj = this;
            var formatter = new BinaryFormatter();
            var MainPath = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
            var file = new FileStream(MainPath + obj.SaveName + ".save", FileMode.Create);
            var json = JsonUtility.ToJson(obj);
            formatter.Serialize(file, json);
            file.Close();
            return Task.CompletedTask;
        }
        // Save null check özelliği ekle
        // Otamatik hale gelebilecek bi özellik
        // Vector3 alabilecek bi hale gelsin
        public Task LoadObjectData() {
            var obj = this;
            var formatter = new BinaryFormatter();
            var MainPath = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves")) Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
            if (!File.Exists(MainPath + obj.SaveName + ".save")) {
                var file = new FileStream(MainPath + obj.SaveName + ".save", FileMode.Create);
                var json = JsonUtility.ToJson(obj);
                formatter.Serialize(file, json);
                file.Close();
            }
            var stream = File.Open(MainPath + obj.SaveName + ".save", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), obj);
            stream.Close();
            return Task.CompletedTask;
        }

        public Task DeleteObjectData() {
            var MainPath = Application.persistentDataPath + "/Saves/";
            if (File.Exists(MainPath + SaveName + ".save")) File.Delete(MainPath + SaveName + ".save");
            return Task.CompletedTask;
        }

        #endregion
    }
    [Serializable]
    public class DataHolder {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        public string Name = "";
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        public string Value = "";
    }
}