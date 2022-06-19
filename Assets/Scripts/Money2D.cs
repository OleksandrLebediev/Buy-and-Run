using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Money2D : MonoBehaviour
{
    private float _explostionSpeed = 0.5f;
    private float _explostionFlySpeed = 2.5f;
    private RectTransform _rectTransform;
    private Vector3 _startPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.localPosition;
    }

    public void PlayEffect(RectTransform taget, UnityAction callback)
    {
        gameObject.SetActive(true);
        float angle = Random.Range(0f, 360f);
        float radius = Random.Range(0.1f, 0.15f) * Screen.width;
        Vector3 exp_position = new Vector3(radius * Mathf.Sin(angle), radius * Mathf.Cos(angle), 0);

        float exp_duration = radius / (_explostionSpeed * Screen.width);

        float fly_duration = Vector3.Distance(taget.position, exp_position) / (_explostionFlySpeed * Screen.width);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOMove(_rectTransform.position + exp_position, exp_duration).SetEase(Ease.OutCubic));
        sequence.Append(_rectTransform.DOMove(taget.position, fly_duration).SetEase(Ease.InCubic));
        sequence.AppendCallback(() =>
        {
            gameObject.SetActive(false);
            callback();
        });
    }

    public void ResetPosition()
    {
        _rectTransform.localPosition = _startPosition;
    }
}