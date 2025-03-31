using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WeatherEffect : MonoBehaviour
{
    [SerializeField] WeatherState _weatherState;
    [SerializeField] WeatherParameters currentWeatherEffectParameters;
    [SerializeField] WeatherParameters targetWeatherEffectParameters;
   
    [SerializeField] WeatherParameters stormWeatherParameters;

    [SerializeField] WeatherParameters tornadoWeatherParameters;

    [SerializeField] WeatherParameters lightningWeatherParameters;

    [SerializeField] private LightningEffect lightningEffect;
    private void Awake()
    {
        currentWeatherEffectParameters = stormWeatherParameters;
        targetWeatherEffectParameters = new WeatherParameters();
    }
    private void Start()
    {
        SetWeatherEffect(_weatherState.GetCurrentState());
    }

    public void SetWeatherEffect(WeatherState.State state)
    {
        switch(state)
        {
            case WeatherState.State.Duststorm :
                currentWeatherEffectParameters = stormWeatherParameters;
                break;
            case WeatherState.State.Tornado:
                currentWeatherEffectParameters = tornadoWeatherParameters;
                break;
            case WeatherState.State.Lightning:
                currentWeatherEffectParameters = lightningWeatherParameters;
                break;
        }
    }

    public void UpdateWeatherEffect()
    {
    }
}
