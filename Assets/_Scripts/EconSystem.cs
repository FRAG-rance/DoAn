using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconSystem : Singleton<EconSystem>
{
    public static int currentEcon;
    public ObjectPlacer _objectPlacer;
    [SerializeField] private TextMeshProUGUI econDisplay;


    /// <summary>
    /// w.i.p action system
    /// </summary>
    /*private int currentEconTemp = 36;
    [SerializeField] private TextMeshProUGUI econDisplayTemp;


    private void OnEnable()
    {
        ActionSystem.AttackPerformer<SpendEconGA>(SpendEconPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendEconGA>();

    }

    public bool HasEnoughEcon(int econ)
    {
        return currentEconTemp < econ;
    }

    private IEnumerator SpendEconPerformer(SpendEconGA spendEconGA)
    {
        currentEconTemp -= spendEconGA.Amount;
        econDisplayTemp.text = currentEconTemp.ToString();
        Debug.Log("current ECON is: " + currentEconTemp);
        yield return null;
    }*/


    /// <summary>
    /// works 
    /// </summary>

    public void DeductEcon(int amount)
    {
        currentEcon -= amount;
    }

    private float CalculateRoundEcon()
    {
        List<GameObject> buildingGameObjects = _objectPlacer.GetPlacedGameObject();
        foreach(var currentBuilding in buildingGameObjects)
        {
            BuildingData buildingData = currentBuilding.GetComponent<BuildingData>();
            currentEcon += buildingData.GetBuildingEcon();
        }
        return currentEcon;
    }

    public void UpdateEconVisual()
    {
        econDisplay.text = currentEcon.ToString();
    }

    public void HandleUpdateEcon()
    {
        CalculateRoundEcon();
        UpdateEconVisual();
    }

}
