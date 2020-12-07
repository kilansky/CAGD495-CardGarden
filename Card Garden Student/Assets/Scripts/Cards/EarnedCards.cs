using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EncounterMatrix
{
    public DeckQuantity[] earnedCards;
}

public class EarnedCards : SingletonPattern<EarnedCards>
{
    public EncounterMatrix[] encounters;

    private void Update()
    {
        /*
        if(Input.GetKeyDown("r"))
        {
            CardManager.Instance.GenerateNewCards(encounters[0].earnedCards);
        }

        if (Input.GetKeyDown("t"))
        {
            CardManager.Instance.GenerateNewCards(encounters[1].earnedCards);
        }

        if (Input.GetKeyDown("y"))
        {
            CardManager.Instance.AddNewCardsToDeck();
        }
        */
    }
}
