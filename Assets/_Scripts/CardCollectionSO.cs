using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardCollectionSO : ScriptableObject
{
    public List<CardSO> cardData;
}
[Serializable]
public class CardSO
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
}
