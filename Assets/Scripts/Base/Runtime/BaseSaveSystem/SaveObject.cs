using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Base
{
    [System.Serializable]
    public class SaveObject : ScriptableObject
    {
        public string SaveName;
        public List<DataHolder> SaveCluster;
        Dictionary<string, object> saveDic;

        public object GetData(object name)
        {
            if (saveDic.ContainsKey(name.ToString())) { return saveDic[name.ToString()]; }
            else return null;
        }

        public void SetData(object name, object value)
        {
            Debug.Log(saveDic[name.ToString()]);
            if (saveDic.ContainsKey(name.ToString())) { saveDic[name.ToString()] = value; }
        }

        [Button]
        public void CreateEnums()
        {
            string[] _temp = new string[SaveCluster.Count];
            for (int i = 0; i < SaveCluster.Count; i++)
            {
                _temp[i] = SaveCluster[i].Name;
            }
            EnumCreator.CreateEnum(SaveName, _temp);
        }

        [Button]
        public void SaveThisData()
        {
            for (int i = 0; i < saveDic.Count; i++)
            {
                SaveCluster[i].Value = saveDic[SaveCluster[i].Name].ToString();
            }
            this.SaveObjectData();
        }

        [Button]
        public void LoadThisData()
        {
            this.LoadObjectData();
            saveDic = new Dictionary<string, object>();
            for (int i = 0; i < SaveCluster.Count; i++)
            {
                saveDic.Add(SaveCluster[i].Name, SaveCluster[i].Value);
            }
        }
    }
    [System.Serializable]
    public class DataHolder
    {
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Left")]
        public string Name;
        [HorizontalGroup("Split")]
        [VerticalGroup("Split/Right")]
        public string Value;
    }
}