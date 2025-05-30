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

    private List<int> bag = new();
    private int bagIndex;

    public void Awake()
    {
        base.Awake();
        cardDict = datas.cardData.ToDictionary(r => r.ID, r => r);
        Debug.Log(datas.cardData.Count);

        InitializeBag();
    }

    private void InitializeBag()
    {
        bag.Clear();
        for (int i = 0; i < datas.cardData.Count; i++)
        {
            bag.Add(i);
        }
        ShuffleBag();
    }

    private void ShuffleBag()
    {
        // Fisher-Yates shuffle
        for (int i = bag.Count-1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = bag[i];
            bag[i] = bag[j];
            bag[j] = temp;
        }
        bagIndex = 0;
    }

    public CardSO GetCardByID(int id)
    {
        return cardDict[id];
    }

    public CardSO GetRandomCard()
    {

        if (bagIndex >= bag.Count)
        {
            ShuffleBag();
        }
        return datas.cardData[bag[bagIndex++]];
    }

    public void SetCurrentDeckSize(int newSize)
    {
        currentDeckSize = newSize;
    }

    public int BagRNG(int size)
    {
        if (bagIndex >= bag.Count)
        {
            ShuffleBag();
        }
        return bag[bagIndex++];
    }
}
