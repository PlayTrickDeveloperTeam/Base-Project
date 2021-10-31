using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Base;
using DG.Tweening;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEditor;
#endif
using Random = UnityEngine.Random;
namespace Base.UI
{
    public class B_UI_ManagerMainFrame : B_M_ManagerBase
    {
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

        public override Task ManagerStrapping()
        {
            if (instance == null) instance = this; else Destroy(this.gameObject);
            foreach (var item in Subframes)
            {
                item.SetupFrame(this);
            }
            GUIManager.SetupStaticFrame();
            //Subframes.ForEach(t => t.GetComponent<RectTransform>().DOLocalMove(Vector3.zero, 0, true));
            GUIManager.ActivateAllPanels(0);
            //Just an example
            //B_UI_SMF_MainFrame.DeactivateAllPanelsWithAnim().onComplete += () => B_UI_SMF_MainFrame.Loading.EnableUI(1);

            return base.ManagerStrapping();
        }

        public override Task ManagerDataFlush()
        {
            return base.ManagerDataFlush();
        }


        #region Helper Functions

        #region Getters

        void AddChilds(Transform item)
        {
            foreach (Transform child in item)
            {
                if (child.GetComponent<B_UI_MenuSubFrame>())
                {
                    Subframes.Add(child.GetComponent<B_UI_MenuSubFrame>());
                }
                if (child.childCount > 0) { AddChilds(child); }
            }
        }
        #endregion

        #endregion

        #region Editor
#if UNITY_EDITOR
        #region Functions
        [ShowIf("OnEditor")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Setup Subframes")]
        public async void SetupSubframes()
        {
            Subframes = new List<B_UI_MenuSubFrame>();
            //Needs a better logic system for deciding when to do what
            if (transform.childCount != 5)
            {
                AddEmptyMenus();
            }
            AddChilds(transform);
            //foreach (Transform item in transform)
            //{
            //    if (item.GetComponent<B_UI_MenuSubFrame>())
            //    {
            //        Subframes.Add(item.GetComponent<B_UI_MenuSubFrame>());
            //    }
            //    if (item.childCount > 0)
            //    {

            //    }
            //}
            foreach (var item in Subframes)
            {
                await item.SetupFrame(this);
            }
            await SetNamesForSubframes();
            await CreateEnums();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
        [ShowIf("AreYouSure")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Reset Subframes")]
        public async void ResetSubframes()
        {
            foreach (var subframe in Subframes)
            {
                foreach (var subcomponents in subframe.SubComponents)
                {
                    await subcomponents.FlushData();
                }
                await subframe.FlushFrameData();
            }
            this.Subframes = new List<B_UI_MenuSubFrame>();
        }

        [ShowIf("AreYouSure")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Clear Subframes")]
        public void ClearSubframes()
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
            Subframes = new List<B_UI_MenuSubFrame>();
        }

        [ShowIf("AreYouSure")]
        [HorizontalGroup("Editor Functions/Split", -.5f)]
        [Button("Set Names On Subframes", ButtonSizes.Small)]
        Task SetNamesForSubframes()
        {
            for (int i = 0; i < Subframes.Count; i++)
            {
                Subframes[i].name = Subframes[i].MenuType.ToString();
            }
            return Task.CompletedTask;
        }

        [ShowIf("AreYouSure")]
        [HorizontalGroup("Editor Functions/Split", .5f)]
        [Button("Create SubModule Enums", ButtonSizes.Small)]
        Task CreateEnums()
        {
            int TotalDuplicateCount = 0;
            int TotalComponentCount = 0;
            for (int i = 0; i < Subframes.Count; i++)
            {
                string enumGenericName = Subframes[i].MenuType.ToString() + "Component";
                List<string> names = new List<string>();
                for (int k = 0; k < Subframes[i].SubComponents.Count; k++)
                {
                    string _tempName = Subframes[i].SubComponents[k].ComponentParticularName;
                    UI_TComponentsSubframe[] duplicates = Subframes[i].SubComponents.Where(t => t.ComponentParticularName == _tempName).ToArray();

                    if (duplicates.Count() <= 0) continue;
                    if (duplicates.Count() == 1)
                    {
                        names.Add(duplicates[0].ComponentParticularName);
                        duplicates[0].EnumName = duplicates[0].ComponentParticularName;
                        TotalComponentCount++;
                    }
                    else
                    {
                        for (int j = 0; j < duplicates.Count(); j++)
                        {
                            if (names.Contains(duplicates[j].ComponentParticularName + "_" + j.ToString())) continue;
                            names.Add(duplicates[j].ComponentParticularName + "_" + j.ToString());
                            duplicates[j].EnumName = duplicates[j].ComponentParticularName + "_" + j.ToString();
                            if (j != 0) TotalDuplicateCount++;
                            TotalComponentCount++;
                        }
                    }
                }
                if (names.Count <= 0) { Debug.LogWarning("No Components Found, Please Add Components"); }
                else
                {
                    EnumCreator.CreateEnum(enumGenericName, names.ToArray());
                }
            }
            Debug.Log(TotalComponentCount + " Components Found! " + TotalDuplicateCount + " Duplicates Renamed!");
            Debug.Log("Please Save The Unity Editor And Then Check If Enums Are Set");
            return Task.CompletedTask;
        }
        #endregion

        #region Inspector Helpers

        bool OnEditor()
        {
            return !EditorApplication.isPlaying;
        }
        bool AreYouSure()
        {
            if (!EditorApplication.isPlaying && Admin) return true;
            return false;
        }
        void AddEmptyMenus()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Enum_MenuTypes)).Length; i++)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<RectTransform>();
                obj.transform.parent = transform;
                obj.transform.localPosition = Vector3.zero;
                obj.AddComponent(MenuType((Enum_MenuTypes)i).GetType());
                obj.GetComponent<B_UI_MenuSubFrame>().MenuType = (Enum_MenuTypes)i;
                obj.GetComponent(MenuType(Enum_MenuTypes.Menu_GameOver).GetType());
            }
        }

        B_UI_MenuSubFrame MenuType(Enum_MenuTypes types)
        {
            switch (types)
            {
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