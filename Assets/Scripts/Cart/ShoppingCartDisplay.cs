using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShoppingCartDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _cashHeader;
    private int _currentCash = 0;
    private Tween _sizeTween;
    private Tween _hideTween;

   

    public void AddCash(int cash)
    {
        Show();
        if ((cash > 0 && _currentCash < 0) || (cash < 0 && _currentCash > 0)) _currentCash = 0;
        _currentCash += cash;
        Color color = _currentCash > 0 ? Color.green : Color.red;
        AppearanceAnimation(color, _currentCash);
    }

    private void AppearanceAnimation(Color color, int cash)
    {
        string shortScaleNumber = ShortScale.ParseInt(cash, 3, 1000, true);
        _cashHeader.text = "$" + shortScaleNumber;
        _cashHeader.color = color;

        if (_sizeTween.IsActive() == false)
            _sizeTween = transform.DOScale(transform.localScale.x + 0.25f, 0.07f).SetLoops(2, LoopType.Yoyo);

        if (_hideTween.IsActive() == true) _hideTween.Restart();
        else _hideTween = DOVirtual.DelayedCall(1, Hide);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        _currentCash = 0;
    }
}
