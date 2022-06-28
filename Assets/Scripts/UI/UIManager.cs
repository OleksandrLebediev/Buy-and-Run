using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour, IUIEventsHandler
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private TransitionScreen _transitionScreen;
    
    private IPlayerEvents _playerEvents;
    private IFinishEvents _finishEvents;
    private UIInputData _uIInputData;

    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;

    public void Initialize(IPlayerEvents playerEvents, IFinishEvents finishEvents,
        UIInputData uIInputData, PlayerWallet playerWallet)
    {
        _playerEvents = playerEvents;
        _finishEvents = finishEvents;
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
        DOTween.Clear();
        _loseScreen.Hide();
        _transitionScreen.Show();
        _mainMenu.Show();
        PressedRestartButton?.Invoke();
    }

    private void OnPressedNextButton()
    {
        DOTween.Clear();
        _winScreen.Hide();
        _transitionScreen.Show();
        _mainMenu.Show();
        PressedNextButton?.Invoke();
    }

    public void OnPlayerDied()
    {
        _loseScreen.Show(_uIInputData.LevelsInformant.CurrentLevelID + 1,
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void OnLevelCompleted()
    {
        _winScreen.Show(_uIInputData.LevelsInformant.CurrentLevelID + 1, 
            _uIInputData.BalanceInformant.AmountMoneyPerLevel);
    }

    private void Subscribe()
    {
        _loseScreen.PressedRestartButton += OnPressedRestartButton;
        _winScreen.PressedNextButton += OnPressedNextButton;
        _playerEvents.PlayerDied += OnPlayerDied;
        _finishEvents.LevelCompleted += OnLevelCompleted;
    }

    private void Unsubscribe()
    {
        _loseScreen.PressedRestartButton -= OnPressedRestartButton;
        _winScreen.PressedNextButton -= OnPressedNextButton;
        _playerEvents.PlayerDied -= OnPlayerDied;
        _finishEvents.LevelCompleted -= OnLevelCompleted;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}
