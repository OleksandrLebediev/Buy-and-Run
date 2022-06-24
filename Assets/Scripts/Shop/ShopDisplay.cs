using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemPrice;
    [SerializeField] private Image _itemSprite;

    public void UpdatePriceDisplay(int price, Sprite sprite)
    {
        _itemPrice.text = $"{price}$";
        _itemSprite.sprite = sprite;
    }
}
