using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FarmBuilding : BuildingData
{
    protected override void Update()
    {
        base.Update();
        UpdateEcon();

    }
    [ContextMenu("test")]
    public void UpdateEcon()
    {
        float finalEcon = initEcon;
        
        Vector2Int size = new Vector2Int(1, 1);
        List<Vector3> surroundingPositions = ObjectPlacer.Instance.GetSurroundingGameObject(currentPosition, size);
        
        foreach (var position in surroundingPositions)
        {
            GameObject adjacentBuilding = ObjectPlacer.Instance.GetGameObject(position);
            if (adjacentBuilding != null && adjacentBuilding.TryGetComponent<FarmBuilding>(out _))
            {
                finalEcon *= 2f;
                break; 
            }
        }
        
        SetBuildingEcon(Mathf.RoundToInt(finalEcon));
    }
}
