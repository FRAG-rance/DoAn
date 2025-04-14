using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
       
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isHovering;
    //[SerializeField] private PlacementSystem placementSystem;

    [Header("Movement")]
    [SerializeField] private float moveSpeedLimit = 50;

    [Header("Selection")]
    public bool selected;

    [Header("Events")]
    [HideInInspector] public UnityEvent<Card> PointerEnterEvent;
    [HideInInspector] public UnityEvent<Card> PointerExitEvent;
    [HideInInspector] public UnityEvent<Card, bool> PointerUpEvent;
    [HideInInspector] public UnityEvent<Card> PointerDownEvent;
    [HideInInspector] public UnityEvent<Card> BeginDragEvent;
    [HideInInspector] public UnityEvent<Card> EndDragEvent;
    [HideInInspector] public UnityEvent<Card, bool> SelectEvent;

    [SerializeField] private StructureSO data;

    private Vector3 offset;
    private Canvas canvas;
    
    public Card()
    {
        //this.cardData = data.structureData[0];
    }
    
    public void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void Update()
    {
        
        ClampPosition();
        if (isDragging)
        {
            Vector2 targetPosition = Input.mousePosition;
            transform.position = targetPosition;
        }
    }

    void ClampPosition()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        BeginDragEvent?.Invoke(this);
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragEvent?.Invoke(this);
        isDragging = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterEvent.Invoke(this);
        isHovering = true;

        
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        PointerExitEvent.Invoke(this);
        isHovering = false;

        
    }

    public void OnPointerUp(PointerEventData eventData) { }

    public void OnPointerDown(PointerEventData pointerEventData) { }
}
