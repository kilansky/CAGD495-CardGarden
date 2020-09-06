using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCard : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI minionHealthText;
    public TextMeshProUGUI buildingRangeText;

    private void Start()
    {
        nameText.text = card.name;

        costText.text = card.cost.ToString();
        
        attackPowerText.text = card.attackPower.ToString();

        buildingRangeText.text = card.buildingRange.ToString();
        minionHealthText.text = card.minionHealth.ToString();

        if (card.cardType == Card.CardType.building)
        {
            minionHealthText.text = null;
        }
        else if (card.cardType == Card.CardType.minion)
        {
            buildingRangeText.text = null;
        }
    }

}
