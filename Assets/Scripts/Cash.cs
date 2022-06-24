using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ICashCollector>(out ICashCollector collector))
        {
            collector.CollectCash(1);
            Destroy(gameObject);
        }
    }
}
