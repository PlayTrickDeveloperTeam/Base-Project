namespace Base {
    public static class SaveSystem {

        public static object GetDataObject(object saveName, object saveEnum) {
            if (string.IsNullOrEmpty((string)B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum))) return null;
            return B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum);
        }

        public static string GetDataString(object saveName, object saveEnum) {
            if (string.IsNullOrEmpty((string)B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum))) return null;
            return B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString();
        }

        public static int GetDataInt(object saveName, object saveEnum) {
            return int.Parse(string.Format("{0}", B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum)));
        }

        public static float GetDataFloat(object saveName, object saveEnum) {
            return float.Parse(B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).GetData(saveEnum).ToString());
        }

        public static void SetData(object saveName, object saveEnum, object DataToSave) {
            B_GM_GameManager.instance.Save.GetSaveObject(saveName.ToString()).SetData(saveEnum, DataToSave);
        }
    }
}