using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
//4, 1, 0.5f)
public class ItemTransmitter
{
    private void Transmitting(Item item, IItemsRecipient recipient)
    {
        JumpData data = recipient.JumpData;
        item.SetParent(recipient.ThisTransform);
        item.DisablePhysics();
        item.transform.DOLocalJump(recipient.GetPosition(), data.Power, data.Number, data.Duration).OnComplete(() =>
       {
           recipient.OnItemReceived(item);
       });
    }

    public IEnumerator MultiTransmittingCoroutine(IItemsSender sender, IItemsRecipient recipient, 
        string name = null, UnityAction<Item> action = null)
    {
        WaitForSeconds delay = new WaitForSeconds(recipient.ReceiveDelay);
        while (sender.IsEmpty == false)
        {
            Item item = sender.GetItem(name);
            if (item == null) yield break;
            Transmitting(item, recipient);
            recipient.OnItemReceiving(item);
            if(action != null) action?.Invoke(item);
            yield return delay;
        }
    }

    public IEnumerator MultiCountTransmittingCoroutine(int count, string nameItem ,
        IItemsSender sender, IItemsRecipient recipient, UnityAction action)
    {
        WaitForSeconds delay = new WaitForSeconds(recipient.ReceiveDelay);
        while (count != 0)
        {
            Item item = sender.GetItem(nameItem);
            if(item == null) yield break;

            Transmitting(item, recipient);
            recipient.OnItemReceiving(item);
            count--;
            yield return delay;
        }

        action.Invoke();
    }

    public void TransmittingItem(Item item,IItemsSender sender, IItemsRecipient recipient)
    {
        Transmitting(item, recipient);
        recipient.OnItemReceiving(item);
    }
}

public class JumpData
{
    private int _number;
    private float _power;
    private float _duration;

    public JumpData(int number, float power, float duration)
    {
        _number = number;
        _power = power;
        _duration = duration;
    }

    public int Number { get { return _number; } }
    public float Power { get { return _power; } }
    public float Duration { get { return _duration; } }
}


