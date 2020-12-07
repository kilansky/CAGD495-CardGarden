using System.Collections;
using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using System.Runtime.Remoting.Messaging;
using UnityEngine;

public enum SeasonType { Spring, Summer, Autumn, Winter, Untyped };

[System.Serializable]
[CreateAssetMenu(fileName = "New Season", menuName = "Season")]
public class SeasonTypes : ScriptableObject
{
    public SeasonType season;

    private static float allyDamageMod = 0.75f;
    private static float enemyDamageMod = 1.5f;
    private static float neutralDamageMod = 1.0f;

    public List<SeasonType> AllySeasons = new List<SeasonType>();
    public List<SeasonType> EnemySeasons = new List<SeasonType>();

    public float GetDamageMod(SeasonType otherSeason)
    {
        if (AllySeasons.Contains(otherSeason))
        {
            return allyDamageMod;
        }
        else if (EnemySeasons.Contains(otherSeason))
        {
            return enemyDamageMod;
        }
        else
        {
            return neutralDamageMod;
        }
    }

    // for debugging purposes...
    public string GetSeasonString()
    {
        switch (season)
        {
            case (SeasonType.Autumn):
                return "Autumn";
            case (SeasonType.Summer):
                return "Summer";
            case (SeasonType.Spring):
                return "Spring";
            case (SeasonType.Winter):
                return "Winter";
        };

        return "Untyped";
    }
}











