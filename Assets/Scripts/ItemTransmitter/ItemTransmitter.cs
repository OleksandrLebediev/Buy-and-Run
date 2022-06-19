using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemTransmitter
{
    private void Transmitting(Item item, Vector3 target, UnityAction<Item> callback, Transform parent)
    {
        item.SetParent(parent);
        item.DisablePhysics();
        item.transform.DOLocalJump(target, 4, 1, 0.5f).OnComplete(() =>
       {
           callback.Invoke(item);
       });
    }

    public IEnumerator MultiTransmittingCoroutine(IItemsSender sender, IItemsRecipient recipient)
    {
        WaitForSeconds delay = new WaitForSeconds(recipient.ReceiveDelay);
        while (sender.IsEmpty == false)
        {
            Item item = sender.GetItem();
            Transmitting(item, recipient.GetPosition(), recipient.OnItemReceived, recipient.ThisTransform);
            recipient.OnItemReceiving(item);
            yield return delay;
        }
    }

    public void TransmittingItem(Item item, IItemsSender sender, IItemsRecipient recipient)
    {
        Transmitting(item, recipient.GetPosition(), recipient.OnItemReceived, recipient.ThisTransform);
        recipient.OnItemReceiving(item);
    }
}


