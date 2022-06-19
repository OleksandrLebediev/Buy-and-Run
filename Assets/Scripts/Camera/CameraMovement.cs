using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Player _target;
    private bool _isFollow;

    private void Start()
    {
        Initialize(GameObject.FindObjectOfType<Player>());
    }

    public void Initialize(Player target)
    {
        _target = target;
        _isFollow = true;
    }

    public IEnumerator MoveToTarget(Transform target, float time)
    {
        while (true)
        {
            _offset = Vector3.Lerp(_offset, target.localPosition, time);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, time);
            if(Vector3.Distance(_offset, target.localPosition) < 0.001f)
            {
                yield break;
            }
            yield return null;
        }
    }

    private void LateUpdate()
    {
        if (_isFollow == false) return;
        
        Vector3 offsetTarget = _target.transform.position + _offset;
        transform.position = offsetTarget;
    }
}
