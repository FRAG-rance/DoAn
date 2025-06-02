using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePanel : Singleton<MiddlePanel>
{
    public void EnablePanel()
    {
        transform.gameObject.SetActive(true);
    }
    public void DisablePanel()
    {
        transform.gameObject.SetActive(false);
    }
}
