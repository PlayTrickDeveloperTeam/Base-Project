using System.Collections;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Base
{
    public static class SaveSystem
    {

        public static object GetDataObject(object saveName, object saveEnum)
        {
            return B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum);
        }

        public static int GetDataInt(object saveName, object saveEnum)
        {
            return int.Parse(string.Format("{0}", B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString()));
        }

        public static void SetData(object saveName, object saveEnum, object DataToSave)
        {
            B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).SetData(saveEnum, DataToSave);
        }

        public static Task SaveObjectData(this SaveObject obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string Path = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
            FileStream file = new FileStream(Path + obj.name + ".save", FileMode.Create);
            var json = JsonUtility.ToJson(obj);
            formatter.Serialize(file, json);
            file.Close();
            return Task.CompletedTask;
        }

        public static Task LoadObjectData(this SaveObject obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string Path = Application.persistentDataPath + "/Saves/";
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
                FileStream file = new FileStream(Path + obj.name + ".save", FileMode.Create);
                var json = JsonUtility.ToJson(obj);
                formatter.Serialize(file, json);
                file.Close();
            }
            //if (!File.Exists(Application.persistentDataPath + "/Saves"))
            //{
            //    FileStream file = new FileStream(Path + obj.name + ".save", FileMode.Create);
            //    var json = JsonUtility.ToJson(obj);
            //    formatter.Serialize(file, json);
            //    file.Close();
            //}
            FileStream stream = File.Open(Path + obj.name + ".save", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), obj);
            stream.Close();
            return Task.CompletedTask;
        }
    }
}