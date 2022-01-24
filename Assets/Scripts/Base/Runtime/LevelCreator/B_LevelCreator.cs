using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.Utilities;
using UnityEditor.SceneManagement;
#endif

public class B_LevelCreator : SerializedMonoBehaviour {

    #if UNITY_EDITOR
    
    [BoxGroup("Level Creator")]
    [ValueDropdown("GetLevels")]
    [HideLabel]
    [OnValueChanged("LoadLevel")]
    public string SelectedLevel;
    
    private GameObject currentLevel;
    

    List<string> GetLevels() {
        var Levels = new List<string>();
        Levels.Add("Null");
        var levels = Resources.LoadAll(B_Database_String.Path_Res_MainLevels);
        for (int i = 0; i < levels.Length; i++) {
            Levels.Add(levels[i].name);
        }
        return Levels;
    }

    void LoadLevel() {
        if(SelectedLevel.IsNullOrWhitespace()) return;
        if (SelectedLevel == "Null") {
            SaveChanges();
            Clear();
            return;
        }
        if (currentLevel) {
            SaveChanges();
            Clear();
        }
        GameObject obj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + SelectedLevel);
        currentLevel = PrefabUtility.InstantiatePrefab(obj, transform) as GameObject;
        PrefabUtility.UnpackPrefabInstance(currentLevel, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        AssetDatabase.SaveAssets();
    }
    [VerticalGroup("Level Creator/VA")]
    [HorizontalGroup("Level Creator/VA/A", .5f)]
    [Button]
    void CreateNewLevel() {

        if (SelectedLevel.IsNullOrWhitespace() || SelectedLevel == "Null") {
            var allLevels = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_MainLevels);
            var orderedEnumerable = allLevels.OrderBy(t => t.name);
            SelectedLevel = orderedEnumerable.Last().name;
            Debug.Log(SelectedLevel);
        }
        if (currentLevel) {
            SaveChanges();
            Clear();
        }
        GameObject obj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + SelectedLevel);
        int CurrentLevelsCount = Resources.LoadAll<GameObject>(B_Database_String.Path_Res_MainLevels).Length;
        string newLevelName = (CurrentLevelsCount + 1).ToString();
        if (newLevelName.Length < 3) {
            newLevelName = newLevelName.Insert(0, "0");
            if (newLevelName.Length < 3) {
                newLevelName = newLevelName.Insert(0, "0");
            }
        }
        newLevelName = newLevelName.Insert(0, "MainLevel ");
        currentLevel = Instantiate(obj, transform);
        currentLevel.name = newLevelName;
        PrefabUtility.SaveAsPrefabAsset(currentLevel, $"Assets/Resources/Levels/Mainlevels/{currentLevel.name}.prefab");
        GameObject newObj = Resources.Load<GameObject>(B_Database_String.Path_Res_MainLevels + newLevelName);
        DestroyImmediate(currentLevel);
        currentLevel = PrefabUtility.InstantiatePrefab(newObj, transform) as GameObject;
        PrefabUtility.UnpackPrefabInstance(currentLevel, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        SelectedLevel = newLevelName;
        AssetDatabase.SaveAssets();
    }
    
    
    [HorizontalGroup("Level Creator/VA/A", .5f)]
    [Button]
    void SaveChanges() {
        if(!currentLevel) return;
        PrefabUtility.SaveAsPrefabAsset(currentLevel, $"Assets/Resources/Levels/Mainlevels/{currentLevel.name}.prefab");
        AssetDatabase.SaveAssets();
    }
    [VerticalGroup("Level Creator/VB")]
    [HorizontalGroup("Level Creator/VB/A", .5f)]
    [Button]
    void ResetChanges() {
        if(!currentLevel) return;
        DestroyAllChildren(transform);
        // transform.DestroyAllChildren();
        LoadLevel();
        AssetDatabase.SaveAssets();
    }
    
    [HorizontalGroup("Level Creator/VB/A", .5f)]
    [Button]
    public void Clear() {
        SaveChanges();
        DestroyAllChildren(transform);
        // transform.DestroyAllChildren();
        AssetDatabase.SaveAssets();
    }

    #region HelperFunctions

    
    void DestroyAllChildren(Transform transform) {
        if (transform.childCount <= 0) return;
        for (int i = transform.childCount - 1; i >= 0; i--) {
                #if UNITY_EDITOR
            if (!Application.isPlaying)
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
                #endif
            if (Application.isPlaying)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    #endregion
    #endif
}
