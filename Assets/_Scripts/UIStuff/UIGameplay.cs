using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] private ImageAnimation timeDisplay;
    
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UIIngameOption>();
        MiddlePanel.Instance.DisablePanel();
    }

    public void EndTurnButton()
    {
        timeDisplay.Stop();
        GameManager.Instance.CyclePhase();
    }

    public void ShopButton()
    {
        UIManager.Instance.OpenUI<UIShop>();
        //UIManager.Instance.OpenUI<UISetting>();
    }

    public void ShopeeButton()
    {
        UIManager.Instance.OpenUI<UIShopee>();
    }

    public void GuideButton()
    {
        UIManager.Instance.OpenUI<UIGuide>();
    }

    public void LandscappingButton()
    {
        PlacementSystem.Instance.StartRemoving();
    }

    public void RepairButton()
    {
        PlacementSystem.Instance.StartRepair();
    }
}
