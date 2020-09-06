using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Quaternion startRot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Store the starting Position and Rotation of the card
        startPos = transform.localPosition;
        startRot = transform.localRotation;

        //Set rotation to 0 so card is upright
        transform.rotation = Quaternion.identity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Follow mouse position while dragging
        transform.position = eventData.position;//Input.mousePosition;     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Invalid Placement - Move card back to starting position
        transform.localPosition = startPos;
        transform.localRotation = startRot;
    }
}
