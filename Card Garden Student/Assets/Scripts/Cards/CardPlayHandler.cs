using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardPlayHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image playAreaPanel;

    private void Start()
    {
        playAreaPanel = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject selectedCard = eventData.pointerDrag;

        //If a card is dragged into the Play Area
        if (selectedCard)
        {
            selectedCard.GetComponent<CardSelector>().inPlayArea = true;

            //Set panel alpha to be visible
            var tempColor = playAreaPanel.color;
            tempColor.a = 0.35f;
            playAreaPanel.color = tempColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject selectedCard = eventData.pointerDrag;

        //If a card is dragged out of the Play Area
        if (selectedCard)
        {
            selectedCard.GetComponent<CardSelector>().inPlayArea = false;

            //Set panel alpha to be invisible
            var tempColor = playAreaPanel.color;
            tempColor.a = 0f;
            playAreaPanel.color = tempColor;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject selectedCard = eventData.pointerDrag;
        selectedCard.GetComponent<CardSelector>().canPlay = true;
        selectedCard.transform.SetParent(transform);

        //Set panel alpha to be invisible
        var tempColor = playAreaPanel.color;
        tempColor.a = 0f;
        playAreaPanel.color = tempColor;
    }
}
