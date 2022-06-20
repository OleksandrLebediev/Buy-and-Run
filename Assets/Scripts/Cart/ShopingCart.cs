using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopingCart : MonoBehaviour, IItemsRecipient, IItemsSender
{
    [SerializeField] private ShoppingCartDisplay _display;

    private List<Item> _items = new List<Item>();
    private ItemTransmitter _itemTransmitter = new ItemTransmitter();
    private JumpData _jumpData = new JumpData(1, 4, 0.5f);

    private float _receiveDelay = 0.05f;

    private readonly float _length = 1;
    private readonly float _width = 1;
    private readonly float _offset = 0.4f;

    public Transform ThisTransform => transform;
    public JumpData JumpData => _jumpData;
    public bool IsEmpty => _items.Count == 0;
    public float TransmitDelay => _receiveDelay;
    public float ReceiveDelay => _receiveDelay;

    public event UnityAction<int> ItemAdded;
    public event UnityAction<int> ItemRemoved;
    public event UnityAction<int> ItemSold;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void TryGetItemForMoney(IBuyer buyer)
    {
        Item item = _items.Find(x => x.Name == buyer.OrderItemName);
        if (item == null) return;
        _itemTransmitter.TransmittingItem(item, this, buyer);
        ItemSold?.Invoke(buyer.OrderPrice);
        RemoveItem(item);
    }

    private void AddItem(Item item)
    {
        _items.Add(item);
        _display.AddCash(item.Price);
        ItemAdded?.Invoke(item.Price);
    }

    private void RemoveItem(Item item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(item.Price);
    }

    public void TakeDamage(int damage)
    {
        while (damage > 0 && _items.Count != 0)
        {
            Item item = _items[0];
            RemoveItem(item);
            _display.AddCash(-item.Price);
            item.SetParent(null);
            item.Explosion(transform.up * 20);
            Destroy(item.gameObject, 4);
            damage--;
        }
    }

    public Item GetItem()
    {
        Item item = _items[0];
        RemoveItem(item);
        return item;
    }
  
    public void OnItemReceiving(Item item)
    {
        AddItem(item);
    }

    public void OnItemReceived(Item item)
    {
        item.EnablePhysics();
    }

    public Vector3 GetPosition()
    {
        return new Vector3(Random.Range(_offset - _width, _width - _offset), 1, Random.Range(_offset - _length, _length));
    }
}
