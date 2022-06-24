using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ShopingCart>(out ShopingCart cart))
        {
            cart.SpeedBoost();
        }
    }
}
