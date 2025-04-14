using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public Dictionary<Vector3 ,GameObject> placedGameObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 position, Vector3Int gridPosition)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.GetChild(1).gameObject.SetActive(true);
        newObject.transform.position = position;
        placedGameObjects.Add(gridPosition, newObject);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(Vector3 gridPosition)
    {
        if (!placedGameObjects.ContainsKey(gridPosition))
            return;

        Destroy(placedGameObjects[gridPosition]);
        placedGameObjects.Remove(gridPosition);
    }

    public GameObject GetGameObject(Vector3 gridPosition)
    {
        if (!placedGameObjects.ContainsKey(gridPosition))
            return null;

        return placedGameObjects[gridPosition];
    }

    public List<Vector3> GetAllBuildingLocation()
    {
        List<Vector3> list = new List<Vector3>();
        foreach(var (key, value) in placedGameObjects)
        {
            list.Add(key);
        }
        return list;
    }
}