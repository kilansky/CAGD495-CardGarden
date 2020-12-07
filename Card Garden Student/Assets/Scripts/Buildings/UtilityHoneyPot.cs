using UnityEngine;

public class UtilityHoneyPot : Utility
{
    public int hiveNumBonus; 

    public void SetUtilityStats(int d, int h)
    {
        attackBonus = d;
        hiveNumBonus = h;
    }

    public int GetHiveBonus()
    {
        return hiveNumBonus;
    }

    public override int GetAttackBonus(SeasonType towerSeason)
    {
        // give bonus only if spring type
        if (towerSeason == SeasonType.Spring) return attackBonus;
        else return 0;
    }
}
