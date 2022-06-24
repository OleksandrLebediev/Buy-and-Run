using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _countHeader;
    [SerializeField] private Image _item;
    [SerializeField] private Image _smile;
    [SerializeField] private Sprite[] _spriteSmiles;

    private int _amountItems;

    public void Initialize(int amountItems, Sprite orderSprite)
    {
        _countHeader.text = $"{0}/{amountItems}";
        _item.sprite = orderSprite;
        _amountItems = amountItems;
        _smile.gameObject.SetActive(false);
    }

    public void UpdateCountItems(int currentAmountItems)
    {
        _countHeader.text = $"{currentAmountItems}/{_amountItems}";
    }

    public void ShowSmile()
    {
        _countHeader.gameObject.SetActive(false);
        _item.gameObject.SetActive(false);

        _smile.transform.localScale = Vector3.zero;
        _smile.gameObject.SetActive(true);


        Sprite smile = _spriteSmiles[Random.Range(0, _spriteSmiles.Length)];
        _smile.sprite = smile;

        _smile.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
}
