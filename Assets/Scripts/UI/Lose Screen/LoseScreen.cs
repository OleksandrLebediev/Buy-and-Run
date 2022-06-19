using CommonUIElement;
using LoseScreenElement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private LevelFailedTitle _levelFailedTitle;
    [SerializeField] private RestartButton _restartButton;

    private readonly float _delayBetweenShowed = 0.5f;
    private readonly float _durationShowAnimation = 0.5f;

    public event UnityAction PressedRestartButton;

    public void Initialize()
    {
        _levelFailedTitle.Initialize(_durationShowAnimation);
        _restartButton.Initialize(_durationShowAnimation);

        _restartButton.Subscribe(OnPressedRestartButton);
        _levelFailedTitle.Hide();
        _restartButton.Hide();
    }

    private void OnDestroy()
    {
        _restartButton.Unsubscribe(OnPressedRestartButton);
    }

    public void Show(int numberCurrentLevel, int amountMoneyPerLevel)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowScenarioCoroutine(numberCurrentLevel, amountMoneyPerLevel));
    }

    public void Hide()
    {
        _levelFailedTitle.Hide();
        _restartButton.Hide();
        gameObject.SetActive(false);
    }

    private void OnPressedRestartButton()
    {
        PressedRestartButton?.Invoke();
    }

    private IEnumerator ShowScenarioCoroutine(int numberCompletedLevel, int amountMoneyPerLevel)
    {
        yield return new WaitForSeconds(_delayBetweenShowed);
        _levelFailedTitle.Show(numberCompletedLevel);
        yield return new WaitForSeconds(_delayBetweenShowed);
        _restartButton.Show();
    }
}
