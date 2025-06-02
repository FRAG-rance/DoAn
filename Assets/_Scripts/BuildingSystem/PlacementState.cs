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
    AudioClip clip;
    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          StructureSO database,
                          GridData floorData,
                          GridData furnitureData,
                          ObjectPlacer objectPlacer,
                          AudioClip clip)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        this.clip = clip;

        selectedObjectIndex = database.structureData.FindIndex(data => data.Id == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(
                database.structureData[selectedObjectIndex].Prefab,
                database.structureData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object with ID {iD}");
        this.clip = clip;
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (EconSystem.currentEcon < database.structureData[selectedObjectIndex].Cost)
        {
            Debug.Log("dont have enough econ");
            return;
        }

        if (placementValidity == false)
        {
            Debug.Log("cant");
            return;
        }
        int index = objectPlacer.PlaceObject(database.structureData[selectedObjectIndex],
        grid.CellToWorld(gridPosition), gridPosition);

        GridData selectedData = database.structureData[selectedObjectIndex].Id == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition,
            database.structureData[selectedObjectIndex].Size,
            database.structureData[selectedObjectIndex].Id,
            index);

        EconSystem.Instance.DeductEcon(database.structureData[selectedObjectIndex].Cost);
        EconSystem.Instance.UpdateEconVisual();

        //SpendEconGA spendEconGA = new SpendEconGA(database.structureData[selectedObjectIndex].Cost);
        //ActionSystem.Instance.Perform(spendEconGA);

        AudioManager.Instance.PlaySoundFXClip(clip, new Vector3(0,0,0), 1f);
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