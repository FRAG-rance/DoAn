using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler
{
    private Canvas canvas;
    [SerializeField] private StructureSO database;
    public UnityEvent<Deck, int> PointerClickEvent;

    private List<Card> deck = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        int ID = GenRandomID();
        Debug.Log(ID);
        PointerClickEvent.Invoke(this, ID);
    }

    public int GenRandomID()
    {
        return Random.Range(0, 3);
    }
}
