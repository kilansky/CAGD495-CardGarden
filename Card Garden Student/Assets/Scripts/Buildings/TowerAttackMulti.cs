using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// inherits from TowerAttack
public class TowerAttackMulti : TowerAttack
{
    public int numHives;

    // scan all utility honey pots; sum their bonuses
    public int NumHives {
        get
        {
            int bonus = 0;
            UtilityHoneyPot[] utilityBuildings = FindObjectsOfType<UtilityHoneyPot>();
            foreach (UtilityHoneyPot u in utilityBuildings)
                bonus += u.GetHiveBonus();
            return bonus + numHives;
        }
    }

    //Fires a single projectile at EACH enemy within this tower's radius
    protected override void Attack() 
    {
        Transform target;
        bool hit_something = false;

        for (int i=0; i < enemies.Count; i++)
        {
            if (i == NumHives || NumHives <= 0) break; // ensure limit projectils to numHives

            //If the i'th enemy in the list has not been destroyed, set it as the current target
            if (enemies[i])
                target = enemies[i];
            else //Otherwise, remove it from the list
            {
                target = null;
                enemies.Remove(enemies[i]);
            }

            //If there is a target and fireCountdown is 0 or less, fire a projectile
            if (target && fireCountdown <= 0)
            {
                FireProjectile(target);
                hit_something = true;
            }
        }

        if (hit_something) fireCountdown = attackRate;

        //Decrease the fireCountdown timer
        fireCountdown -= Time.deltaTime;
    }
}
