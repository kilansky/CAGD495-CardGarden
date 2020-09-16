using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCard : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Image backgroundArt;

    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI minionHealthText;
    public TextMeshProUGUI buildingRangeText;

    public Sprite buildingCard;
    public Sprite minionCard;

    //Display all text and images using data from a Card
    private void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        backgroundArt.sprite = card.artwork;
        costText.text = card.cost.ToString();
        
        attackPowerText.text = card.attackPower.ToString();
        attackSpeedText.text = card.attackRate.ToString();
        buildingRangeText.text = card.attackRadius.ToString();
        minionHealthText.text = card.minionHealth.ToString();

        //Check the type of card in order to display more specific information
        if (card.cardType == CardType.Building)
        {
            minionHealthText.text = null;
            gameObject.GetComponent<Image>().sprite = buildingCard;
        }
        else if (card.cardType == CardType.Minion)
        {
            buildingRangeText.text = null;
            gameObject.GetComponent<Image>().sprite = minionCard;
        }
    }

}
