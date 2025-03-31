using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI duststormPct;
    [SerializeField] private TextMeshProUGUI tornadoPct;
    [SerializeField] private TextMeshProUGUI lightningPct;

    [SerializeField] private bool isDuststorm;
    [SerializeField] private bool isTornado;
    [SerializeField] private bool isLightning;

    [SerializeField] private WeatherState _weatherState;
    [SerializeField] private WeatherEffect _weatherEffect;
    [SerializeField] private WeatherParameters _weatherParameters;

    private void OnEnable()
    {
        _weatherState.OnWeatherChanged.AddListener(HandleWeatherStateChanged);
    }

    private void OnDisable()
    {
        
    }

    public void HandleWeatherStateChanged(WeatherState.State state)
    {
        _weatherEffect.SetWeatherEffect(state);
        Debug.Log(_weatherState.GetCurrentState());
    }

    public void HandleWeatherChanged()
    {
        _weatherState.CycleWeatherState();
    }

    void Start()
    {
    }

}
