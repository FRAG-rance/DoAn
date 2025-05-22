using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UIShop : UICanvas
{
    public void CloseShop()
    {
        UIManager.Instance.CloseUI<UIShop>(0);
    }

    public void UpgradeHouse()
    {
        if (EconSystem.currentEcon < 10)
            return;

        List<GameObject> houses = ObjectPlacer.Instance.GetPlacedGameObjectOfType<HouseBuilding>();
        foreach(var house in houses)
        {
            BuildingData buildingData = house.GetComponent<BuildingData>();
            buildingData.Upgrade(50);
        }

        EconSystem.Instance.DeductEcon(10);
        EconSystem.Instance.UpdateEconVisual();

    }
    public void UpgradeFarm()
    {
        if (EconSystem.currentEcon < 10)
            return;

        List<GameObject> farms = ObjectPlacer.Instance.GetPlacedGameObjectOfType<FarmBuilding>();
        foreach (var farm in farms)
        {
            BuildingData buildingData = farm.GetComponent<BuildingData>();
            buildingData.Upgrade(50);
        }

        EconSystem.Instance.DeductEcon(10);
        EconSystem.Instance.UpdateEconVisual();

    }
    public void UpgradeFactory()
    {
        if (EconSystem.currentEcon < 20)
            return;

        List<GameObject> factories = ObjectPlacer.Instance.GetPlacedGameObjectOfType<FactoryBuilding>();
        foreach (var factory in factories)
        {
            BuildingData buildingData = factory.GetComponent<BuildingData>();
            buildingData.Upgrade(50);
        }
        EconSystem.Instance.DeductEcon(20);
        EconSystem.Instance.UpdateEconVisual();

    }
    public void UpgradePost()
    {
        if (EconSystem.currentEcon < 20)
            return;

        List<GameObject> offices = ObjectPlacer.Instance.GetPlacedGameObjectOfType<PostOfficeBuilding>();
        foreach (var office in offices)
        {
            BuildingData buildingData = office.GetComponent<BuildingData>();
            buildingData.Upgrade(50);
        }
        EconSystem.Instance.DeductEcon(20);
        EconSystem.Instance.UpdateEconVisual();

    }
    public void UpgradeTower()
    {
        if (EconSystem.currentEcon < 10)
            return;
        EconSystem.Instance.DeductEcon(10);

        List<GameObject> towers = ObjectPlacer.Instance.GetPlacedGameObjectOfType<TowerBuilding>();
        foreach (var tower in towers)
        {
            BuildingData buildingData = tower.GetComponent<BuildingData>();
            buildingData.Upgrade(50);
        }
        EconSystem.Instance.DeductEcon(10);
        EconSystem.Instance.UpdateEconVisual();
    }
}
