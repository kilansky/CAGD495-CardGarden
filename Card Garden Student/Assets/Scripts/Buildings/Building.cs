using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour
{
    public int level;
    private float goldGenPerSec;

    [HideInInspector] public Card cardData;
    [HideInInspector] public BuildingSubtype buildingSubtype;
    [HideInInspector] public GameObject rangeSphere;
    [HideInInspector] public GameObject projectile;
    [HideInInspector] public Transform firePoint;
    [HideInInspector] public SeasonTypes seasonType; // TODO buildings need seasons?

    private void Start()
    {
        //Set base stats of the tower at start
        seasonType = cardData.seasonType;
        SetLevelUpStats();
    }

    //Sets the new stats based on building type and level
    public void SetLevelUpStats()
    {
        level = GetComponent<LevelUp>().baseLevel;
        //Debug.Log(gameObject.name + " is now Level " + GetComponent<LevelUp>().totalLevel);

        //If TOWER: Set tower attack stats
        if (buildingSubtype == BuildingSubtype.Tower)
            SetTowerStats();

        //If GENERATOR: Begin generating gold repeatedly
        else if (buildingSubtype == BuildingSubtype.Generator)
            SetGeneratorStats();

        else if (buildingSubtype == BuildingSubtype.Utility)
            SetUtilityStats();
    }

    //Sets this towers attack rate, power, and range based on its current level
    private void SetTowerStats()
    {
        LevelUp levelUp = GetComponent<LevelUp>();
        float attackPower = cardData.attackPowers[level];// + levelUp.elementalAttackPower;
        float attackRate = cardData.attackRates[level];// + levelUp.elementalAttackRate;
        float attackRange = cardData.attackRanges[level];// + levelUp.elementalAttackRange;

        rangeSphere.GetComponent<TowerAttack>().SetLevelStats(attackPower, attackRate, attackRange);
    }

    private void SetGeneratorStats()
    {
        //If the amount of gold generation has increased, add the new amt to the player's Gold Income
        if (goldGenPerSec < cardData.goldGenAmounts[level])
        {
            //If gold gen has increased from 1 to 3, increase Income by 2
            PlayerStats.Instance.AddGoldIncome(-goldGenPerSec);
            goldGenPerSec = cardData.goldGenAmounts[level];
            PlayerStats.Instance.AddGoldIncome(goldGenPerSec);
        }
    }

    private void SetUtilityStats()
    {
        // blacksmiths increases attack bonus
        // honey pot increases attack bonus on spring types and increases projectile count on apiary buildings
        int attackBonus = Mathf.FloorToInt(cardData.attackPowers[level]);
        int projectileBonus = Mathf.FloorToInt(cardData.attackRanges[level]); // attack range is representing projectile bonus here
        UtilityBlacksmith bs = gameObject.GetComponent<UtilityBlacksmith>();
        UtilityHoneyPot hp = gameObject.GetComponent<UtilityHoneyPot>();
        if (bs != null)
        {
            /*
            print(gameObject.name +
                ", a blacksmith's attack bonus and projectile bonus: " +
                attackBonus + " : " + projectileBonus);*/
            bs.SetUtilityStats(attackBonus);
        }
        else if (hp != null)
        {
            /*print(gameObject.name +
                ", a honey pots's attack bonus and projectile bonus: " +
                attackBonus + " : " + projectileBonus);*/
            hp.SetUtilityStats(attackBonus, projectileBonus);
        }
    }
}

















