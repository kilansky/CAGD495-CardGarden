using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject emptyCard;
    public GameObject canvas;

    public List<Card> cards = new List<Card>();
    public List<int> cardCounts = new List<int>();

    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> hand = new List<GameObject>();
    public List<GameObject> discard = new List<GameObject>();

    private void Start()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    private void GenerateDeck()
    {
        int cardNum = 0;
        //Iterate for each type of card in the deck
        foreach (Card card in cards)
        {
            //Check the cardCount value and spawn that many of the corresponding card type
            for (int i = 0; i < cardCounts[cardNum]; i++)
            {
                GameObject newCard = Instantiate(emptyCard, Vector3.zero, Quaternion.identity, canvas.transform);
                newCard.GetComponent<DisplayCard>().card = card;
                newCard.GetComponent<RectTransform>().position = new Vector3(1000, 1000, 1000);
                newCard.name = card.name;
            }
            cardNum++;
        }
    }

    private void ShuffleDeck()
    {

    }

    private void DrawCard()
    {

    }

    private void DiscardCard()
    {

    }
}
