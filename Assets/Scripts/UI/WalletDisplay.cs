using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _amountMoney;
    [SerializeField] private PlayerWallet _playerWallet;

    private Animator _animator;

    public void Initializer(PlayerWallet playerWallet)
    {
        _playerWallet = playerWallet;
    }

    private void OnEnable()
    {
        _playerWallet.AmountMoneyChanged += OnAmountMoneyChanged;
    }

    private void OnDisable()
    {
        _playerWallet.AmountMoneyChanged -= OnAmountMoneyChanged;
    }

    private void OnAmountMoneyChanged(int amount)
    {
        _amountMoney.text = amount.ToString();
    }
}
