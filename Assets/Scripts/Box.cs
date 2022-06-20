using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IItemsRecipient
{
    [SerializeField] private Transform _body;

    [SerializeField] private Transform _earsLeft;
    [SerializeField] private Transform _earsRight;
    [SerializeField] private Transform _earsForward;
    [SerializeField] private Transform _earsBack;

    private readonly Vector3 _leftCloseRotation = new Vector3(0, 0, -125);
    private readonly Vector3 _rightCloseRotation = new Vector3(0, 0, 125);
    private readonly Vector3 _backCloseRotation = new Vector3(-125, 0, 0);
    private readonly Vector3 _forwardCloseRotation = new Vector3(125, 0, 0);

    private float _receiveDelay = 0.05f;
    private float _closeDuration = 1f;
    private float _openDuration = 0.1f;
    public Transform ThisTransform => transform;
    public float ReceiveDelay => _receiveDelay;
    public JumpData JumpData => _jumpData;

    private List<Item> _items = new List<Item>();
    private JumpData _jumpData = new JumpData(1, 5, 0.5f);

    private Tween _sizeTween;
    private float _scaleTarget = 1.2f;
    private float _scaleDuration = 0.1f;

    public Vector3 GetPosition()
    {
        return Vector3.zero;
    }

    public void OnItemReceived(Item item)
    {
        item.EnablePhysics();
    }

    public void OnItemReceiving(Item item)
    {
        if (_sizeTween.IsActive() == false)
            _sizeTween = transform.DOScale(_scaleTarget, _scaleDuration).SetLoops(2, LoopType.Yoyo);
        _items.Add(item);
    }

    public IEnumerator CloseCoroutine()
    {
        _earsLeft.DOLocalRotate(_leftCloseRotation, _closeDuration);
        _earsRight.DOLocalRotate(_rightCloseRotation, _closeDuration);
        _earsForward.DOLocalRotate(_forwardCloseRotation, _closeDuration);
        _earsBack.DOLocalRotate(_backCloseRotation, _closeDuration);
        yield return new WaitForSeconds(_closeDuration);
    }

    public IEnumerator OpenCoroutine()
    {
        _earsLeft.DOLocalRotate(Vector3.zero, _closeDuration);
        _earsRight.DOLocalRotate(Vector3.zero, _closeDuration);
        _earsForward.DOLocalRotate(Vector3.zero, _closeDuration);
        _earsBack.DOLocalRotate(Vector3.zero, _closeDuration);
        yield return new WaitForSeconds(_openDuration);
    }

    public void ItemsExplosion(int power)
    {
        foreach (var item in _items)
        {
            item.Explosion(Vector3.up * Random.Range(power / 2, power));
        }
    }
}
