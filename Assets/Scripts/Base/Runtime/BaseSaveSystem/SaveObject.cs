﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Base {
    [System.Serializable]
    public class SaveObject : ScriptableObject {
        public string SaveName;
        public bool IsPermanent;
        public List<DataHolder> SaveCluster;
        Dictionary<string, object> saveDic;

        public object GetData(object name) {
            if (saveDic.ContainsKey(name.ToString())) { return saveDic[name.ToString()]; }
            else return null;
        }

        public void SetData(object name, object value) {
            if (saveDic.ContainsKey(name.ToString())) {
                saveDic[name.ToString()] = value.ToString();
            }
        }

        [Button]
        public void CreateEnums() {
            string[] _temp = new string[SaveCluster.Count];
            for (int i = 0; i < SaveCluster.Count; i++) {
                if (SaveCluster[i].Name.Length <= 0) continue;
                _temp[i] = SaveCluster[i].Name;
            }
            EnumCreator.CreateEnum(SaveName, _temp);
        }

        [Button]
        public void SaveThisData() {
            if (!Application.isPlaying) {
                saveDic = new Dictionary<string, object>();
                for (int i = 0; i < SaveCluster.Count; i++) {
                    if (SaveCluster[i].Name.Length <= 3 || SaveCluster[i].Name == null || SaveCluster[i].Name == "Null") {
                        SaveCluster[i].Name = "EmptyData_" + i.ToString();
                    }
                    saveDic.Add(SaveCluster[i].Name, SaveCluster[i].Value);
                }
            }
            else {
                for (int i = 0; i < SaveCluster.Count; i++) {
                    SaveCluster[i].Value = saveDic[SaveCluster[i].Name].ToString();
                }
            }
            this.SaveObjectData();
        }

        [Button]
        public void LoadThisData() {
            this.LoadObjectData();
            saveDic = new Dictionary<string, object>();
            for (int i = 0; i < SaveCluster.Count; i++) {
                saveDic.Add(SaveCluster[i].Name, SaveCluster[i].Value);
            }
        }


        #region Save and Load Functions
        public Task SaveObjectData() {
            SaveObject obj = this;
            BinaryFormatter formatter = new BinaryFormatter();
            string MainPath = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
            FileStream file = new FileStream(MainPath + obj.SaveName + ".save", FileMode.Create);
            var json = JsonUtility.ToJson(obj);
            formatter.Serialize(file, json);
            file.Close();
            return Task.CompletedTask;
        }

        public Task LoadObjectData() {
            SaveObject obj = this;
            BinaryFormatter formatter = new BinaryFormatter();
            string MainPath = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves")) {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
            }
            if (!File.Exists(MainPath + obj.SaveName + ".save")) {
                FileStream file = new FileStream(MainPath + obj.SaveName + ".save", FileMode.Create);
                var json = JsonUtility.ToJson(obj);
                formatter.Serialize(file, json);
                file.Close();
            }
            FileStream stream = File.Open(MainPath + obj.SaveName + ".save", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), obj);
            stream.Close();
            return Task.CompletedTask;
        }

        #endregion
    }
    [System.Serializable]
    public class DataHolder {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        public string Name;
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        public string Value;
    }
}