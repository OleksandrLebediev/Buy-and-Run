using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] Level _level;
    public int LevelsCount => _levels.Count;

    public Level Spawn(int levelID)
    {
        if (_level != null)
           RemoveLevel(_level);


        //_level = GameObject.FindObjectOfType<Level>();
        _level = Instantiate(_levels[0]);
        return _level;
    }

    private void RemoveLevel(Level level)
    {
        Destroy(level.gameObject);
    }
}
