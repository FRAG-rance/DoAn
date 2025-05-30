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

    private void Start()
    {
        emptyTemplate.gameObject.SetActive(false);
        float templateHeight = 100f;

        for(int i = 0; i < 3; i++)
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
            int score = Random.Range(0, 9999);
            tempEntry.Find("DaysText").GetComponent<TextMeshProUGUI>().text = score.ToString();
            tempEntry.Find("MaxEconText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        }
    }   
}
