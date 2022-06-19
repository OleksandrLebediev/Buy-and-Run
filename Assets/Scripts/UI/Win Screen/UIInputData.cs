using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputData 
{
    public UIInputData(ILevelsInformant levelsInformant, IBalanceInformant balanceInformant, ITouchHandler firstTapHandler)
    {
        LevelsInformant = levelsInformant;
        BalanceInformant = balanceInformant;
        FirstTapHandler = firstTapHandler;
    }

    public ILevelsInformant LevelsInformant { get; }
    public IBalanceInformant BalanceInformant { get; }
    public ITouchHandler FirstTapHandler { get; }
}
