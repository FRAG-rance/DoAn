using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;
    StructureSO database;

    public RepairState( Grid grid,
                        PreviewSystem previewSystem,
                        GridData floorData,
                        GridData furnitureData,
                        ObjectPlacer objectPlacer,
                        StructureSO database)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        this.database = database;
        previewSystem.StartShowingRemovePreview();
        this.database = database;
    }
    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }
    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !(furnitureData.CanPlaceObejctAt(gridPosition, Vector2Int.one) &&
            floorData.CanPlaceObejctAt(gridPosition, Vector2Int.one));
    }
    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
    public void OnAction(Vector3Int gridPosition)
    {
        if (EconSystem.currentEcon < 1) //hard coded
        {
            Debug.Log("dont have enough econ");
            return;
        }

        GridData selectedData = null;
        if (furnitureData.CanPlaceObejctAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = furnitureData;
        }
        else if (floorData.CanPlaceObejctAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = floorData;
        }

        if (selectedData == null)
        {
            //sound
        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            GameObject building = objectPlacer.GetGameObject(gridPosition);
            if (!building)
            {
                return;
            }
            BuildingData buildingData = building.GetComponent<BuildingData>();
            buildingData.Repair();

            EconSystem.Instance.DeductEcon(1); //hard coded aswell
            EconSystem.Instance.UpdateEconVisual();
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }
}
