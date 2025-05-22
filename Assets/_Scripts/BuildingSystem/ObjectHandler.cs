using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class ObjectPlacer : Singleton<ObjectPlacer>
{
    public Dictionary<List<Vector3>,GameObject> placedGameObjects = new();

    public int PlaceObject(StructureData currentData, Vector3 position, Vector3Int gridPosition)
    {
        GameObject newObject = Instantiate(currentData.Prefab);
        newObject.transform.GetChild(1).gameObject.SetActive(true);
        newObject.transform.position = position; 
        if (newObject.TryGetComponent(out BuildingData building))
        {
            building.Initialize(currentData);
        }

        // Add collider for the object
        AddObjectCollider(newObject, currentData.Size);

        placedGameObjects.Add(CalculatePositions(gridPosition, currentData.Size), newObject);
        
        return placedGameObjects.Count - 1;
    }

    private void AddObjectCollider(GameObject obj, Vector2Int size)
    {
        // Add a box collider that matches the grid size
        BoxCollider collider = obj.AddComponent<BoxCollider>();
        collider.size = new Vector3(size.x, 1f, size.y);
        collider.center = new Vector3(size.x * 0.5f - 0.5f, 0.5f, size.y * 0.5f - 0.5f);
        
        // Make it a trigger if you don't want physical collisions
        collider.isTrigger = true;
    }

    private List<Vector3> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3(x, 0, y));
            }
        }
        return returnVal;
    }

    private List<Vector3> GetKey(Vector3 gridPosition)
    {
        foreach (var key in placedGameObjects.Keys)
        {
            if (key.Contains(gridPosition))
            {
                return key;
            }
        }
        return null; // Not found
    }

    internal void RemoveObjectAt(Vector3 gridPosition)
    {
        if (!placedGameObjects.ContainsKey(GetKey(gridPosition)))
            return;

        Destroy(placedGameObjects[GetKey(gridPosition)]);
        placedGameObjects.Remove(GetKey(gridPosition));
    }

    public GameObject GetGameObject(Vector3 gridPosition)
    {
        if (GetKey(gridPosition) == null)
        {
            return null;
        }
        return placedGameObjects[GetKey(gridPosition)];
    }

    public List<Vector3> GetSurroundingGameObject(Vector3 gridPosition, Vector2Int objectSize)
    {
        List<Vector3> neighborPos = new List<Vector3>();

        int x = (int)gridPosition.x;
        int y = (int)gridPosition.y;
        int z = (int)gridPosition.z;

        // Calculate the boundaries of the object
        int startX = x - 1;
        int endX = x + objectSize.x;
        int startZ = z - 1;
        int endZ = z + objectSize.y;

        // Check all surrounding positions
        for (int checkX = startX; checkX <= endX; checkX++)
        {
            for (int checkZ = startZ; checkZ <= endZ; checkZ++)
            {
                // Skip positions that are part of the object itself
                if (checkX >= x && checkX < x + objectSize.x &&
                    checkZ >= z && checkZ < z + objectSize.y)
                {
                    continue;
                }

                // Check bounds (assuming a 50x50 grid centered at origin)
                if (checkX >= -25 && checkX < 25 && checkZ >= -25 && checkZ < 25)
                {
                    neighborPos.Add(new Vector3(checkX, y, checkZ));
                }
            }
        }
        
        return neighborPos;
    }

    public List<GameObject> GetPlacedGameObject()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var (key, value) in placedGameObjects)
        {
            list.Add(value);
        }
        return list;
    }

    public List<GameObject> GetPlacedGameObjectOfType<T>() where T : BuildingData
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var (key, value) in placedGameObjects)
        {
            if (value.TryGetComponent<T>(out _))
            {
                list.Add(value);
            }
        }
        return list;
    }
}