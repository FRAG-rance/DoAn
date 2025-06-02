using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMainMenu : UICanvas
{
    private void Start()
    {
        MiddlePanel.Instance.DisablePanel();
    }
    public void PlayButton()
    {
        Close(0);
        MiddlePanel.Instance.EnablePanel();
        //UIManager.Instance.OpenUI<UIGameplay>();
    }

    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }

    public void HighScoreButton()
    {
        UIManager.Instance.OpenUI<UIHighscore>();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
