using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif
namespace Base.UI {
    public class B_UI_ManagerMainFrame : B_M_ManagerBase {

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            foreach (var item in Subframes) item.SetupFrame(this);
            GUIManager.SetupStaticFrame();
            GUIManager.ActivateAllPanels();

            return base.ManagerStrapping();
        }



        #region Helper Functions

        #region Getters

        private void AddChilds(Transform item) {
            foreach (Transform child in item) {
                if (child.GetComponent<B_UI_MenuSubFrame>()) Subframes.Add(child.GetComponent<B_UI_MenuSubFrame>());
                if (child.childCount > 0) AddChilds(child);
            }
        }

        #endregion

        #endregion
        #region Properties

        public static B_UI_ManagerMainFrame instance;
        [ShowIf("OnEditor")]
        [FoldoutGroup("Editor Functions")]
        [PropertyTooltip("DO NOT ENABLE THIS IF YOU DON'T KNOW WHAT YOU ARE DOING")]
        public bool Admin;
        [EnableIf("AreYouSure")]
        [ShowIf("OnEditor")]
        [FoldoutGroup("Editor Functions")]
        [SerializeField] public List<B_UI_MenuSubFrame> Subframes;

        #endregion

        #region Editor

#if UNITY_EDITOR
        #region Functions

        [ShowIf("OnEditor")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Setup Subframes")]
        public async void SetupSubframes() {
            Subframes = new List<B_UI_MenuSubFrame>();
            //Needs a better logic system for deciding when to do what
            if (transform.childCount != 5) AddEmptyMenus();
            AddChilds(transform);

            foreach (var item in Subframes) await item.SetupFrame(this);
            await SetNamesForSubframes();
            await CreateEnums();
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        }
        [ShowIf("AreYouSure")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Reset Subframes")]
        public async void ResetSubframes() {
            foreach (var subframe in Subframes) {
                foreach (var subcomponents in subframe.SubComponents) await subcomponents.FlushData();
                await subframe.FlushFrameData();
            }
            Subframes = new List<B_UI_MenuSubFrame>();
        }

        [ShowIf("AreYouSure")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Clear Subframes")]
        public void ClearSubframes() {
            for (var i = transform.childCount; i > 0; --i)
                DestroyImmediate(transform.GetChild(0).gameObject);
            Subframes = new List<B_UI_MenuSubFrame>();
        }

        [ShowIf("AreYouSure")]
        [HorizontalGroup("Editor Functions/Split", -.5f)]
        [Button("Set Names On Subframes", ButtonSizes.Small)]
        private Task SetNamesForSubframes() {
            for (var i = 0; i < Subframes.Count; i++) Subframes[i].name = Subframes[i].MenuType.ToString();
            return Task.CompletedTask;
        }

        [ShowIf("AreYouSure")]
        [HorizontalGroup("Editor Functions/Split", .5f)]
        [Button("Create SubModule Enums", ButtonSizes.Small)]
        private Task CreateEnums() {
            var TotalDuplicateCount = 0;
            var TotalComponentCount = 0;
            for (var i = 0; i < Subframes.Count; i++) {
                var enumGenericName = Subframes[i].MenuType + "Component";
                var names = new List<string>();
                for (var k = 0; k < Subframes[i].SubComponents.Count; k++) {
                    var _tempName = Subframes[i].SubComponents[k].ComponentParticularName;
                    var duplicates = Subframes[i].SubComponents.Where(t => t.ComponentParticularName == _tempName).ToArray();

                    if (duplicates.Count() <= 0) continue;
                    if (duplicates.Count() == 1) {
                        names.Add(duplicates[0].ComponentParticularName);
                        duplicates[0].EnumName = duplicates[0].ComponentParticularName;
                        TotalComponentCount++;
                    }
                    else {
                        for (var j = 0; j < duplicates.Count(); j++) {
                            if (names.Contains(duplicates[j].ComponentParticularName + "_" + j)) continue;
                            names.Add(duplicates[j].ComponentParticularName + "_" + j);
                            duplicates[j].EnumName = duplicates[j].ComponentParticularName + "_" + j;
                            if (j != 0) TotalDuplicateCount++;
                            TotalComponentCount++;
                        }
                    }
                }
                if (names.Count <= 0) Debug.LogWarning("No Components Found, Please Add Components");
                else EnumCreator.CreateEnum(enumGenericName, names.ToArray());
            }
            Debug.Log(TotalComponentCount + " Components Found! " + TotalDuplicateCount + " Duplicates Renamed!");
            Debug.Log("Please Save The Unity Editor And Then Check If Enums Are Set");
            return Task.CompletedTask;
        }

        private bool Separated;
        private string SeparateButtonName = "Separate Menus";
        [Button("$SeparateButtonName")]
        public void MenuAdjustment() {
            if (Separated) {
                for (var i = 1; i < Subframes.Count + 1; i++) Subframes[i - 1].MoveUIRect(new Vector3(0, 0, 0));
                SeparateButtonName = "Separate Menus";
            }
            else {
                for (var i = 1; i < Subframes.Count + 1; i++) Subframes[i - 1].MoveUIRect(new Vector3(1200 * i, 0, 0));
                SeparateButtonName = "Pull Menus";
            }
            Separated = Separated ? false : true;
        }

        #endregion



        #region Inspector Helpers

        private bool OnEditor() {
            return !EditorApplication.isPlaying;
        }
        private bool AreYouSure() {
            if (!EditorApplication.isPlaying && Admin) return true;
            return false;
        }
        private void AddEmptyMenus() {
            for (var i = 0; i < Enum.GetValues(typeof(Enum_MenuTypes)).Length; i++) {
                var obj = new GameObject();
                obj.AddComponent<RectTransform>();
                obj.transform.parent = transform;
                obj.transform.localPosition = Vector3.zero;
                obj.AddComponent(MenuType((Enum_MenuTypes)i).GetType());
                obj.GetComponent<B_UI_MenuSubFrame>().MenuType = (Enum_MenuTypes)i;
                obj.GetComponent(MenuType(Enum_MenuTypes.Menu_GameOver).GetType());
            }
        }

        private B_UI_MenuSubFrame MenuType(Enum_MenuTypes types) {
            switch (types) {
                case Enum_MenuTypes.Menu_Main:
                    return new UI_Main();
                case Enum_MenuTypes.Menu_PlayerOverlay:
                    return new UI_PlayerOverlay();
                case Enum_MenuTypes.Menu_Paused:
                    return new UI_Paused();
                case Enum_MenuTypes.Menu_GameOver:
                    return new UI_Gameover();
                case Enum_MenuTypes.Menu_Loading:
                    return new UI_Loading();
                default:
                    return new UI_Default();
            }
        }

        #endregion
#endif

        #endregion
    }
}