using System.Collections;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Base {
    public static class SaveSystem {

        public static object GetDataObject(object saveName, object saveEnum) {
            return B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum);
        }

        public static int GetDataInt(object saveName, object saveEnum) {
            return int.Parse(string.Format("{0}", B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString()));
        }

        public static void SetData(object saveName, object saveEnum, object DataToSave) {
            B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).SetData(saveEnum, DataToSave);
        }


    }
}