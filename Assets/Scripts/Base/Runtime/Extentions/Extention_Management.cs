using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Advertisements;
using Newtonsoft.Json;

namespace Base
{
    public static class Extention_Management
    {
        #region Save System Extentions
        //All save system extentions are subject to change
        public static bool SaveGameData(this B_SE_SaveDataObject dataObject)
        {
            if (!Directory.Exists(dataObject.SaveLocation.AddSaveDirectory())) { Directory.CreateDirectory(dataObject.SaveLocation.AddSaveDirectory()); }
            File.WriteAllText(dataObject.AddSaveFileName(), JsonConvert.SerializeObject(dataObject.DataContainer.DataCluster, Formatting.Indented));
            return true;
        }

        public static bool LoadGameData(this B_SE_SaveDataObject dataObject)
        {
            if (!File.Exists(dataObject.AddSaveFileName())) { return false; }
            using (StreamReader _file = File.OpenText(dataObject.AddSaveFileName()))
            {
                JsonSerializer _serializer = new JsonSerializer();
                dataObject.DataContainer.DataCluster = (Dictionary<string, string>)_serializer.Deserialize(_file, typeof(Dictionary<string, string>));
                _file.Close();
            }
            return true;
        }

        public static bool SE_LogicCheck(this B_SE_SaveDataObject dataObject)
        {
            if (File.Exists(dataObject.AddSaveFileName()))
            {
                //if (dataObject.DataContainer.DataCluster.Count <= 0) { dataObject.FillEmptyData(); return false; }
                using (StreamReader _file = File.OpenText(dataObject.AddSaveFileName()))
                {
                    JsonSerializer _serializer = new JsonSerializer();
                    Dictionary<string, string> _tempDic = new Dictionary<string, string>();
                    _tempDic = (Dictionary<string, string>)_serializer.Deserialize(_file, typeof(Dictionary<string, string>));
                    dataObject.DataContainer.DataCluster = new Dictionary<string, string>();
                    dataObject.DataContainer.DataCluster = _tempDic;
                    _file.Close();
                    return true;
                }
            }
            else
            {
                dataObject.FillEmptyData();
                dataObject.SaveGameData();
                return false;
            }

        }

        #endregion

        #region Adds Manager Extentions
        //Not tested to its fullest, needs more tests and development
        public static void ShowRewardedAdd(this M_AddManager _addsManager)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
            }
            else
            {
                Debug.Log("Add not ready");
            }
        }

        public static void ShowNormalAdd(this M_AddManager _addsManager)
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
        }

        #endregion

        #region Recttransform Extentions
        //Use this to move Pesky uý objects
        public static void MoveUIObject(this RectTransform rectTransform, Vector2 vector2)
        {
            rectTransform.offsetMax = vector2;
            rectTransform.offsetMin = vector2;
        }

        #endregion
    }
}