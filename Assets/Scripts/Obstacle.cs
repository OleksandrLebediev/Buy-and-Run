using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _damage;
    private bool isCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollision == true) return;

        if (other.TryGetComponent<ShopingCart>(out ShopingCart cart))
        {
            cart.TakeDamage(_damage);
            isCollision = true;
        }
    }
}
