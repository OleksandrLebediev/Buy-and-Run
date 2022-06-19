using DG.Tweening;
using UnityEngine;

public class MultiplierBoard : MonoBehaviour
{
    [SerializeField] private Transform _place;
    [SerializeField] private Transform _body;
    [SerializeField] private ParticleSystem[] confrtti;

    private MeshRenderer _meshRenderer;
    private MultiplierBoardDisplay _display;

    private void Awake()
    {
        _meshRenderer = _body.GetComponent<MeshRenderer>();
        _display = GetComponentInChildren<MultiplierBoardDisplay>();

        foreach (var item in confrtti)
        {
            item.Stop();
        }
    }

    public void SetDisplayMultiplier(float multiplierValue)
    {
        _display.SetMultiplier(multiplierValue);
    }

    public Color GetColor()
    {
        return _meshRenderer.material.color;//GetColor("_Color");
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color; //("_Color", color);
    }


    public void ActivatePlace()
    {
        _place.DOLocalMoveZ(-1.5f, 0.2f);
    }

    public void ActivateConfetti()
    {
        foreach (var item in confrtti)
        {
            item.Play();
        }
    }

}
