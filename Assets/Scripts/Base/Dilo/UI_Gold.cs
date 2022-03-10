using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_Gold : MonoBehaviour
{
    public static Action<int> OnGoldChange;
    public TextMeshProUGUI goldText;
    
    private void Awake()
    {
        goldText = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    IEnumerable Start()
    {
        yield return new WaitForSeconds(0.1f);
        var gold = SaveSystem.GetDataInt(Enum_Saves.MainSave, Enum_MainSave.PlayerCoin);
        GoldTextUpdate(gold);
    }

    private void OnEnable()
    {
        OnGoldChange += GoldTextUpdate;
    }

    private void OnDisable()
    {
        OnGoldChange  -= GoldTextUpdate;
    }
    
    private void GoldTextUpdate(int gold)
    {
        DOTween.Kill("Gold");
        goldText.transform.DOLocalMoveY(goldText.transform.localPosition.y + 20f,0.25f).SetLoops(2,LoopType.Yoyo).SetId("Gold");
        goldText.text = gold.ToString();
    }
}
