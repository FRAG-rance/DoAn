using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainMenu : UICanvas
{
    private GameObject panel;
    private void Start()
    {
        panel = GameObject.FindGameObjectWithTag("IgnoreLayout");
        panel.SetActive(false);
    }
    public void PlayButton()
    {
        Close(0);
        panel.SetActive(true);
        //UIManager.Instance.OpenUI<UIGameplay>();
    } 

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }

    public void HighScoreButton()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
