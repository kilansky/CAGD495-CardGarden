using UnityEngine;

public class UtilityBlacksmith : Utility
{
    public void SetUtilityStats(int d) 
    {
        attackBonus = d;
    }

    public override int GetAttackBonus(SeasonType towerSeason)
    {
        return attackBonus;
    }
}
