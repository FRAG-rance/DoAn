using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private ImageAnimation timeDisplay;
    [SerializeField] private TextMeshProUGUI dayDisplay;
    [SerializeField] private WeatherInteraction _weatherInteraction;

    [Header("GameStats")]
    public static int sol;

    IGameplayState gameplayState;
    public UnityEvent OnGameplayStateChanged;

    private void Awake()
    {
        base.Awake();
        PlacementSystem.Instance.firstBuildingEvent.AddListener(CyclePhase);
    }

    private void OnEnable()
    {
        _weatherInteraction.endWeatherEvent.AddListener(CyclePhase);
    }

    private void Start()
    {
        UIManager.Instance.OpenUI<UIMainMenu>();
        InitializeValues();
    }

    public void CyclePhase()
    {
        if (gameplayState is PrepStage)
        {
            HandleWeatherStage();
        } else if(gameplayState is WeatherStage)
        {
            HandlePrepStage();
        }
        TriggerCurrentStage();
    }

    public void HandleWeatherStage()
    {
        GameStateExit();
        StopAllCoroutines();
        gameplayState = new WeatherStage(timeDisplay);
    }

    public void HandlePrepStage()
    {
        GameStateExit();
        StopAllCoroutines();
        gameplayState = new PrepStage(sol, timeDisplay, dayDisplay);
        StartCoroutine(Helpers.WaitForRealtime(CyclePhase, 23));
    }

    public void TriggerCurrentStage()
    {
        gameplayState.Execute();
    }

    private void GameStateExit()
    { 
        gameplayState.Exit();
    }

    private void InitializeValues()
    {
        EconSystem.currentEcon = 1000;
        sol = 0;
        CardSystem.baseDeckSize = 5;
        CardSystem.currentDeckSize = CardSystem.baseDeckSize;
        gameplayState = new WeatherStage(timeDisplay);
    }
}
