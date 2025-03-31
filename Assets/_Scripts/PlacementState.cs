using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = 1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    StructureSO database;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          StructureSO database,
                          GridData floorData,
                          GridData furnitureData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = database.structureData.FindIndex(data => data.Id == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.structureData[selectedObjectIndex].Prefab,
                database.structureData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object with ID {iD}");

    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            Debug.Log("cant");
            return;
        }
        int index = objectPlacer.PlaceObject(database.structureData[selectedObjectIndex].Prefab,
        grid.CellToWorld(gridPosition), gridPosition);

        GridData selectedData = database.structureData[selectedObjectIndex].Id == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.structureData[selectedObjectIndex].Size,
            database.structureData[selectedObjectIndex].Id,
            index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = database.structureData[selectedObjectIndex].Id == 0 ?
            floorData :
            furnitureData;

        return selectedData.CanPlaceObejctAt(gridPosition, database.structureData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}