using UnityEngine;
using UnityEngine.Events;

public interface IBuyer : IItemsRecipient
{
    public int OrderPrice { get; }
    public int OrderAmountItems { get; }
    public string OrderItemName { get; }
}