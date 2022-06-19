using System;
using TMPro;
using UnityEngine;

public class MultiplierBoardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _multiplierHeader;

    public void SetMultiplier(float multiplier)
    {
        multiplier = (float)Math.Round(multiplier, 1);
        _multiplierHeader.text = $"{multiplier}x";
    }

}
