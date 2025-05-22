using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostOfficeBuilding : BuildingData
{
    private int lastSol = -1;

    protected override void Update()
    {
        base.Update();
        
        if (GameManager.sol != lastSol)
        {
            if (GameManager.sol % 7 == 0)
            {
                SetBuildingEcon(20);
            }
            else
            {
                SetBuildingEcon(0);
            }
            lastSol = GameManager.sol;
        }
    }
}
