using System;
using UnityEngine;

namespace Base
{
    public static class B_Extention_Unity
    {
        #region Transform Extentions

        public static void ResizeObject(this Transform objToEnlarge, float Size)
        {
            objToEnlarge.localScale = new Vector3(Size, Size, Size);
        }

        #endregion Transform Extentions

        #region Singleton Extentions

        //Doesn't work, needs more testing and development
        //public static bool SetSingleton(this MonoBehaviour main, MonoBehaviour instance)
        //{
        //    if (instance != null) return false;
        //    instance = main;
        //    return true;
        //}

        #endregion Singleton Extentions

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

        public static float IsFloat(this string s)
        {
            if (s.IsAllDigits())
                return float.Parse(s);
            else return 0;
        }

        #endregion String Extentions
    }
}