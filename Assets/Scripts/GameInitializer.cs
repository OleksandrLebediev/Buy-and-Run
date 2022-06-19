using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private TouchHandler _touchHandler;
    [SerializeField] private LevelsControl _levelsControl;

    [SerializeField] private Player _player;
    private BinarySaveSystem _saveSystem;

    private void Start()
    {
        _player.Initialize(_touchHandler);

        _saveSystem = new BinarySaveSystem();
        SaveData data = _saveSystem.Load();


        UIInputData uIInputData = new UIInputData(_levelsControl, _player.Wallet, _touchHandler);
        _uIManager.Initialize(_player, uIInputData, _player.Wallet);

        //_levelsSwitcher.LevelChanges += OnLevelChanges;
        //_levelsSwitcher.LevelRestarted += OnLevelRestarted;
        //_levelsSwitcher.LevelStart += OnLevelStart;
    }

    //private void OnLevelStart()
    //{
    //    TinySauce.OnGameStarted("level_" + _levelsSwitcher.CurrentLevel);
    //    SaveData data = new SaveData(_levelsSwitcher.CurrentLevel, _playerProvider.Player.Wallet.AmountMoney);
    //    _saveSystem.Save(data);
    //}

    //private void OnLevelRestarted()
    //{
    //    TinySauce.OnGameFinished(false, _playerProvider.Player.Wallet.AmountMoneyPerLevel,
    //        "level_" + _levelsSwitcher.CurrentLevel);
    //}

    //private void OnLevelChanges()
    //{
    //    TinySauce.OnGameFinished(true, _playerProvider.Player.Wallet.AmountMoneyPerLevel,
    //        "level_" + _levelsSwitcher.CurrentLevel);
    //}

    //private void OnDestroy()
    //{
    //    _levelsSwitcher.LevelChanges -= OnLevelChanges;
    //    _levelsSwitcher.LevelRestarted -= OnLevelRestarted;
    //    _levelsSwitcher.LevelStart -= OnLevelStart;
    //}

    private void OnApplicationQuit()
    {
        //SaveData data = new SaveData(_levelsSwitcher.CurrentLevel, _playerProvider.Player.Wallet.AmountMoney);
        //_saveSystem.Save(data);
    }
}
