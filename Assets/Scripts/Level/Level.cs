using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _lastPlatformPosition;
    public Vector3 LastPlatformPosition => _lastPlatformPosition.position;
}
