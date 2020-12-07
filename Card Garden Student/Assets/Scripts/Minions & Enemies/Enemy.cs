using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public float baseValue;
    public float growthValue;
    public ProgressionType progressionType;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public SeasonTypes seasonType;
    public EnemyClass enemyClass;
    public GameObject projectilePrefab;

    public float[] movementSpeeds = new float[20];
    public EnemyStats movementSpeedStats;

    public float[] maxHealths = new float[20];
    public EnemyStats maxHealthStats;

    public float[] attackPowers = new float[20];
    public EnemyStats attackPowerStats;

    public float[] attackRanges = new float[20];
    public EnemyStats attackRangeStats;

    public float[] attackRates = new float[20];
    public EnemyStats attackRateStats;
    public float minAttackRate;

    public float[] expValues = new float[20];
    public EnemyStats expValueStats;

    public float[] goldDropped = new float[20];
    public EnemyStats goldDroppedStats;


    //Called when the Card inspector is saved to set all stats
    public void CalculateAllStats()
    {
        CalculateStats(movementSpeedStats, movementSpeeds);
        CalculateStats(maxHealthStats, maxHealths);
        CalculateStats(attackPowerStats, attackPowers);
        CalculateStats(attackRangeStats, attackRanges);
        CalculateAttackRate(attackRateStats, attackRates);
        CalculateStats(expValueStats, expValues);
        CalculateStats(goldDroppedStats, goldDropped);
    }


    private void CalculateStats(EnemyStats stat, float[] statArray)
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

    private void CalculateAttackRate(EnemyStats stat, float[] statArray)
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
