using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsRecipient
{
    public void OnItemReceiving(Item item);
    public void OnItemReceived(Item item);
    public Vector3 GetPosition();
    public Transform ThisTransform { get; }
    public JumpData JumpData { get; }
    public float ReceiveDelay { get; }
}
