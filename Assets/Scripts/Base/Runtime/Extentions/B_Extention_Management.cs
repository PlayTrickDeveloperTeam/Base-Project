using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Collections;
using UnityEngine.Advertisements;
using System.Runtime.Serialization.Formatters.Binary;

namespace Base
{
    public static class B_Extention_Management
    {
        #region Vector3 Extentions

        public static Vector3 GetWorldPosition(Ray ray, LayerMask Mask)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Mask))
            {
                return hit.point;
            }
            return Vector3.zero;
        }

        public static Vector3 GetWorldPosition(this Vector3 vector3, Camera cam, LayerMask Mask)
        {
            Ray ray = cam.ScreenPointToRay(vector3);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Mask))
            {
                return hit.point;
            }
            return Vector3.zero;
        }

        public static Transform GetWorldObject(this Vector3 vec3, Camera cam, LayerMask mask)
        {
            Ray ray = cam.ScreenPointToRay(vec3);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                return hit.collider.transform;
            }
            return null;
        }


        public static Vector3 GetHitPosition(this Vector3 mainObj, Vector3 objectToPush, float yMinus, float force)
        {
            Vector3 _temp = mainObj;
            _temp.y -= yMinus;
            return (objectToPush - _temp) * force;
        }

        //public static Vector3 GetHitPositionReverse(this Vector3 mainObj, Vector3 objectToPush, float yMinus, float force)
        //{
        //    Vector3 _temp = mainObj;
        //    _temp.y -= yMinus;
        //    return (_temp - objectToPush) * force;
        //}

        //public static Vector3[] WorldPositionsOfCameraWithY(this Camera cam, float y)
        //{
        //    Vector3[] worldPositions = new Vector3[4];
        //    Vector3[] cameraPositions = cam.GetCameraCorners(y);
        //    //Vector3[] realWorldPosition = cam.ViewportToWorldPoint(cameraPositions[i]);
        //    for (int i = 0; i < worldPositions.Length; i++)
        //    {

        //        worldPositions[i] = new Vector3(cam.ScreenToWorldPoint(cameraPositions[i]).x, y, cam.ScreenToWorldPoint(cameraPositions[i]).y);
        //        //worldPositions[i].y = y;
        //    }
        //    return worldPositions;
        //}

        public static Vector3[] GetCameraCorners(this Camera cam, float y)
        {
            Vector3[] cameraPositions = new Vector3[4];
            //cameraPositions[0] = new Vector3(Screen.width, 0, y);
            //cameraPositions[1] = new Vector3(Screen.width, 0, y);
            //cameraPositions[2] = new Vector3(Screen.width, Screen.height, y);
            //cameraPositions[3] = new Vector3(0, Screen.height, y);
            cameraPositions[0] = new Vector3(0, 0);
            cameraPositions[1] = new Vector3(1, 0);
            cameraPositions[2] = new Vector3(0, 0);
            cameraPositions[3] = new Vector3(1, 1);
            return cameraPositions;

        }

        #endregion

        #region Math Extentions

        public static float Round(float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }

        public static float Multi(this float value, float multiplier)
        {
            return value * multiplier;
        }
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle, 360);
            min = Mathf.Repeat(min, 360);
            max = Mathf.Repeat(max, 360);
            bool inverse = false;
            var tmin = min;
            var tangle = angle;
            if (min > 180)
            {
                inverse = !inverse;
                tmin -= 180;
            }
            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            var result = !inverse ? tangle > tmin : tangle < tmin;
            if (!result)
                angle = min;

            inverse = false;
            tangle = angle;
            var tmax = max;
            if (angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            if (max > 180)
            {
                inverse = !inverse;
                tmax -= 180;
            }

            result = !inverse ? tangle < tmax : tangle > tmax;
            if (!result)
                angle = max;
            return angle;
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