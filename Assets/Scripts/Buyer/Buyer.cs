using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Buyer : MonoBehaviour, IBuyer
{
    [SerializeField] private int _orderPrice;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private BuyerZone _buyerZone;
    [SerializeField] private BuyerDisplay _buyerDisplay;
    [SerializeField] private Transform _receptionPoint;

    private float _receiveDelay = 0.05f;

    public Transform ThisTransform => transform;
    public float ReceiveDelay => _receiveDelay;
    public int OrderPrice =>  _orderPrice;
    public string OrderItemName => _itemData.ItemName;

    private void OnEnable()
    {
       _buyerZone.ShopingCartEntered += OnShopingCartEntered;
       _buyerZone.ShopingCartExited += OnShopingCartExited;
    }

    private void OnDisable()
    {
        _buyerZone.ShopingCartEntered -= OnShopingCartEntered;
        _buyerZone.ShopingCartExited -= OnShopingCartExited;
    }

    public void OnItemReceiving(Item item)
    {
        
    }

    public void OnItemReceived(Item item)
    {
        
    }

    public Vector3 GetPosition()
    {
        return _receptionPoint.localPosition;
    }

    private void OnShopingCartEntered(ShopingCart cart)
    {
        cart.TryGetItemForMoney(this);
    }

    private void OnShopingCartExited(ShopingCart cart)
    {
        
    }
}
