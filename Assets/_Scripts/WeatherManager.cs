using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeatherManager : Singleton<WeatherManager>
{
    [SerializeField] private float duststormPct;
    [SerializeField] private float tornadoPct;
    [SerializeField] private float lightningPct;

    [SerializeField] private TextMeshProUGUI duststormTextObject;
    [SerializeField] private TextMeshProUGUI tornadoTextObject;
    [SerializeField] private TextMeshProUGUI lightningTextObject;

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
        //Debug.Log(_weatherState.GetCurrentState());
    }

    public void HandleWeatherChanged()
    {
        _weatherState.CycleWeatherState(duststormPct, tornadoPct, lightningPct);
    }

    void Start()
    {
    }

    private float GenRandomPct()
    {
        return Random.Range(0f, 100.001f);
    }

    private void RandomizeWeatherPct()
    {
        duststormPct = GenRandomPct();
        tornadoPct = GenRandomPct();
        lightningPct = GenRandomPct();
    }

    private void DisplayPercentage()
    {
        duststormTextObject.text = Mathf.Round(duststormPct).ToString();
        tornadoTextObject.text = Mathf.Round(tornadoPct).ToString();
        lightningTextObject.text = Mathf.Round(lightningPct).ToString();
    }

    public void SetPercentage()
    {
        RandomizeWeatherPct();
        DisplayPercentage();
    }
}
