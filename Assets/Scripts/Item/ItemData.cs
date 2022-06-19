using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Item _itemPrefab;

    public string ItemName => _itemName; 
    public int Price => _price;
    public Sprite Icon => _icon;
    public Item Item => _itemPrefab;
}
