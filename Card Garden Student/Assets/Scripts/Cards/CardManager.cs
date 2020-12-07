using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ObjectReferences
{
    public GameObject emptyCard;
    public Transform handZone;
    public Transform displayZone;
    public Button drawButton;
    public Button discardButton;
    public Transform deckHolder;
    public Transform discardHolder;
    public TextMeshProUGUI deckQuantityText;
    public TextMeshProUGUI cardCostText;
    public TextMeshProUGUI discardQuantityText;
}

[System.Serializable]
public class DeckQuantity
{
    public Card cardType;
    [Range(1,20)]public int cardLevel;
    public int amtInDeck;
}

public class CardManager : SingletonPattern<CardManager>
{
    //[Header("Object References")]
    public ObjectReferences objectReferences;

    //[Header("Cards & Deck Setup")]
    public int maxHandSize = 5;
    public float timeToDraw = 5f;
    public int baseCardCost = 10;
    public int costIncPerCard = 5;
    public bool drawHandOnStart;
    public DeckQuantity[] cards;

    //[HideInInspector]
    public List<GameObject> hand = new List<GameObject>();

    private int cardCost;
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> discard = new List<GameObject>();
    private List<GameObject> display = new List<GameObject>();

    private void Start()
    {
        cardCost = baseCardCost;

        GenerateDeck();

        if(drawHandOnStart)
            DrawHand();

        CardHandController.Instance.InitializeHandCurve();
    }

    private void Update()
    {
        //Set the draw button to be interactable if the player has enough gold to buy a new card and does not have the max number of cards
        objectReferences.drawButton.interactable = (PlayerStats.Instance.PlayerGold >= cardCost && hand.Count < maxHandSize) ? true : false;

        //Set the discard button to be interactable if the player has selected a card
        objectReferences.discardButton.interactable = (CardSelector.cardSelected) ? true : false;

        //Set the card cost text color to be red if the player cannot afford to purchase a card
        objectReferences.cardCostText.color = (PlayerStats.Instance.PlayerGold >= cardCost) ? Color.black : Color.red;
    }

    //Creates a new deck of cards using the cards array
    private void GenerateDeck()
    {
        //Iterate for each type of card in the deck
        foreach (DeckQuantity deckCard in cards)
        {
            //Check the cardQuantity value and spawn that many of the corresponding card type
            for (int i = 0; i < deckCard.amtInDeck; i++)
            {
                //Assign new card values
                GameObject newCard = Instantiate(objectReferences.emptyCard, Vector3.zero, Quaternion.identity, objectReferences.deckHolder);
                newCard.GetComponent<DisplayCard>().card = deckCard.cardType; //Set card type
                newCard.GetComponent<DisplayCard>().level = (deckCard.cardLevel - 1); //Set card level
                //Debug.Log(newCard.GetComponent<DisplayCard>().card.name + " set to level " + newCard.GetComponent<DisplayCard>().level);
                newCard.name = deckCard.cardType.name;

                //Disable the new card and add it to the deck
                newCard.SetActive(false);
                deck.Add(newCard);
            }
        }

        //Update the deck and discard pile quantities text
        UpdateCardText();
    }

    //Creates new cards earned between encounters, and displays them
    public void GenerateNewCards(DeckQuantity[] newCards)
    {
        //Iterate for each type of card in the deck
        foreach (DeckQuantity displayCard in newCards)
        {
            //Check the cardQuantity value and spawn that many of the corresponding card type
            for (int i = 0; i < displayCard.amtInDeck; i++)
            {
                //Assign new card values
                GameObject newCard = Instantiate(objectReferences.emptyCard, Vector3.zero, Quaternion.identity, objectReferences.deckHolder);
                newCard.GetComponent<DisplayCard>().card = displayCard.cardType; //Set card type
                newCard.GetComponent<DisplayCard>().level = (displayCard.cardLevel - 1); //Set card level
                newCard.name = displayCard.cardType.name;

                newCard.transform.SetParent(objectReferences.displayZone.transform);

                //Add the new card into the display array
                display.Add(newCard);            
            }
        }
    }

    //Clear the display of new cards and add them into the deck
    public void AddNewCardsToDeck()
    {
        //Add all cards from the display to the deck and disable them
        foreach (GameObject card in display)
        {
            deck.Add(card);
            card.transform.SetParent(objectReferences.deckHolder.transform);
            card.SetActive(false);
        }

        //Remove all cards from the display array
        display.Clear();

        //Update the deck and discard pile quantities text
        UpdateCardText();
    }

    //Draws cards up to the max hand size
    private void DrawHand()
    {
        for (int i = 0; i < maxHandSize; i++)
        {
            DrawCard();
        }
    }

    //Draws a card randomly from the deck
    public void DrawCard()
    {
        //Check if the deck has cards and if the hand is full before allowing a card draw
        if(deck.Count > 0 && hand.Count < maxHandSize)
        {
            //Get a random card value from the deck
            int randCard = Random.Range(0, deck.Count);

            //Move the card from the deck to the player's hand
            GameObject drawnCard = deck[randCard];
            deck.Remove(drawnCard);
            hand.Add(drawnCard);
            drawnCard.transform.SetParent(objectReferences.handZone.transform);
            drawnCard.SetActive(true);

            //Update the deck and discard pile quantities text
            UpdateCardText();

            //Update the indexes of the cards in the hand
            CardHandController.Instance.UpdateCardIndexes();
        }
        else
        {
            /*
            if (deck.Count == 0)
                Debug.Log("Failed to draw a card because there are no cards in the deck");
            else
                Debug.Log("Failed to draw a card because the max hand size was reached");
            */
        }
    }

    //Moves the selected card from the player's hand to the discard pile
    public void DiscardCard()
    {
        if(CardSelector.cardSelected != null)
        {
            GameObject discardCard = CardSelector.cardSelected;

            //Move card to discard
            hand.Remove(discardCard);
            discard.Add(discardCard);

            //Disable card
            discardCard.transform.SetParent(objectReferences.discardHolder);
            discardCard.SetActive(false);

            //Set the card selected value to null
            CardSelector.cardSelected = null;

            //Update the deck and discard pile quantities text
            UpdateCardText();
        }
        //else
            //Debug.Log("Failed to discard a card because no card was selected");
    }

    public void PurchaseCard()
    {
        PlayerStats.Instance.SubtractGold(cardCost);
        cardCost += costIncPerCard;
        DrawCard();
    }

    //Adds all card objects from the Hand and Discard back into the Deck and disable them
    public void ResetDeck()
    {
        CardSelector.cardSelected = null; //Deselect the selected card
        CardSelector.cardHovered = null; //Deselect the hovered card

        //Move Hand cards into Deck, disable them, and reset the scaling
        foreach (GameObject card in hand)
        {
            deck.Add(card);
            card.SetActive(false);
            card.GetComponent<CardSelector>().ResetTransform();
            card.transform.SetParent(objectReferences.deckHolder);
        }

        //Move Discard cards into Deck, disable them, and reset the scaling
        foreach (GameObject card in discard) 
        {
            deck.Add(card);
            card.SetActive(false);
            card.GetComponent<CardSelector>().ResetTransform();
            card.transform.SetParent(objectReferences.deckHolder);
        }

        //Clear the Hand and Discard lists and update the UI
        hand.Clear();
        discard.Clear();
        UpdateCardText();
    }


    private void UpdateCardText()
    {
        objectReferences.deckQuantityText.text = "(" + deck.Count.ToString() + ")";
        objectReferences.discardQuantityText.text = "(" + discard.Count.ToString() + ")";
        objectReferences.cardCostText.text = cardCost.ToString();
    }
}
