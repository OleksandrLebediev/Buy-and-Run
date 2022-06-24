using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsSender
{
    public Item GetItem(string Name = null);
    public bool IsEmpty { get; }
}
