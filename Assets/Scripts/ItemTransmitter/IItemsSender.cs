using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsSender
{
    public Item GetItem();
    public bool IsEmpty { get; }
}
