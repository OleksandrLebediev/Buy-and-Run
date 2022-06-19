using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyerZone : MonoBehaviour
{
    public event UnityAction<ShopingCart> ShopingCartEntered;
    public event UnityAction<ShopingCart> ShopingCartExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ShopingCart>(out ShopingCart cart))
        {
            ShopingCartEntered?.Invoke(cart);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ShopingCart>(out ShopingCart cart))
        {
            ShopingCartExited?.Invoke(cart);
        }
    }
}
