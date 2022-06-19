using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private WalletDisplay _walletDisplay;
    [SerializeField] private ShopButton _shopButton;
    [SerializeField] private LevelHeader _levelHeader;
    [SerializeField] private ControlHeader _controlHeader;
    
    private ITouchHandler _tapHandler;
    
    public void Initializer(PlayerWallet playerWallet, UIInputData inputData)
    {
        _walletDisplay.Initializer(playerWallet);
        _shopButton.Initialize();
        _levelHeader.Initialize(inputData.LevelsInformant);
        _controlHeader.Initialize();
        
        _tapHandler = inputData.FirstTapHandler;
        _tapHandler.FirstTouch += OnFirstTap;
    }
    
    private void OnFirstTap()
    {
        Hide();
    }

    public void Show()
    {
        _shopButton.Show();
        _levelHeader.Show();
        _controlHeader.Show();
    }

    public void Hide()
    {
        _shopButton.Hide();
        _levelHeader.Hide();
        _controlHeader.Hide();
    }


    private void OnDestroy()
    {
        _tapHandler.FirstTouch -= OnFirstTap;
    }
}
