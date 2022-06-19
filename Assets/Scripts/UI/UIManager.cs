using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour, IUIEventsHandler
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private TransitionScreen _transitionScreen;
    
    private IPlayerEvents _playerEvents;
    private UIInputData _uIInputData;

    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;

    public void Initialize(IPlayerEvents playerEvents, 
        UIInputData uIInputData, PlayerWallet playerWallet)
    {
        _playerEvents = playerEvents;
        _uIInputData = uIInputData;

        _transitionScreen.Show();
        _mainMenu.Initializer(playerWallet, uIInputData);
        _winScreen.Initialize();
        _loseScreen.Initialize();

        _loseScreen.Hide();
        _winScreen.Hide();

        Subscribe();
    }

    private void OnPressedRestartButton()
    {
        _loseScreen.Hide();
        _transitionScreen.Show();
        _mainMenu.Show();
        PressedRestartButton?.Invoke();
    }

    private void OnPressedNextButton()
    {
        _winScreen.Hide();
        _transitionScreen.Show();
        _mainMenu.Show();
        PressedNextButton?.Invoke();
    }

    public void OnCubeDestroyed()
    {
        _loseScreen.Show(_uIInputData.LevelsInformant.CurrentLevelID + 1,
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void OnRewardPlaygroundPassed()
    {
        _winScreen.Show(_uIInputData.LevelsInformant.CurrentLevelID + 1, 
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void Subscribe()
    {
        _loseScreen.PressedRestartButton += OnPressedRestartButton;
        _winScreen.PressedNextButton += OnPressedNextButton;
        _playerEvents.CubeDestroyed += OnCubeDestroyed;
    }

    private void Unsubscribe()
    {
        _loseScreen.PressedRestartButton -= OnPressedRestartButton;
        _winScreen.PressedNextButton -= OnPressedNextButton;
        _playerEvents.CubeDestroyed -= OnCubeDestroyed;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
