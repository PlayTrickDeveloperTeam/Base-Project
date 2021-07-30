using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Base;
namespace Herkdess.Tools.General.Debugging
{
    public class DC_DebugController : MonoBehaviour
    {
        public static DC_DebugController instance;
        Text DebugText;
        int DebugCounter = 0;
        private void Awake()
        {
            if (instance == null) instance = this; else Destroy(this.gameObject);
            DebugCounter = 0;
            DebugText = GameObject.Find(Database_String.Text_Object_Debug).GetComponent<Text>();
        }

        private void OnDestroy() => instance = null;


        public void DebugTracker(int Count)
        {
            DebugCounter++;
            DebugText.text = "Phase " + Count.ToString() + " Has Finished";
        }

        public void DebugTracker()
        {
            DebugCounter++;
            DebugText.text = "Phase " + DebugCounter + " Has Finished";
        }

    }
}