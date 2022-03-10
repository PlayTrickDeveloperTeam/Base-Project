using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClickAnim : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(Vector3.one * 0.75f, 0.75f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
    }
}
