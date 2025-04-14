using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


public class CardHolder : MonoBehaviour
{
    [SerializeField] private Card selectedCard;
    [SerializeReference] private Card hoveredCard;

    [SerializeField] private List<GameObject> cardPrefab;

    [SerializeField] private PlacementSystem placementSystem; 
    [SerializeField] private CardCollectionSO cards;

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
        InstantiateCardVisual();
    }
    public void InstantiateCardVisual()
    {
        Instantiate(cardPrefab[Random.Range(0,cardPrefab.Count)], transform);

        card = GetComponentInChildren<Card>();

        card.PointerEnterEvent.AddListener(CardPointerEnter);
        card.PointerExitEvent.AddListener(CardPointerExit);
        card.BeginDragEvent.AddListener(BeginDrag);
        card.EndDragEvent.AddListener(EndDrag);
    }

    public void CardPointerEnter(Card card) {
        hoveredCard = card;

    }

    public void CardPointerExit(Card card) {
        hoveredCard = null;
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
            Debug.Log(selectedCard.name);
            int currentCardIndex;
            if(selectedCard.name == "Card 1(Clone)")
            {
                placementSystem.StartPlacement(1);
            } else if (selectedCard.name == "Card 2(Clone)")
            {
                placementSystem.StartPlacement(2);
            }
            else if (selectedCard.name == "Card 3(Clone)")
            {
                placementSystem.StartPlacement(3);
            }

            //int currentCardIndex = cards.cardData.FindIndex(cardData => cardData.Name == selectedCard.name);
            //Debug.Log(currentCardIndex);
            Destroy(selectedCard.gameObject);
            selectedCard = null;
        }     
    }

    
}
