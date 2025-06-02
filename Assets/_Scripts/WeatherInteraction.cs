using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeatherInteraction : MonoBehaviour
{
    [SerializeField] private WeatherManager _weatherManager;
    [SerializeField] private WeatherEffect _weatherEffect;
    [SerializeField] private EconSystem _econSystem;

    [SerializeField] private StormEffect _stormEffect;
    [SerializeField] private LightningEffect _lightningEffect;
    [SerializeField] private TornadoEffect _tornadoEffect;
    [SerializeField] private ObjectPlacer _objectPlacer;

    [HideInInspector] public UnityEvent endWeatherEvent;

    private void OnEnable()
    {
        _weatherEffect.OnWeatherTrigger.AddListener(HandleWeatherInteraction);
        _lightningEffect.OnLightningStrike.AddListener(HandleLightningInteraction);
        _stormEffect.OnStormEvent.AddListener(HandleStormInteraction);
        _tornadoEffect.OnTornadoEvent.AddListener(HandleTornadoEvent);

        _lightningEffect.OnLightningEventFinished.AddListener(HandleEndWeatherEvent);
        _stormEffect.OnStormEventFinished.AddListener(HandleEndWeatherEvent);
        _tornadoEffect.OnTornadoEventFinished.AddListener(HandleEndWeatherEvent);
    }

    private void OnDisable()
    {
        _lightningEffect.OnLightningStrike.RemoveAllListeners();
    }

    private void HandleWeatherInteraction(WeatherState.State state)
    {
        switch(state)
        {
            case WeatherState.State.Lightning:
                _lightningEffect.ActivateLightningEffect();
                break;
            case WeatherState.State.Duststorm:
                _stormEffect.ActivateStormEffect();
                break;
            case WeatherState.State.Tornado:
                _tornadoEffect.ActivateTornadoEvent();
                break;
        }
    }

    private void HandleLightningInteraction(Vector3 gridPosition)
    {
        GameObject building = _objectPlacer.GetGameObject(gridPosition);
        List<Vector3> list = _objectPlacer.GetSurroundingGameObject(gridPosition, new Vector2Int(1, 1));
        if (!building)
        {
            return;
        }

        foreach(var obj in list)
        {
            GameObject temp = _objectPlacer.GetGameObject(obj);
            if (!temp)
            {
                continue;
            }
            if (temp.TryGetComponent<LightningRodBuilding>(out _))
            {
                Debug.Log("is rod");
                return;
            }
        }

        BuildingData buildingData = building.GetComponent<BuildingData>();
        buildingData.TakeDamage(70f + 10 * GameManager.sol);
    }

    private void HandleStormInteraction(List<Vector3> damagedBuilding)
    {
        if (damagedBuilding == null)
        {
            Debug.Log("damagedBuilding is null");
            return;
        }
        foreach (var buildingLocation in damagedBuilding) {
            GameObject currentBuilding = _objectPlacer.GetGameObject(buildingLocation);
            //Debug.Log(currentBuilding);
            if (!currentBuilding)
            {
                return;
            }
            BuildingData buildingData = currentBuilding.GetComponent<BuildingData>();
            //Debug.Log(buildingData);
            buildingData.TakeDamage(50f + 10 * GameManager.sol);
        }
    }

    private void HandleTornadoEvent(Vector3 buildingPosition)
    {
        GameObject building = _objectPlacer.GetGameObject(buildingPosition);
        if (!building)
        {
            return;
        }
        BuildingData buildingData = building.GetComponent<BuildingData>();
        buildingData.TakeDamage(60f + 10 * GameManager.sol);
    }

    public void HandleEndWeatherEvent()
    {
        endWeatherEvent?.Invoke();
    }

}
