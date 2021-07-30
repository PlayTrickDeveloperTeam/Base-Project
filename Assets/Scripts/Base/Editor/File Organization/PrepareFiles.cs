using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

namespace PlayTrick.Tools
{
    public class PrepareFiles
    {

        [MenuItem("Tools/File Organizer/Setup Files")]
        public static void GO()
        {
            string MainPath = "Assets/";
            string Resources = MainPath + "Resources";
            string ScriptableAssets = Resources + "/ScriptableAssets";
            string SavesAssets = ScriptableAssets + "/Saves";

            string Scripts = MainPath + "Scripts";
            string GameManagement = Scripts + "/GameManagement";
            string Gameplay = Scripts + "/Gameplay";
            string Player = Scripts + "/Player";
            string OOP = Scripts + "/OOP";

            string SaveSystemFunctions = Scripts + "/SaveSystemFunctions";


            string ThirdParty = MainPath + "ThirdParty";
            string Prefabs = MainPath + "Prefabs";
            string Plugins = MainPath + "Plugins";
            string Settings = MainPath + "Settings";
            string Scenes = MainPath + "Scenes";
            string Visuals = MainPath + "Visuals";
            string Misc = MainPath + "Misc";

            List<string> Files = new List<string>();

            Files.Add(Resources);
            Files.Add(ScriptableAssets);
            Files.Add(SavesAssets);

            Files.Add(Scripts);
            Files.Add(GameManagement);
            Files.Add(Gameplay);
            Files.Add(Player);
            Files.Add(OOP);
            Files.Add(SaveSystemFunctions);

            Files.Add(ThirdParty);
            Files.Add(Prefabs);
            Files.Add(Plugins);
            Files.Add(Settings);
            Files.Add(Scenes);
            Files.Add(Visuals);
            Files.Add(Misc);



            for (int i = 0; i < Files.Count; i++)
            {
                if (!File.Exists(Files[i]))
                {
                    Directory.CreateDirectory(Files[i]);
                }
            }


            AssetDatabase.Refresh();
        }
    }
}