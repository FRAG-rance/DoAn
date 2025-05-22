using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Unity.PlasticSCM.Editor.WebApi;


public class CardHolder : MonoBehaviour
{
    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;
    private CardSO currentCard;

    [SerializeField] private CardSystem cardSystem;

    private RectTransform rect;
    private RectTransform canvas;
    private Image image;
    private Card card;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<RectTransform>();
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.raycastTarget = false;
    }
    public void InstantiateCard()
    {
        //fix this into card system to track current card
        currentCard = cardSystem.GetRandomCard();
        Instantiate(currentCard.Prefab, transform);

        card = GetComponentInChildren<Card>();

        card.PointerEnterEvent.AddListener(CardPointerEnter);
        card.PointerExitEvent.AddListener(CardPointerExit);
        card.BeginDragEvent.AddListener(BeginDrag);
        card.EndDragEvent.AddListener(EndDrag);
    }

    public void InstantiateCardVisual()
    {

    }

    public void CardPointerEnter(Card card) {
        hoveredCard = card;

        transform.DOScale(1.5f, .15f).SetEase(Ease.OutBack);
        DOTween.Kill(2, true);
        transform.DOPunchRotation(Vector3.forward * 5, .15f, 20, 1).SetId(2);
    }

    public void CardPointerExit(Card card) {
        hoveredCard = null;

        transform.DOScale(1, .15f).SetEase(Ease.OutBack);
    }

    public void BeginDrag(Card card) {
        selectedCard = card;   
    }

    public void EndDrag(Card card) {
        if (selectedCard == null)
            return;

        selectedCard.transform.DOLocalMove(Vector3.zero, .15f).SetEase(Ease.OutBack);
        selectedCard = null;
    }


    public bool IsUIElementInLowerThird(RectTransform uiElement, RectTransform gameplayScreen)
    {
        // Convert to screen space
        Canvas canvas = gameplayScreen.GetComponentInParent<Canvas>();
        Vector3[] screenCorners = new Vector3[4];
        gameplayScreen.GetWorldCorners(screenCorners);

        // Calculate lower third Y position in screen space
        float lowerThirdY = screenCorners[0].y + (screenCorners[1].y - screenCorners[0].y) * (1 / 3f);

        // Get UI element corners
        Vector3[] uiCorners = new Vector3[4];
        uiElement.GetWorldCorners(uiCorners);

        // Check if the bottom of UI element is below the lower third line
        return (uiCorners[0].y < lowerThirdY);
    }
    // Update is called once per frame
    void Update()
    { 
        if (selectedCard == null)
            return;
        
        RectTransform cardRect = selectedCard.GetComponent<RectTransform>();
        if (!IsUIElementInLowerThird(cardRect, canvas))
        {
            PlacementSystem.Instance.StartPlacement(currentCard.ID);
            //int currentCardIndex = cards.cardData.FindIndex(cardData => cardData.Name == selectedCard.name);
            //Debug.Log(currentCardIndex);
            Destroy(selectedCard.gameObject);
            currentCard = null; 
            selectedCard = null;
        }     
    }

    
}
