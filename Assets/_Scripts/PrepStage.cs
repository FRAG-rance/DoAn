using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrepStage : IGameplayState
{

    public PrepStage(int sol, ImageAnimation timeDisplay, TextMeshProUGUI dayDisplay)
    {
        CardSystem.currentDeckSize = CardSystem.baseDeckSize;
        timeDisplay.Play();
        dayDisplay.text = "DAY " + sol;
    }

    public void Enter()
    {       
    }

    public void Execute()
    {
        WeatherManager.Instance.SetPercentage();
    }

    public void Exit()
    {
        
    }
}
