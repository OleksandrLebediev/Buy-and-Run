using UnityEngine;
using UnityEngine.Events;

public class LevelsControl : MonoBehaviour, ILevelsInformant, ILevelsControlEventHandler
{
    [SerializeField] private LevelSpawner _spawner;
    private IUIEventsHandler _uIEvents;
    private int _currentLevel = 0;

    public int CurrentLevelID => _currentLevel;

    public event UnityAction<int> LevelChanges;
    public event UnityAction<int> LevelRestarted;
    public event UnityAction<int> LevelStart;

    public void Initialize(IUIEventsHandler uIEvent, int currentLevel)
    {
        _currentLevel = currentLevel;
        _uIEvents = uIEvent;
        _uIEvents.PressedRestartButton += RestartLevel;
        _uIEvents.PressedNextButton += StartNextLevel;
        StartLevel(_currentLevel);
    }

    private void OnDestroy()
    {
       _uIEvents.PressedRestartButton -= RestartLevel;
       _uIEvents.PressedNextButton -= StartNextLevel;
    }

    public void StartLevel()
    {
        LevelStart?.Invoke(_currentLevel);
    }


    private void StartLevel(int level)
    {
        LevelStart?.Invoke(level);
    }

    private void StartNextLevel()
    {
        LevelChanges?.Invoke(_currentLevel);
        _currentLevel++;
        if (_currentLevel >= _spawner.LevelsCount)
        {
            _currentLevel = Random.Range(0, _spawner.LevelsCount);
        }
        StartLevel(_currentLevel);
    }

    private void RestartLevel()
    {
        LevelRestarted?.Invoke(_currentLevel);
        StartLevel(_currentLevel);
    }
}



