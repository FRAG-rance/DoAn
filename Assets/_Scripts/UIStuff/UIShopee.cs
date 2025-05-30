using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopee : UICanvas
{
    public void CloseShop()
    {
        UIManager.Instance.CloseUI<UIShop>(0);
    }

    public void LightningRod()
    {
        Close(0);
        PlacementSystem.Instance.StartPlacement(6);
    }

    public void StormBlocker()
    {
        Close(0);
        PlacementSystem.Instance.StartPlacement(7);
    }

}
