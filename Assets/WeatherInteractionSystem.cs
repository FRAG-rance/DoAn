using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherInteractionSystem : MonoBehaviour
{
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private WeatherManager _weatherManager;
    
    [SerializeField] private LightningEffect _lightningEffect;
    [SerializeField] private ObjectPlacer _objectPlacer;
    //[SerializeField] private BuildingData _buildingData;

    private void OnEnable()
    {
        _lightningEffect.OnLightningStrike.AddListener(GetHandleLightningStrike);
    }

    private void OnDisable()
    {
        _lightningEffect.OnLightningStrike.RemoveAllListeners();
    }

    private void GetHandleLightningStrike(Vector3 gridPosition)
    {
        GameObject building = _objectPlacer.GetGameObject(gridPosition);
        if (!building)
        {
            return;
        }

        BuildingData buildingData = building.GetComponent<BuildingData>();
        buildingData.TakeDamage(20f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
