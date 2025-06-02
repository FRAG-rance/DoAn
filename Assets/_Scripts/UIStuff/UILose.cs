using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILose : UICanvas
{
    [SerializeField] private TextMeshProUGUI econ;
    [SerializeField] private TextMeshProUGUI sol;

    private void Awake()
    {
        UIHighscore.AddHighscoreEntry(GameManager.sol, EconSystem.currentMaxEcon);
        econ.text = "Your highest econ acummulated: " + EconSystem.currentMaxEcon; 
        sol.text = "Survived for: " + GameManager.sol;
    }

    public void MainMenuButton()
    {
        CloseDirecly();
        GameManager.Instance.ResetGame();
        UIManager.Instance.OpenUI<UIMainMenu>();
        MiddlePanel.Instance.DisablePanel();
    }
}
