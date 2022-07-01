using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHendler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera1;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera2;
    [SerializeField] private CinemachineBrain _mainCamera;
    [SerializeField] Vector2 DefaultResolution = new Vector2(720, 1280);
    [SerializeField, Range(0f, 1f)] private float WidthOrHeight = 0;

    private float initialSize;
    private float targetAspect;

    private float initialFov;
    private float horizontalFov = 120f;

    private void Awake()
    {
        SetOrthographicSize();
    }

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

    public void SetPhysicsUpdateMethod()
    {
        _mainCamera.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
    }

    private void ResetPriority()
    {
        _virtualCamera1.Priority = 10;
        _virtualCamera2.Priority = 9;
        _mainCamera.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
    }

    private void SetOrthographicSize()
    {

        initialSize = _virtualCamera1.m_Lens.OrthographicSize;
        targetAspect = DefaultResolution.x / DefaultResolution.y;

        initialFov = _virtualCamera1.m_Lens.FieldOfView;
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
    }


    private void Update()
    {

        if (_virtualCamera1.m_Lens.Orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / _virtualCamera1.m_Lens.Aspect);
            _virtualCamera1.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(horizontalFov, _virtualCamera1.m_Lens.Aspect);
            _virtualCamera1.m_Lens.FieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
        }
    }


    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}
