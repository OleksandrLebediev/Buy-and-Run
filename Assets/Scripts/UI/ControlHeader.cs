using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControlHeader : MonoBehaviour
{
    public void Initialize()
    {
        transform.DOScale(1.1f, 1).SetLoops(-1, LoopType.Yoyo);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
