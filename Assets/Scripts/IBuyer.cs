using UnityEngine;
using UnityEngine.Events;

public interface IBuyer : IItemsRecipient
{
    public int OrderPrice { get; }
    public string OrderItemName { get; }
}