using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardHolder cardHolder;
    public void OnPointerClick(PointerEventData eventData)
    {
        cardHolder.InstantiateCardVisual();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
