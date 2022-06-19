using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopSellZone : MonoBehaviour
{
    public event UnityAction<IItemsRecipient> RecipientEntered;
    public event UnityAction<IItemsRecipient> RecipientExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IItemsRecipient>(out IItemsRecipient recipient))
        {
            RecipientEntered?.Invoke(recipient);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IItemsRecipient>(out IItemsRecipient recipient))
        {
            RecipientExited?.Invoke(recipient);
        }
    }
}
