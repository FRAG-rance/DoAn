using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class MenuEventHandler : MonoBehaviour
{
    [Header("References")] 
    public List<Selectable> selectables = new List<Selectable>();

    [Header("Animations")]
    [SerializeField] protected float selectedAnimationScale = 1.2f;
    [SerializeField] protected float scaleDuration = 0.25f;
     
    protected Dictionary<Selectable, Vector3> scales = new Dictionary<Selectable, Vector3>();

    protected Tween scaleUpTween;
    protected Tween scaleDownTween;

    public virtual void Awake()
    {
        foreach (var selectable in selectables)
        {
            AddSelectionListeners(selectable);
            scales.Add(selectable, selectable.transform.localScale);
        }
    }

    public virtual void OnEnable()
    {
        for (int i = 0; i < selectables.Count; i++)
        {
            selectables[i].transform.localScale = scales[selectables[i]];
        }
    }

    public virtual void OnDisable()
    {
        scaleUpTween.Kill(true);
        scaleDownTween.Kill(true);  
    }

    protected virtual void AddSelectionListeners(Selectable selectable)
    {
        //add listener
        EventTrigger trigger = selectable.gameObject.AddComponent<EventTrigger>();
        if (trigger != null)
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }

        //add SELECT event
        EventTrigger.Entry SelectEntry = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.Select
        };
        SelectEntry.callback.AddListener(OnSelect);

        //add DESELECT event
        EventTrigger.Entry DeSelectEntry = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.Deselect
        };
        SelectEntry.callback.AddListener(OnDeselect);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Vector3 newScale = eventData.selectedObject.transform.localScale * selectedAnimationScale; 
        scaleUpTween = eventData.selectedObject.transform.DOScale(newScale, scaleDuration);
        Debug.Log("ónelect");
    }

    public void OnDeselect(BaseEventData eventData) {
        Selectable sel = eventData.selectedObject.GetComponent<Selectable>();
        scaleUpTween = eventData.selectedObject.transform.DOScale(scales[sel], scaleDuration);
        Debug.Log("ónDeelect");

    }

}
