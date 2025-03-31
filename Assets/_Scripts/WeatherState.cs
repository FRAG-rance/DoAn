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
    [SerializeField] private float duststormPct;
    [SerializeField] private float tornadoPct;
    [SerializeField] private float lightningPct;
    [SerializeField] private State currentWeatherState;
    [HideInInspector] public UnityEvent<State> OnWeatherChanged;

    public enum State
    {
        Duststorm,
        Tornado,
        Lightning
    }

    public float GenRandomPct()
    {
        return Random.Range(0f, 100.001f);
    }

    public void RandomizeWeatherPct()
    {
        duststormPct = GenRandomPct();
        tornadoPct = GenRandomPct();
        lightningPct = GenRandomPct();
    }

    public State GetRandomWeather(float duststormPct, float tornadoPct, float lightningPct)
    {
        float totalPct = duststormPct + tornadoPct + lightningPct;
        float randomValue = Random.Range(0f, totalPct);
        if (randomValue < duststormPct) return State.Duststorm;
        else if (randomValue < duststormPct + tornadoPct) return State.Tornado;
        return State.Lightning;
    }
    public State GetCurrentState()
    {
        return currentWeatherState;
    }
    public void CycleWeatherState()
    {
        RandomizeWeatherPct();
        currentWeatherState = GetRandomWeather(duststormPct, tornadoPct, lightningPct);
        OnWeatherChanged?.Invoke(currentWeatherState);
    }
}
