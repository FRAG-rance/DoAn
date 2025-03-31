using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
       
    [SerializeField] private bool isDragging;
    [SerializeField] private bool isHovering;

    //[SerializeField] private PlacementSystem placementSystem;

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
        Vector2 localPoint;
        if(transform.position.x > 400 && transform.position.y > 300)
        {
            Destroy(this.gameObject);
            //placementSystem.StartPlacement(1);
        }
        
        ClampPosition();
        if (isDragging)
        {
            //Vector2 targetPosition = Input.mousePosition - offset;
            //Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            //Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            //transform.Translate(velocity * Time.deltaTime);
            //transform.position = targetPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform) canvas.transform, Input.mousePosition, canvas.worldCamera, out localPoint);
            transform.position = canvas.transform.TransformPoint(localPoint);
            //Debug.Log(transform.position);
        }
    }

    void ClampPosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        /*Debug.Log(screenBounds);
        Debug.Log(clampedPosition);*/
        //transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        isHovering = false;
    }

    public void OnPointerUp(PointerEventData eventData) { }

    public void OnPointerDown(PointerEventData pointerEventData) { }
}
