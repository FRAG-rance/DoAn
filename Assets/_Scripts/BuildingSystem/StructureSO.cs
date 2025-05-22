using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StructureSO : ScriptableObject
{
    public List<StructureData> structureData;
}

[Serializable]
public class StructureData
{
    [field: SerializeField]
    public String Name { get; private set; }
    [field: SerializeField]
    public int Id { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public float Health { get; private set; } = 100f;
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    public int Econ { get; private set; }
    
}
