using TMPro;
using UnityEngine;

public class LevelHeader : MonoBehaviour
{
    [SerializeField] private TMP_Text _header;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ILevelsInformant levelsInformant)
    {
        _header.text = $"Level {levelsInformant.CurrentLevelID + 1}";
    }
}
