using TMPro;
using UnityEngine;

public class PlayerPriceDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _priceHeader;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdatePrice(int price)
    {
        string shortScaleNumber = ShortScale.ParseInt(price, 3, 1000, true);
        _priceHeader.text = "$" + shortScaleNumber;
    }
}
