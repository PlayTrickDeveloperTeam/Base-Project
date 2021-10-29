using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif
using Random = UnityEngine.Random;
namespace Base.UI
{
    public class B_UI_ManagerMainFrame : MonoBehaviour
    {
        [ShowIf("OnEditor")]
        [FoldoutGroup("Editor Functions")]
        [SerializeField] private List<B_UI_MenuSubFrame> Subframes;

        #region Editor
#if UNITY_EDITOR
        #region Functions
        [ShowIf("OnEditor")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Setup Subframes")]
        public async void SetupSubframes()
        {
            Subframes = new List<B_UI_MenuSubFrame>();
            if (transform.childCount < 5)
            {
                AddChildren();
            }
            foreach (Transform item in transform)
            {
                if (item.GetComponent<B_UI_MenuSubFrame>())
                {
                    Subframes.Add(item.GetComponent<B_UI_MenuSubFrame>());
                }
            }
            foreach (var item in Subframes)
            {
                await item.SetupFrame(this);
            }
            await SetNamesForSubframes();
            await CreateEnums();
        }
        [ShowIf("OnEditor")]
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

        [ShowIf("OnEditor")]
        [VerticalGroup("Editor Functions/Upper", .5f)]
        [Button("Clear Subframes")]
        public async void ClearSubframes()
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }

        [ShowIf("OnEditor")]
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

        string EnumName = "UIComponentFrames";
        [ShowIf("OnEditor")]
        [HorizontalGroup("Editor Functions/Split", .5f)]
        [Button("Create SubModule Enums", ButtonSizes.Small)]
        Task CreateEnums()
        {
            List<string> names = new List<string>();
            int TotalDuplicateCount = 0;
            for (int i = 0; i < Subframes.Count; i++)
            {
                for (int k = 0; k < Subframes[i].SubComponents.Count; k++)
                {
                    string _tempName = Subframes[i].SubComponents[k].ComponentParticularName;
                    B_UI_ComponentsSubframe[] duplicates = Subframes[i].SubComponents.Where(t => t.ComponentParticularName == _tempName).ToArray();
                    if (duplicates.Count() <= 0) continue;
                    if (duplicates.Count() == 1)
                    {
                        names.Add(duplicates[0].ComponentParticularName);
                    }
                    else
                    {
                        for (int j = 0; j < duplicates.Count(); j++)
                        {
                            if (names.Contains(duplicates[j].ComponentParticularName + "_" + j.ToString())) continue;
                            names.Add(duplicates[j].ComponentParticularName + "_" + j.ToString());
                            if (j != 0) TotalDuplicateCount++;
                        }
                    }
                }
            }
            if (names.Count <= 0) { throw new Exception("No Components Found, Please Add Components"); }
            else
            {
                Debug.Log(names.Count + " Components Found! " + TotalDuplicateCount + " Duplicates Renamed!");
                Debug.Log("Please Save The Unity Editor And Then Check If Enums Are Set");
                EnumCreator.CreateEnum(EnumName, names.ToArray());
                return Task.CompletedTask;
            }
        }
        #endregion

        #region Inspector Helpers

        bool OnEditor()
        {
            return !EditorApplication.isPlaying;
        }

        void AddChildren()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Enum_MenuTypes)).Length; i++)
            {
                GameObject obj = new GameObject();
                //obj.AddComponent<RectTransform>();
                obj.transform.parent = transform;
                obj.AddComponent(MenuType((Enum_MenuTypes)i).GetType());
                obj.GetComponent<B_UI_MenuSubFrame>().MenuType = (Enum_MenuTypes)i;
            }
        }

        MonoBehaviour MenuType(Enum_MenuTypes types)
        {
            switch (types)
            {
                case Enum_MenuTypes.Menu_Main:
                    return new B_UI_MSF_Main();
                case Enum_MenuTypes.Menu_PlayerOverlay:
                    return new B_UI_MSF_PlayerOverlay();
                case Enum_MenuTypes.Menu_Paused:
                    return new B_UI_MSF_Paused();
                case Enum_MenuTypes.Menu_GameOver:
                    return new B_UI_MSF_GameOver();
                case Enum_MenuTypes.Menu_Loading:
                    return new B_UI_MSF_Loading();
                default:
                    return new B_UI_MSF_Default();
            }
        }

        #endregion
#endif
        #endregion

    }
}