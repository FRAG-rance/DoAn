using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    public static int baseDeckSize;
    public static int currentDeckSize;
    public CardCollectionSO datas;
    public Dictionary<int, CardSO> cardDict;

    public void Awake()
    {
        base.Awake();
        cardDict = datas.cardData.ToDictionary(r => r.ID, r => r);
    }

    public CardSO GetCardByID(int id)
    {
        return cardDict[id];
    }

    public CardSO GetRandomCard()
    {
        return datas.cardData[Random.Range(0, datas.cardData.Count)];
    }

    public void SetCurrentDeckSize(int newSize)
    {
        currentDeckSize = newSize;
    }
}
