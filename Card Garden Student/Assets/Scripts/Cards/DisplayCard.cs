using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCard : MonoBehaviour
{
    public Card card;
    public int level;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI subtypeText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI levelText;
    public Image artwork;
    public Image minionHealthIcon;

    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI minionHealthText;
    //public TextMeshProUGUI buildingRangeText;

    //Display all text and images using data from a Card
    private void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artwork.sprite = card.artwork;

        levelText.text = (level + 1).ToString();
        costText.text = card.costs[level].ToString();
        attackPowerText.text = card.attackPowers[level].ToString();
        attackSpeedText.text = card.attackRates[level].ToString();

        //Check the type of card in order to display more specific information
        if (card.cardType == CardType.Building)
        {
            //buildingRangeText.text = card.attackRanges[level].ToString();
            minionHealthText.text = null;
            minionHealthIcon.sprite = null;

            //Set null sprite color to match dark card background
            //Color blackCardColor;
            //ColorUtility.TryParseHtmlString("#141414", out blackCardColor);
            //minionHealthIcon.color = blackCardColor;
            //4B4B4B - Description Area Color

            //subtypeText.text = card.buildingSubtype.ToString();
            subtypeText.text = "Building";
            //gameObject.GetComponent<Image>().sprite = buildingCard;
        }
        else if (card.cardType == CardType.Minion)
        {
            minionHealthText.text = card.minionHealths[level].ToString();
            //subtypeText.text = card.minionSubtype.ToString();
            subtypeText.text = "Minion";
            //buildingRangeText.text = null;
            //gameObject.GetComponent<Image>().sprite = minionCard;
        }
        else if(card.cardType == CardType.Spell)
        {
            //subtypeText.text = card.spellSubtype.ToString();
            subtypeText.text = "Spell";
        }

    }

    private void Update()
    {
        //Set the card cost text color to be red if the player cannot afford the card
        costText.color = (PlayerStats.Instance.PlayerGold >= card.costs[level]) ? Color.black : Color.red;
    }
}
