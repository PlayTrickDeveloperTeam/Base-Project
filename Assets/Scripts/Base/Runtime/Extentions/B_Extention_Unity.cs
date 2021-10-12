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
    }
}