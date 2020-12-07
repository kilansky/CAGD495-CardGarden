using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Building, Minion, Spell };
public enum BuildingSubtype { Tower, Generator, Utility };
public enum MinionSubtype { PersonPeople };
public enum SpellSubtype { LevelUp, TileEffect, Emergency };
public enum ProgressionType { Full, ThreeQuarters, Half };

[System.Serializable]
public class CardStats
{
    public float baseValue;
    public float growthValue;
    public ProgressionType progressionType;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //----------------Card Type & Prefabs----------------
    [HideInInspector] public CardType cardType;
    [HideInInspector] public SeasonTypes seasonType;
    [HideInInspector] public BuildingSubtype buildingSubtype;
    [HideInInspector] public MinionSubtype minionSubtype;
    [HideInInspector] public SpellSubtype spellSubtype;
    [HideInInspector] public GameObject thingToSpawn;
    [HideInInspector] public GameObject thingInProgress;

    //----------------Card Text & Visuals----------------
    [HideInInspector] public new string name;
    [HideInInspector] public string description;
    [HideInInspector] public Sprite artwork;

    //----------------Card Stats----------------
    public float[] costs;
    public CardStats costStats;

    public float[] buildTimes;
    public CardStats buildTimeStats;

    public float[] attackPowers;
    public CardStats attackPowerStats;

    public float[] attackRates;
    public CardStats attackRateStats;
    public float minAttackRate;

    public float[] attackRanges;
    public CardStats attackRangeStats;

    //Minion Exclusive Stats
    public float[] minionHealths;
    public CardStats minionHealthStats;

    //Generator Exclusive Stats
    public float[] goldGenAmounts;
    public CardStats goldGenAmountStats;

    //Spell Exclusive Stats
    public float[] spellDurations;
    public CardStats spellDurationStats;

    //----------------Spell Methods----------------
    public Spell spell;

    //Called when the Card inspector is saved to set all stats
    public void CalculateAllStats()
    {
        CalculateStats(costStats, costs);
        CalculateStats(buildTimeStats, buildTimes);
        CalculateStats(attackPowerStats, attackPowers);
        CalculateStats(attackRangeStats, attackRanges);
        CalculateAttackRate(attackRateStats, attackRates);
        CalculateStats(minionHealthStats, minionHealths);
        CalculateStats(goldGenAmountStats, goldGenAmounts);
        CalculateStats(spellDurationStats, spellDurations);
    }

    private void CalculateStats(CardStats stat, float[] statArray)
    {
        statArray[0] = stat.baseValue;

        if (stat.progressionType == ProgressionType.Full)
        {
            //Increase stat per level with rate of 1, 1, 1, 1, 1
            for (int i = 1; i < statArray.Length; i++)
                statArray[i] = statArray[i - 1] + stat.growthValue; //Ex: cost[1] = cost[0] + costStats.growthValue
        }
        else if (stat.progressionType == ProgressionType.ThreeQuarters)
        {
            //Increase stat per level with rate of 1, 0.5, 1, 0.5, 1
            for (int i = 1; i < statArray.Length; i++)
            {
                if (i % 2 == 0) //If even
                    statArray[i] = statArray[i - 1] + stat.growthValue; //Ex: cost[2] = cost[1] + costStats.growthValue
                else //If odd
                    statArray[i] = statArray[i - 1] + stat.growthValue / 2; //Ex: cost[1] = cost[0] + costStats.growthValue / 2
            }
        }
        else //ProgressionType.Half
        {
            //Increase stat per level with rate of 1, 0, 1, 0, 1
            for (int i = 1; i < statArray.Length; i++)
            {
                if (i % 2 == 0) //If even
                    statArray[i] = statArray[i - 1] + stat.growthValue; //Ex: cost[2] = cost[1] + costStats.growthValue
                else //If odd
                    statArray[i] = statArray[i - 1];
            }
        }
    }

    private void CalculateAttackRate(CardStats stat, float[] statArray)
    {
        statArray[0] = stat.baseValue;

        if (stat.progressionType == ProgressionType.Full)
        {
            //Increase stat per level with rate of 1, 1, 1, 1, 1
            for (int i = 1; i < statArray.Length; i++)
                statArray[i] = statArray[i - 1] - stat.growthValue; //Ex: cost[1] = cost[0] + costStats.growthValue
        }
        else if (stat.progressionType == ProgressionType.ThreeQuarters)
        {
            //Increase stat per level with rate of 1, 0.5, 1, 0.5, 1
            for (int i = 1; i < statArray.Length; i++)
            {
                if (i % 2 == 0) //If even
                    statArray[i] = statArray[i - 1] - stat.growthValue; //Ex: cost[2] = cost[1] + costStats.growthValue
                else //If odd
                    statArray[i] = statArray[i - 1] - stat.growthValue / 2; //Ex: cost[1] = cost[0] + costStats.growthValue / 2
            }
        }
        else //ProgressionType.Half
        {
            //Increase stat per level with rate of 1, 0, 1, 0, 1
            for (int i = 1; i < statArray.Length; i++)
            {
                if (i % 2 == 0) //If even
                    statArray[i] = statArray[i - 1] - stat.growthValue; //Ex: cost[2] = cost[1] + costStats.growthValue
                else
                    statArray[i] = statArray[i - 1];
            }
        }

        for (int i = 1; i < statArray.Length; i++)
        {
            if (statArray[i] < minAttackRate)
                statArray[i] = minAttackRate;
        }
    }
}
