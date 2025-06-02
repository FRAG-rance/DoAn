using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlacementSystem : Singleton<PlacementSystem>
{
    [SerializeField] private AudioClip destroyClip;
    [SerializeField] private AudioClip buildClip;
    [SerializeField] private AudioClip repairClip;

    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private StructureSO database;

    [SerializeField]
    private GameObject gridVisualization;

    public static GridData floorData, furnitureData;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    public bool firstBuilding = true;
    public UnityEvent firstBuildingEvent;

    IBuildingState buildingState;

    private void Start()
    {
        gridVisualization.SetActive(false);
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID)
    {   StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           database,
                                           floorData,
                                           furnitureData,
                                           objectPlacer,
                                           buildClip);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer, destroyClip);
        inputManager.OnClicked += PlaceStructure;
        //inputManager.OnExit += StopPlacement;
    }

    public void StartRepair()
    {
        StopPlacement();
        gridVisualization.SetActive(true);  
        buildingState = new RepairState(grid,preview, floorData, furnitureData, objectPlacer, database, repairClip);
        inputManager.OnClicked += PlaceStructure;
        //inputManager.OnExit += StopPlacement;
    }


    private void PlaceStructure()
    {
        //dont touch this
        /*if (inputManager.IsPointerOverUI())
        {
            return;
        }*/

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        buildingState.OnAction(gridPosition);

        if (firstBuilding)
        {
            firstBuildingEvent?.Invoke();
            firstBuilding = false;
        }

        //Debug.Log(gridPosition);
        StopPlacement();
    }

    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    public GridData GetFurnitureData()
    {
        return furnitureData;
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartPlacement(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartPlacement(6);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartPlacement(7);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartRemoving();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartRepair();
        }



        if (buildingState == null)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }

    }
}