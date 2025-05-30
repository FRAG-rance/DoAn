using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherStage : IGameplayState
{
    public WeatherStage(ImageAnimation timeDisplay)
    {
        timeDisplay.Stop();
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        WeatherManager.Instance.HandleWeatherChanged();
    }

    public void Exit()
    {
        EconSystem.Instance.HandleUpdateEcon();
        GameManager.sol++;
    }
}
