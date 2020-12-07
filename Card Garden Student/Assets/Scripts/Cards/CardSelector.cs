using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*IBeginDragHandler, IDragHandler, IEndDragHandler*/
/*IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler*/
public class CardSelector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float cardHoverScaleAmt = 1.2f;
    public float cardSelectRaiseAmt = 5f;
    public bool inPlayArea = false;
    public bool canPlay = false;
    public bool isScaledUp = false;
    public int cardInHandIndex;

    public static GameObject cardSelected;
    public static GameObject cardHovered;

    private Transform startTransform;
    private float cardMoveSpeed;


    private void Start()
    {
        startTransform = transform;
        cardMoveSpeed = CardHandController.Instance.cardMoveSpeed;

        //Update card indexes
        CardHandController.Instance.UpdateCardIndexes();
    }

    private void Update()
    {
        // Handle Card Position & Rotation while NOT selected
        if (cardSelected != gameObject)
        {
            float t = (cardInHandIndex + 0.5f) / transform.parent.childCount;
  
            Vector3 cardPos = CardHandController.Instance.GetCurvePoint(t);
            cardPos = cardPos + (cardHovered == gameObject ? transform.up * 50f : Vector3.zero);
            cardPos = Vector3.MoveTowards(transform.position, cardPos, cardMoveSpeed * 100 * Time.deltaTime);
            transform.position = cardPos;

            Vector3 cardUp = CardHandController.Instance.GetCurveNormal(t);
            Vector3 cardForward = Vector3.forward;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(cardForward, cardUp), cardMoveSpeed * 10 * Time.deltaTime);
        }

        // Handle Card Position & Rotation while dropped in the Play Area
        if (canPlay)
        {
            Vector3 playPos = transform.parent.position + new Vector3(500f, 0f, 0f);
            playPos = Vector3.MoveTowards(transform.position, playPos, cardMoveSpeed * 100 * Time.deltaTime);
            transform.position = playPos;
        }
    }


    //Scale up a card when hovered over
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Check that this card is NOT hovered over, no cards are selected, and the game is NOT paused
        if (cardHovered != gameObject && cardSelected != gameObject && !MenuUI.Instance.isPaused)
        {
            cardHovered = gameObject;

            if(!isScaledUp)
            {
                transform.localScale *= cardHoverScaleAmt;
                isScaledUp = true;
            }

            GetComponent<Canvas>().sortingOrder = 99;
        }
    }

    //Scale down a card when done hovering over
    public void OnPointerExit(PointerEventData eventData)
    {
        //Check that this card IS hovered over, no cards are selected, and the game is NOT paused
        if (cardHovered == gameObject && cardSelected != gameObject && !MenuUI.Instance.isPaused)
        {
            cardHovered = null;

            if(isScaledUp)
            {
                transform.localScale /= cardHoverScaleAmt;
                isScaledUp = false;
            }

            //Update card indexes
            //GetComponent<Canvas>().sortingOrder = cardInHandIndex;
            CardHandController.Instance.UpdateCardIndexes();
        }
    }


    public void ResetTransform()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
        transform.localScale = startTransform.localScale;
    }


    // -------------CARD DRAGGING SYSTEM-------------
    public void OnBeginDrag(PointerEventData eventData)
    {
        canPlay = false;

        //Check for a previously selected card that may be in the play area
        if (cardSelected && cardSelected != gameObject)
        {
            //Put the previously selected card back in the hand
            cardSelected.transform.SetParent(transform.parent);

            //Update card indexes
            CardHandController.Instance.UpdateCardIndexes();

            //Reset the previously selected card's Scaling and bools
            if(cardSelected.GetComponent<CardSelector>().isScaledUp)
            {
                cardSelected.transform.localScale /= cardHoverScaleAmt;
                cardSelected.GetComponent<CardSelector>().isScaledUp = false;
            }
            cardSelected.GetComponent<CardSelector>().inPlayArea = false;
            cardSelected.GetComponent<CardSelector>().canPlay = false;
        }

        //Set this as the currently selected card
        cardSelected = gameObject;
        transform.GetComponent<Canvas>().sortingOrder = 99;

        //Set rotation to 0 so card is upright
        transform.rotation = Quaternion.identity;

        //Remove the card from the Hand
        transform.SetParent(transform.parent.parent);

        //Update card indexes
        CardHandController.Instance.UpdateCardIndexes();

        //Prevent card from blocking raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Follow mouse position while dragging in the non-playable area
        transform.position = eventData.position;//Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!inPlayArea) //Place this card back in the Hand
        {
            //De-select this card
            cardSelected = null;
            canPlay = false;

            //Put the card back in the hand
            transform.SetParent(transform.parent.GetChild(0));

            //Update card indexes
            CardHandController.Instance.UpdateCardIndexes();

            //Reset the scale of the card
            if (isScaledUp)
            {
                transform.localScale /= cardHoverScaleAmt;
                isScaledUp = false;
            }
        }

        //Allow card to block raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    /**/
}
