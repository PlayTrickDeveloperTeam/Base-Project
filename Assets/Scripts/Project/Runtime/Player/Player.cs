using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Lean.Touch;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Score;
    public bool SetupCam;
    
    void Start()
    {
        var gold = SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.PlayerCoin);
        Score += gold;
        
        transform.localPosition = transform.parent.GetChild(0).localPosition;
        if (SetupCam)
        {
            B_CF_Main_CameraFunctions.instance.VirtualCameraSetAll(ActiveVirtualCameras.VirCam1,transform.parent.GetChild(0));
        }
    }
    

    public void ChangeGold(int val)
    {
        Score += val;
        if (Score < 0)
        {
            Score = 0;
        }
        UI_Gold.OnGoldChange.Invoke(Score);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //something
    }
    
    
}
