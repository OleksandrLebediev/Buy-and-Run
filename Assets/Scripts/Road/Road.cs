using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public const float LeftExtremePoint = -4;
    public const float RightExtremePoint = 4;

    private Platform[] _platforms;
    public int Length => _platforms.Length * Platform.Length;

    private void Awake()
    {
        _platforms = GetComponentsInChildren<Platform>();
    }
}
