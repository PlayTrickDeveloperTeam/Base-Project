using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace Base
{
    [CreateAssetMenu(fileName = "New Save Data Object", menuName = "Save System/Save Data Object")]
    public class B_SE_SaveDataObject : ScriptableObject
    {
        public string SaveLocation;
        public string SaveName;
        public DataContainer DataContainer { get; private set; }

        public void FirstTimeSaveSetup()
        {
            DataContainer = new DataContainer();
            FillEmptyData();
            this.SaveGameData();
            PlayerPrefs.SetInt(Database_String.Save_Int_FirstTime, 1);
            //Debug.Log("FirstTimeSetup");
        }

        public bool DataReady()
        {
            //Debug.Log("LoadingData");
            DataContainer = new DataContainer();
            this.LoadGameData();
            return true;
        }

        public dynamic GetData(B_SE_DataTypes key)
        {
            string _dataKey = key.ToString();
            if (!DataContainer.DataCluster.ContainsKey(_dataKey)) return null;
            return DataContainer.DataCluster[_dataKey];

        }

        public string GetDataS(B_SE_DataTypes key)
        {
            string _dataKey = key.ToString();
            if (!DataContainer.DataCluster.ContainsKey(_dataKey)) return null;
            return DataContainer.DataCluster[_dataKey].ToString();
        }

        public int GetDataI(B_SE_DataTypes key)
        {
            string _dataKey = key.ToString();
            if (!DataContainer.DataCluster.ContainsKey(_dataKey)) return -800;
            return int.Parse(DataContainer.DataCluster[_dataKey].ToString());
        }
        public float GetDataF(B_SE_DataTypes key)
        {
            string _dataKey = key.ToString();
            if (!DataContainer.DataCluster.ContainsKey(_dataKey)) return -800;
            return float.Parse(DataContainer.DataCluster[_dataKey].ToString());
        }


        public void AddData(B_SE_DataTypes key, dynamic value)
        {
            string _temp = key.ToString();
            if (DataContainer.DataCluster.ContainsKey(_temp)) return;
            DataContainer.DataCluster.Add(_temp, value.ToString());
        }

        public void SetData(B_SE_DataTypes key, dynamic value)
        {
            if (!DataContainer.DataCluster.ContainsKey(key.ToString())) return;
            DataContainer.DataCluster[key.ToString()] = value.ToString();
        }


        #region Editor Only

        public void ClusterReset()
        {
            DataContainer.DataCluster = new Dictionary<string, string>();
        }

        public void AddData(string Key, string Value)
        {
            if (DataContainer.DataCluster.ContainsKey(Key))
            {
                DataContainer.DataCluster[Key] = Value;
                return;
            }
            DataContainer.DataCluster.Add(Key, Value);
        }

        public void FillEmptyData()
        {
            for (int i = 0; i < Enum.GetNames(typeof(B_SE_DataTypes)).Length; i++)
            {
                AddData((B_SE_DataTypes)i, "0");
            }
        }
        #endregion


    }

    [System.Serializable]
    public class DataContainer
    {
        [SerializeField] public Dictionary<string, string> DataCluster;
        public DataContainer()
        {
            DataCluster = new Dictionary<string, string>();
        }
    }
}