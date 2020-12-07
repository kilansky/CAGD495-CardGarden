using System.Dynamic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public int attackBonus; 

    public virtual void SetUtilityStats()
    {
        // let derived classes set their own stats
    }

    public virtual int GetAttackBonus(SeasonType towerSeason)
    {
        // let derived classes get their attack bonus, depending on whos calling
        return 0;
    }
}
