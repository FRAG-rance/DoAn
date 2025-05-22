using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilding : BuildingData
{
    private List<BuildingData> affectedBuildings = new List<BuildingData>();
    private Vector2Int size = new Vector2Int(1, 1);
    private int surroundingBuildingCount = 0;

    private void Start()
    {
        UpdateTowerEcon();
    }

    protected override void Update()
    {
        base.Update();
    }

    [ContextMenu("test")]
    private void UpdateTowerEcon()
    {
        List<Vector3> surroudingBuildings = ObjectPlacer.Instance.GetSurroundingGameObject(currentPosition, size);

        foreach (var buildingLocation in surroudingBuildings)
        {
            GameObject currentBuilding = ObjectPlacer.Instance.GetGameObject(buildingLocation);
            if (!currentBuilding)
            {
                continue;
            }
            surroundingBuildingCount++;
        }
        SetBuildingEcon(surroundingBuildingCount * 1);
    }

    protected override void Kill()
    {        
        base.Kill();
    }
}