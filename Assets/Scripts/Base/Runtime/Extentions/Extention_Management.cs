using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Advertisements;

namespace Base
{
    public static class Extention_Management
    {

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