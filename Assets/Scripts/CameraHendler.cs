using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHendler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera2;

    public void SetFollow(Transform transform)
    {
        ResetPriority();
        _virtualCamera1.Follow = transform;
        _virtualCamera2.Follow = transform;
    }

    public void ChangeVirtualCamera()
    {
        int priority = _virtualCamera1.Priority;
        _virtualCamera1.Priority = _virtualCamera2.Priority;
        _virtualCamera2.Priority = priority;
    }
    private void ResetPriority()
    {
        _virtualCamera1.Priority = 10;
        _virtualCamera2.Priority = 9;
    }
}
