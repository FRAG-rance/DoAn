using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class WeatherState : MonoBehaviour
{
    [SerializeField] private State currentWeatherState;
    [HideInInspector] public UnityEvent<State> OnWeatherChanged;

    public enum State
    {
        Duststorm,
        Tornado,
        Lightning
    }


    public WeatherState.State GetRandomWeather(float duststormPct, float tornadoPct, float lightningPct)
    {
        float totalPct = duststormPct + tornadoPct + lightningPct;
        float randomValue = Random.Range(0f, totalPct);
        if (randomValue < duststormPct) return WeatherState.State.Duststorm;
        else if (randomValue < duststormPct + tornadoPct) return WeatherState.State.Tornado;
        return WeatherState.State.Lightning;
    }
    public State GetCurrentState()
    {
        return currentWeatherState;
    }
    public void SetCurrentState(WeatherState.State weatherState)
    {
        currentWeatherState = weatherState;
    }
    public void CycleWeatherState(float duststormPct, float tornadoPct, float lightningPct)
    {
        SetCurrentState(GetRandomWeather(duststormPct, tornadoPct, lightningPct));
        OnWeatherChanged?.Invoke(currentWeatherState);
    }
}
