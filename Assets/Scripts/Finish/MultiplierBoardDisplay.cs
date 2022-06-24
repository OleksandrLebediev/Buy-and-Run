using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class MultiplierBoardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _multiplierHeader;

    public void SetMultiplier(float multiplier)
    {
        multiplier = (float)Math.Round(multiplier, 1);
        _multiplierHeader.text = $"{multiplier}x";
    }

    public void Flashing()
    {
        transform.DOScale(transform.localScale.x + 0.15f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

}
