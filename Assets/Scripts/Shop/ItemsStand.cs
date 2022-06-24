using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsStand : MonoBehaviour, IItemsSender
{
    private List<Item> _items = new List<Item>();
    private ItemData _data;

    private int _maxWidth = 4;
    private int _maxLength = 7;

    private int _nextXPosition = 0;
    private int _nextYPosition = 0;
    private int _nextZPosition = 0;

    private Vector3 _startPosition = new Vector3(0.6f, 0.25f, -2.7f);
    private float _stepPositionX = -0.4f;
    private float _stepPositionY = -0.2f;
    private float _stepPositionZ = 0.5f;

    private float _delay = 0.1f;

    private int _ñapacity => _maxWidth * _maxLength;
    public bool IsEmpty => _items.Count == 0;

    public void Initialize(ItemData data)
    {
        _data = data;
    }

    public Item GetItem(string name = null)
    {
        Item item = _items[0];
        _items.Remove(item);
        return item;
    }

    public void AddAllItems()
    {
        StartCoroutine(AddingAllItemsDelayCoroutin());
    }

    private void AddItem()
    {
        Vector3 position = GetPositionFreeStance();
        Item item = Instantiate(_data.Item, position, _data.Item.transform.localRotation);
        item.Initialize(_data.ItemName, _data.Price);
        item.transform.localScale = Vector3.zero;
        item.transform.SetParent(transform, false);
        item.transform.DOScale(1, 0.3f);
        _items.Add(item);
    }

    private Vector3 GetPositionFreeStance()
    {
        Vector3 newPosition = _startPosition;

        newPosition.x = _startPosition.x + _stepPositionX * _nextXPosition;
        newPosition.y = _startPosition.y + _stepPositionY * _nextYPosition;
        newPosition.z = _startPosition.z + _stepPositionZ * _nextZPosition;

        _nextXPosition++;
        _nextYPosition++;

        if (_nextXPosition == _maxWidth)
        {
            _nextXPosition = 0;
            _nextYPosition = 0;
            _nextZPosition++;

            if (_nextZPosition == _maxLength)
            {
                _nextZPosition = 0;
            }
        }
        return newPosition;
    }

    private IEnumerator AddingAllItemsDelayCoroutin()
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);

        for (int i = 0; i < _ñapacity; i++)
        {
            yield return wait;
            AddItem();
        }
    }
}
