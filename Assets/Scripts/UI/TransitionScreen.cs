using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TransitionScreen : MonoBehaviour
{
    [SerializeField] private Image _imageTransition;

    public void Show()
    {
        gameObject.SetActive(true);
        _imageTransition.DOFade(0, 2).OnComplete(() => { 
            gameObject.SetActive(false);
            _imageTransition.color = Color.black;
        });
    }

    public void Hide()
    {
        gameObject.SetActive(true);
        _imageTransition.DOFade(1, 0).OnComplete(() => { gameObject.SetActive(false); });
       
    }
}
