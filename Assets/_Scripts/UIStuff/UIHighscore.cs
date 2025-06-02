using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIHighscore : UICanvas
{
    [SerializeField] private Transform emptyContainer;
    [SerializeField] private Transform emptyTemplate;
    
    

    public void BackButton()
    {
        Close(0f);
    }

    private void OnEnable()
    {
        emptyTemplate.gameObject.SetActive(false);
        float templateHeight = 100f;

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].sol > highscores.highscoreEntryList[i].sol)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        Debug.Log(jsonString.ToString());

        for (int i = 0; i < 3; i++)
        {
            var tempEntry = Instantiate(emptyTemplate, emptyContainer);
            RectTransform tempEntryTransform = tempEntry.GetComponent<RectTransform>();
            tempEntryTransform.anchoredPosition = new Vector3(0, -i * templateHeight, 0);
            tempEntryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch(rank)
            {
                default:
                    rankString = rank + "TH";
                    break;
                case 1:
                    rankString = "1ST";
                    break;
                case 2:
                    rankString = "2ND";
                    break;
                case 3:
                    rankString = "3RD";
                    break;
            }

            tempEntry.Find("NoText").GetComponent<TextMeshProUGUI>().text = rankString;
            int sol = highscores.highscoreEntryList[i].sol;
            tempEntry.Find("DaysText").GetComponent<TextMeshProUGUI>().text = sol.ToString();
            int econ = highscores.highscoreEntryList[i].econ;
            tempEntry.Find("MaxEconText").GetComponent<TextMeshProUGUI>().text = econ.ToString();
        }
    }

    public static void AddHighscoreEntry(int sol, int econ)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { sol = sol, econ = econ};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Check for duplicate entries
        bool isDuplicate = false;
        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
        {
            if (entry.sol == sol && entry.econ == econ)
            {
                isDuplicate = true;
                break;
            }
        }

        // Only add if not a duplicate
        if (!isDuplicate)
        {
            highscores.highscoreEntryList.Add(highscoreEntry);
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int sol;
        public int econ;
    }
}

