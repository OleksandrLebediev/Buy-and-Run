using UnityEngine.Events;

public interface ITouchHandler
{
    public event UnityAction FirstTouch;
    public event UnityAction<float> MovingTouch;
}