using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip clip; 
    [SerializeField] private CardHolder cardHolder;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(CardSystem.currentDeckSize > 0 )
        {
            cardHolder.InstantiateCard();
            AudioManager.Instance.PlaySoundFXClip(clip, transform, 1f);
            CardSystem.currentDeckSize--;
        }
    }
}
