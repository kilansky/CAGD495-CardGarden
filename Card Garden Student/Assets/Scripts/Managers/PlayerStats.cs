using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each building that adjusts CDR will create one of these when they spawn. 
[System.Serializable]
public class CDRMod
{
    public float drawRateAmount = 0f;
    public GameObject sourceObject;

    public CDRMod(float drawRateChange, GameObject source)
    {
        drawRateAmount = drawRateChange;
        sourceObject = source;
    }
}

// Each building that adjusts GoldIncome will create one of these when they spawn. 
[System.Serializable]
public class GoldIncomeMod
{
    public float incomeAmount = 0f;
    public GameObject sourceObject;

    public GoldIncomeMod(float incomeChange, GameObject source)
    {
        incomeAmount = incomeChange;
        sourceObject = source;
    }
}

public class PlayerStats : SingletonPattern<PlayerStats>
{
    private float playerGold;
    public float PlayerGold { get { return playerGold; } }

    private float goldIncome;
    public float GoldIncome { get { return goldIncome; } }

    // Player Gold Variables
    [Header("Player Gold")]
    public float startingGold;

    [SerializeField]
    private float baseGoldIncome = 0;
    public float goldIncomeInterval = 1f;
    //public List<GoldIncomeMod> goldIncomeMods = new List<GoldIncomeMod>();

    /*
    // Player Card Draw Variables
    [Header("Player Card Draw")]
    public float cardDrawTimeRemaining;
    [SerializeField]
    private float cardDrawBaseTimer = 10.0f;
    public float cardDrawTimer; // need to create a timer for this
    public List<CDRMod> cdrMods = new List<CDRMod>();
    private float extraCardsDrawn = 0.0f;
    */

    // Set starting gold and income amounts
    private void Start()
    {      
        playerGold = startingGold;
        AddGoldIncome(baseGoldIncome);
        InvokeRepeating(nameof(GainIncome), 1.0f, goldIncomeInterval);
    }

    private void Update()
    {
        // Temporarily here, move to optimize later
        //CalculateCardDrawTimer();
        //CalculateGoldIncome();
    }

    // Gold Income (Automatic)
    public void AddGoldIncome(float value)
    {
        goldIncome += value;
        //AddGoldIncomeMod(value, gameObject);
    }

    // Gold Income (Automatic)
    private void GainIncome()
    {
        AddGold(goldIncome);
    }

    // functions to add or subtract gold from the total to keep code clean
    public void AddGold(float goldToAdd)
    {
        playerGold += goldToAdd;
    }
    public void SubtractGold(float goldToRemove)
    {
        playerGold -= goldToRemove;
    }

    /*

    // Functions to be called to add new sources
    public void AddGoldIncomeMod(float income, GameObject source)
    {
        goldIncomeMods.Add(new GoldIncomeMod(income, source));
    }

    // Functions to remove sources
    public void RemoveGoldIncomeMod(GameObject source)
    {
        foreach (GoldIncomeMod item in goldIncomeMods)
        {
            if (item.sourceObject == source)
            {
                goldIncomeMods.Remove(item);
                // do we need to destroy it as well?
            }
        }
    }

    // Functions to change source values
    public void ChangeGoldIncomeMod(GameObject source, float changeValue)
    {
        foreach (GoldIncomeMod item in goldIncomeMods)
        {
            if (item.sourceObject == source)
            {
                item.incomeAmount = changeValue;
            }
        }
        CalculateGoldIncome();
    }
   
    public void CalculateGoldIncome()
    {
        float adjustedIncome = baseGoldIncome;    // start at 0
        foreach (GoldIncomeMod item in goldIncomeMods)
        {
            adjustedIncome += item.incomeAmount; // add from all
        }
        goldIncome = adjustedIncome;
    }

    // Card Draw (Automatic)
    private void DrawCard()
    {
        // Add in function to access card manager and draw a card if hand has room, else wait
    }

    // Calculate Cost to draw an additional card
    public float CardDrawCost()
    {
        return 10.0f * (1 - (cardDrawTimeRemaining / cardDrawBaseTimer + 0.5f) * extraCardsDrawn);
    }

    public void AddCardDrawMod(float rate, GameObject source)
    {
        cdrMods.Add(new CDRMod(rate, source));
    }

    public void RemoveCDRMod(GameObject source)
    {
        foreach (CDRMod item in cdrMods)
        {
            if (item.sourceObject == source)
            {
                cdrMods.Remove(item);
                // do we need to destroy it as well?
            }
        }
    }

    public void ChangeCDRMod(GameObject source, float changeValue)
    {
        foreach (CDRMod item in cdrMods)
        {
            if (item.sourceObject == source)
            {
                item.drawRateAmount = changeValue;
            }
        }
        CalculateCardDrawTimer();
    }

    // Functions to calcualte totals from the values in the lists
    private void CalculateCardDrawTimer()
    {
        float adjustedRated = cardDrawBaseTimer; // start at 10s
        foreach (CDRMod item in cdrMods)
        {
            adjustedRated -= item.drawRateAmount; // subtract from all
        }
        cardDrawTimeRemaining = adjustedRated;
    }
    /**/
}
