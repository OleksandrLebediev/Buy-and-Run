using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _forwardSpeedBoost;
    [SerializeField] private float _horizontalSpeed;

    private ITouchHandler _touchHandler;
    private Vector3 _movePosition = Vector3.zero;
    private float _currentForwardSpeed;
    private float _horizontalPosition;
    private float _acceleration = 5;
    private bool _isMoving;

    public event UnityAction StartMoving;

    public void Initialize(ITouchHandler touchHandler)
    {
        _touchHandler = touchHandler;
        _touchHandler.MovingTouch += HorizontalMove;
        _touchHandler.FirstTouch += OnFirstTap;

        _currentForwardSpeed = _forwardSpeed;
    }

    private void Update()
    {
        if (_isMoving == false) return;

        MoveForward();
    }

    public void DisableMovement()
    {
        _isMoving = false;
    }

    public void SetSpeedBoost()
    {
        _currentForwardSpeed = _forwardSpeedBoost;
    }

    public void SetSpeed()
    {
        _currentForwardSpeed = _forwardSpeed;
    }

    private void OnDestroy()
    {
        _touchHandler.MovingTouch -= HorizontalMove;
        _touchHandler.FirstTouch -= OnFirstTap;
    }

    private void MoveForward()
    {
        if (_currentForwardSpeed < _forwardSpeed)
        {
            _currentForwardSpeed += Time.deltaTime * _acceleration;
        }
        transform.Translate(0, 0, _currentForwardSpeed * Time.deltaTime);
    }

    private void HorizontalMove(float direction)
    {
        if (_isMoving == false) return;

        _horizontalPosition = direction * _horizontalSpeed * Time.deltaTime;
        _movePosition = transform.position;
        _movePosition.x += _horizontalPosition;
        _movePosition.x = Mathf.Clamp(_movePosition.x, Road.LeftExtremePoint, Road.RightExtremePoint);

        transform.position = _movePosition;
    }

    private void OnFirstTap()
    {
        _isMoving = true;
        StartMoving?.Invoke();
    }
}
