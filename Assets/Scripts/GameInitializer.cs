using UnityEngine;
using DG.Tweening;

public class GameInitializer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private TouchHandler _touchHandler;
    [SerializeField] private AddedMoneyEffect _moneyEffect;
    [SerializeField] private BoostSpeedEffect _speedEffect;

    [Header("Spawners")]
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private FinishSpawner _finishSpawner;

    [Header("Handlers")]
    [SerializeField] private LevelsControl _levelsControl;
    [SerializeField] private CameraHendler _cameraHendler;
    [SerializeField] private PlayerWallet _wallet;

    private BinarySaveSystem _saveSystem;
    private GameAnalyticsHandler _gameAnalyticsHandler;

    private void OnEnable()
    {
        _levelsControl.LevelStart += OnLevelStart;
    }

    private void OnDisable()
    {
        _levelsControl.LevelStart -= OnLevelStart;
    }

    private void Start()
    {
        _saveSystem = new BinarySaveSystem();
        _gameAnalyticsHandler = new GameAnalyticsHandler();

        SaveData data = _saveSystem.Load();

        _wallet.Initialize(data.Money);
        _levelsControl.Initialize(_uIManager, data.Level);
        _gameAnalyticsHandler.Intialize(_levelsControl, _wallet);
      
        UIInputData uIInputData = new UIInputData(_levelsControl, _wallet, _touchHandler);
        _uIManager.Initialize(_playerSpawner, _finishSpawner, uIInputData, _wallet);
        _touchHandler.Initialize(_uIManager);
        _levelsControl.StartLevel();
    }

    private void OnApplicationQuit()
    {
        SaveData data = new SaveData(_levelsControl.CurrentLevelID, _wallet.AmountMoney);
        _saveSystem.Save(data);
    }

    private void OnLevelStart(int levelId)
    {
        Player player = _playerSpawner.Spawn();
        player.Initialize(_touchHandler, _speedEffect, _moneyEffect, _wallet);

        Level level = _levelSpawner.Spawn(levelId);
        level.Initialize();

        FinishZone finishZone = _finishSpawner.Spawn(level.LengthLevelRoad);
        finishZone.Initialize(_cameraHendler, _wallet);

        _cameraHendler.SetFollow(player.transform);
        _wallet.ResetMoneyPerLevel();
    }
}
