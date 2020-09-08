using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*IBeginDragHandler, IDragHandler, IEndDragHandler,*/
public class CardSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 startPos;
    private Quaternion startRot;

    public float cardHoverScaleAmt = 1.2f;
    public float cardSelectRaiseAmt = 5f;

    public static GameObject cardSelected;
    private bool wasSelected = false;

    /*
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
    */

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Pointer Enter On: " + transform.gameObject.name);
        if (!wasSelected)
            transform.localScale *= cardHoverScaleAmt;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Pointer Exit On: " + transform.gameObject.name);
        if(!wasSelected)
            transform.localScale /= cardHoverScaleAmt;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pointer Clicked On: " + transform.gameObject.name);
        if (!wasSelected)
        {
            transform.localPosition += new Vector3(0, cardSelectRaiseAmt * 10, 0);

            cardSelected = gameObject;
            wasSelected = true;
        }
    }

    private void Update()
    {
        if(cardSelected != gameObject && wasSelected)
        {
            transform.localPosition -= new Vector3(0, cardSelectRaiseAmt * 10, 0);
            transform.localScale /= cardHoverScaleAmt;
            wasSelected = false;
        }
    }
}
