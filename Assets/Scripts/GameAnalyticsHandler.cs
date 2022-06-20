using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnalyticsHandler
{
    private ILevelsControlEventHandler _levelsControlEventHandler;
    private IBalanceInformant _balanceInformant;

    public void Intialize(ILevelsControlEventHandler levelsControlEvent, 
        IBalanceInformant balanceInformant)
    {
        _levelsControlEventHandler = levelsControlEvent;
        _balanceInformant = balanceInformant;

        _levelsControlEventHandler.LevelStart += OnLevelStart;
        _levelsControlEventHandler.LevelChanges += OnLevelChanges;
        _levelsControlEventHandler.LevelRestarted += OnLevelRestarted;
    }
    ~GameAnalyticsHandler()
    {
        _levelsControlEventHandler.LevelStart -= OnLevelStart;
        _levelsControlEventHandler.LevelChanges -= OnLevelChanges;
        _levelsControlEventHandler.LevelRestarted -= OnLevelRestarted;
    }

    private void OnLevelStart(int level)
    {
        //TinySauce.OnGameStarted("level_" + level);
    }

    private void OnLevelChanges(int level)
    {
       // TinySauce.OnGameFinished(true, _balanceInformant.AmountMoney,
      // "level_" + level);
    }

    private void OnLevelRestarted(int level)
    {
        //    TinySauce.OnGameFinished(false,_balanceInformant.AmountMoney, 
        //        "level_" + level);
    }
}
