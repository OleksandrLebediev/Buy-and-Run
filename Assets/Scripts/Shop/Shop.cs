using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopSellZone _shopSellZone;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemsStand _itemsStand;

    private ItemTransmitter _transmitter = new ItemTransmitter();
    private Coroutine _transmittingCoroutine;

    private void Start()
    {
        _shopSellZone.RecipientEntered += OnRecipientEntered;
        _shopSellZone.RecipientExited += OnRecipientExited;
        _itemsStand.Initialize(_itemData);
        _itemsStand.AddAllItems();
    }

    private void OnRecipientEntered(IItemsRecipient recipient)
    {
        _transmittingCoroutine = StartCoroutine(_transmitter.MultiTransmittingCoroutine(_itemsStand,
            recipient));
    }

    private void OnRecipientExited(IItemsRecipient recipient)
    {
       StopCoroutine(_transmittingCoroutine);
    }

    private void OnDestroy()
    {
        _shopSellZone.RecipientEntered -= OnRecipientEntered;
        _shopSellZone.RecipientExited -= OnRecipientExited;
    }
}
