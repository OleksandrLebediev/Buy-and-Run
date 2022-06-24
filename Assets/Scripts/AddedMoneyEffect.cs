using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AddedMoneyEffect : MonoBehaviour
{
    [SerializeField] private Money2D _moneyTemplate;
    [SerializeField] private RectTransform _target;

    private int _amountToMoneyPool = 40;
    private RectTransform _rect;
    private List<Money2D> _money = new List<Money2D>();
    private int _count;

    public event UnityAction<int> Effect—ompleted;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        SpawnMoney();
    }

    public void Play(int count)
    {
        _count = count;
        for (int i = 1; i <= count; i++)
        {
            Money2D money = GetMoney2D();
            money.ResetPosition();
            money?.PlayEffect(_target, MoneyOnTarget);
        }
    }

    private Money2D GetMoney2D()
    {
        for (int i = 0; i < _amountToMoneyPool; i++)
        {
            if (!_money[i].gameObject.activeInHierarchy)
            {
                return _money[i];
            }
        }
        return null;
    }

    private void SpawnMoney()
    {
        for (int i = 1; i <= _amountToMoneyPool; i++)
        {
            Money2D money2D = Instantiate(_moneyTemplate, _rect);
            money2D.gameObject.SetActive(false);
            _money.Add(money2D);
        }
    }

    private void MoneyOnTarget()
    {
        Effect—ompleted?.Invoke(_count);
    }
}
