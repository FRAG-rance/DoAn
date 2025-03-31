using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CardHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefab;
    private RectTransform rect;
    private Card card;
    [SerializeField] private Deck deck;
    [SerializeField] private CardCollectionSO cards;

    // Start is called before the first frame update
    void Start()
    {
        GenNewCard();
        rect = GetComponent<RectTransform>();
        card = GetComponentInChildren<Card>();

        deck.PointerClickEvent.AddListener(GenNewCard);
        card.PointerEnterEvent.AddListener(CardPointerEnter);
        card.PointerExitEvent.AddListener(CardPointerExit);
        card.BeginDragEvent.AddListener(BeginDrag);
        card.EndDragEvent.AddListener(EndDrag);
    }
    public void GenNewCard() 
    {
        Instantiate(cardPrefab[2], transform);
    }
    public void GenNewCard(Deck deck, int ID)
    {
        Instantiate(cardPrefab[ID], transform);
    }
    public void CardPointerEnter(Card card) { 
    
    }

    public void CardPointerExit(Card card) { 
    }

    public void BeginDrag(Card card) { 
    }

    public void EndDrag(Card card) { 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
