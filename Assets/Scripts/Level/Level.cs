using UnityEngine;

public class Level : MonoBehaviour
{
    public int LengthLevelRoad => _road.Length;
    private Road _road;

    private void Awake()
    {
        _road = GetComponentInChildren<Road>();
    }

    public void Initialize()
    {

    }
}
