using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishSpawner : MonoBehaviour, IFinishEvents
{
    [SerializeField] private FinishZone _finishZoneTemplace;
    private FinishZone _finishZone;

    public event UnityAction LevelCompleted;

    public FinishZone Spawn(int spawnPoint)
    {
        if (_finishZone != null)
            RemoveFinishZone(_finishZone);

        _finishZone = Instantiate(_finishZoneTemplace);
        _finishZone.transform.position = new Vector3(0, 0, spawnPoint);
        _finishZone.LevelCompleted += OnLevelCompleted;
        return _finishZone;
    }

    private void OnLevelCompleted()
    {
        LevelCompleted?.Invoke();
    }

    private void RemoveFinishZone(FinishZone finishZone)
    {
        Destroy(finishZone.gameObject);
    }
}
