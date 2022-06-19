using UnityEngine.Events;

public interface IUIEventsHandler
{
    public event UnityAction PressedRestartButton;
    public event UnityAction PressedNextButton;
}
