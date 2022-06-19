using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, ITouchHandler, IDragHandler, IPointerDownHandler
{
    private IUIEventsHandler _answer;
    private bool _isFirstClick;

    public event UnityAction FirstTouch;
    public event UnityAction<float> MovingTouch;

    //public void Initialize(IUIAnswer uIAnswer)
    //{
    //    _answer = uIAnswer;
    //    uIAnswer.PressedRestartButton += OnRestartFirstClick;
    //    uIAnswer.PressedNextButton += OnRestartFirstClick;
    //}
    //private void OnDestroy()
    //{
    //    _answer.PressedRestartButton -= OnRestartFirstClick;
    //    _answer.PressedNextButton -= OnRestartFirstClick;
    //}

    private void OnRestartFirstClick()
    {
        _isFirstClick = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isFirstClick == true) return;

        FirstTouch?.Invoke();
        _isFirstClick = true;
    }    
    

    public void OnDrag(PointerEventData eventData)
    {
        MovingTouch?.Invoke(eventData.delta.x);
    }
}
