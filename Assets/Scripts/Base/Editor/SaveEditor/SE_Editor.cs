using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;
using System.IO;
using System.Dynamic;
using Base;

namespace Base
{
    public class SE_Editor
    {
        [OnValueChanged("ReadyData")]
        public B_SE_SaveDataObject SaveToModify;
        [ShowIf("IsNotNull", true)]
        [HideLabel]
        public List<SE_Provider> provider;
        public SE_Editor()
        {
            provider = new List<SE_Provider>();

        }

        [ShowIf("IsNotNull", true)]
        [HorizontalGroup("Buttons")]
        [Button("Clear Save Data")]
        void ClearSaveData()
        {
            provider = new List<SE_Provider>();
            for (int i = 0; i < SaveToModify.DataContainer.DataCluster.Count; i++)
            {
                SaveToModify.SetData((B_SE_DataTypes)i, 0);
            }
            foreach (var item in SaveToModify.DataContainer.DataCluster)
            {
                string _temp = item.Value.ToString();
                provider.Add(new SE_Provider(item.Key, _temp));
            }
            SaveToModify.SaveGameData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

        [ShowIf("IsNotNull", true)]
        [HorizontalGroup("Buttons", Order = 0)]
        [Button]
        void SaveDatacluster()
        {
            List<string> CurrentEnums = new List<string>();
            string Path = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("B_SE_EnumStorage")[0]);
            string FileFormat = "public enum B_SE_DataTypes { ";

            for (int i = 0; i < provider.Count; i++)
            {
                if (provider[i].Data.Length <= 0 && provider[i].Value.Length <= 0)
                {
                    provider.Remove(provider[i]);
                    continue;
                }
                CurrentEnums.Add(provider[i].Data.ToString());
            }

            for (int i = 0; i < CurrentEnums.Count; i++)
            {
                FileFormat += CurrentEnums[i];
                if (i == CurrentEnums.Count - 1) { FileFormat += " }"; continue; }
                FileFormat += ", ";
            }

            File.WriteAllText(Path, FileFormat);

            SaveToModify.DataContainer.DataCluster = new Dictionary<string, string>();

            for (int i = 0; i < provider.Count; i++)
            {
                dynamic value = new ExpandoObject();

                if (provider[i].Value.IsAllLetters())
                {
                    Debug.Log(provider[i].Value + " Is all letters");
                    value = provider[i].Value;
                }
                else if (provider[i].Value.Contains(".") || provider[i].Value.Contains("f"))
                {
                    string _temp = provider[i].Value.Replace("f", "");
                    value = float.Parse(_temp);
                }
                else
                {
                    value = int.Parse(provider[i].Value);
                }
                SaveToModify.AddData(provider[i].Data, value);
            }
            SaveToModify.SaveGameData();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        public List<string> CurrentEnums()
        {
            List<string> _temp = new List<string>();
            for (int i = 0; i < Enum.GetNames(typeof(B_SE_DataTypes)).Length; i++)
            {
                _temp.Add(Enum.GetName(typeof(B_SE_DataTypes), i));
            }
            return _temp;
        }

        void ReadyData()
        {
            if (SaveToModify == null)
            {
                provider = new List<SE_Provider>();
                return;
            }
            if (SaveToModify.SE_LogicCheck())
            {
                foreach (var item in SaveToModify.DataContainer.DataCluster)
                {
                    string _temp = item.Value.ToString();
                    provider.Add(new SE_Provider(item.Key, _temp));
                }
            }
            else
            {
                foreach (var item in SaveToModify.DataContainer.DataCluster)
                {
                    string _temp = item.Value.ToString();
                    provider.Add(new SE_Provider(item.Key, _temp));
                }
            }
        }


        bool IsNotNull()
        {
            if (SaveToModify == null) return false;
            return true;
        }

    }
    [System.Serializable]
    public class SE_Provider
    {
        public SE_Provider(string name, string value)
        {
            this.Data = name;
            this.Value = value;
        }
        [HorizontalGroup(LabelWidth = 50)]
        public string Data;
        [HorizontalGroup(LabelWidth = 50)]
        public string Value;
    }
}