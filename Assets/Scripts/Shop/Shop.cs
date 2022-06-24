using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopSellZone _shopSellZone;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ItemsStand _itemsStand;
    [SerializeField] private ShopDisplay _shopDisplay;

    private ItemTransmitter _transmitter = new ItemTransmitter();
    private Coroutine _transmittingCoroutine;

    private void Start()
    {
        _shopSellZone.RecipientEntered += OnRecipientEntered;
        _shopSellZone.RecipientExited += OnRecipientExited;
        _itemsStand.Initialize(_itemData);
        _itemsStand.AddAllItems();
        _shopDisplay.UpdatePriceDisplay(_itemData.Price, _itemData.Icon);
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
