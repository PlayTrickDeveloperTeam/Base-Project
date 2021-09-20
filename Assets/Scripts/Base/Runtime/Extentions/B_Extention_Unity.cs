using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.Advertisements;

namespace Base
{
    public static class B_Extention_Unity
    {

        #region Directory Extentions
        //Simple check before creating directory, So it wouldnt cause any duplication problems
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        #endregion

        #region Math Extentions
        //Not tested
        public static float Round(float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }
        //Simple multipication, don't use
        public static float Multi(this float value, float multiplier)
        {
            return value * multiplier;
        }

        #endregion

        #region Transform Extentions

        public static void ResizeObject(this Transform objToEnlarge, float Size)
        {
            objToEnlarge.localScale = new Vector3(Size, Size, Size);
        }


        #endregion

        #region Singleton Extentions
        //Doesn't work, needs more testing and development
        //public static bool SetSingleton(this MonoBehaviour main, MonoBehaviour instance)
        //{
        //    if (instance != null) return false;
        //    instance = main;
        //    return true;
        //}

        #endregion

        #region String Extentions

        #region Save System

        public static bool IsAllLetters(this string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        public static bool IsAllDigits(this string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }

        public static bool IsAllLettersOrDigits(this string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetterOrDigit(c))
                    return false;
            }
            return true;
        }

        #endregion

        #endregion
    }
}