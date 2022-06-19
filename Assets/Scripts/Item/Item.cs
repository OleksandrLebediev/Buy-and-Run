using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    public string Name { get; private set; }
    public int Price { get; private set; }

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Initialize(string name, int price)
    {
        Name = name;
        Price = price;
        DisablePhysics();
    }

    public void EnablePhysics()
    {
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
    }

    public void DisablePhysics()
    {
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
    }

    public void Explosion(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void Remove()
    {
        Destroy(this);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
