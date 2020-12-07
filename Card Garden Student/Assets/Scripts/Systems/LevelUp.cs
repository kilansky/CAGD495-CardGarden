using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public enum ObjectType { Building, Minion, Enemy };

    public ObjectType objectType;
    [HideInInspector] public int baseLevel;
    public int[] expToNextLevel;

    private SeasonTypes[] seasons;
    private int[] seasonLevel;   
    [HideInInspector] public int exp = 0;

    [HideInInspector] public int totalLevel = 1;
    [HideInInspector] public int maxLevel = 6;

    /*
    [HideInInspector] public float elementalAttackPower = 0;
    [HideInInspector] public float elementalAttackRate = 0;
    [HideInInspector] public float elementalAttackRange = 0;
    [HideInInspector] public float elementalMinionHealth = 0;
    [HideInInspector] public float elementalMinionArmor = 0;
    */

    private void Start()
    {
		if(objectType != ObjectType.Enemy)
	    {
			gameObject.GetComponent<BarUI>().SetNextLevel(expToNextLevel[totalLevel]);
		}
		gameObject.GetComponent<BarUI>().SetLevel(totalLevel);
        //elementalLevels = ElementManager.Instance.elements;
        //elementLevel = new int[elementalLevels.Length];

        //Convert visible level to array element level (ie: lvl 1 is array element 0)
        if (baseLevel > 0)
            baseLevel--;
    }

    private void Update()
    {
       //---------ADD ELEMENTAL LEVELS WITH KEY PRESSES---------
       /*
        if (Input.GetKeyDown("b"))
        {
            Debug.Log("-----Add Base Level-----");
            IncreaseBaseLevel();
        }

        if (Input.GetKeyDown("p"))
        {
            Debug.Log("-----Add Spring Level-----");
            IncreaseSeasonalLevel(SeasonType.Spring);
        }

        if (Input.GetKeyDown("s"))
        {
            Debug.Log("-----Add Summer Level-----");
            IncreaseSeasonalLevel(SeasonType.Summer);
        }

        if (Input.GetKeyDown("a"))
        {
            Debug.Log("-----Add Autumn Level-----");
            IncreaseSeasonalLevel(SeasonType.Autumn);
        }

        if (Input.GetKeyDown("w"))
        {
            Debug.Log("-----Add Winter Level-----");
            IncreaseSeasonalLevel(SeasonType.Winter);
        }
        */
    }


    public void AddExp(int expToAdd)
    {
        //Check if tower is lower than max level
        if (totalLevel < maxLevel)
        {
            //Add Exp
            exp += expToAdd;

            //Check for level up
            if (exp >= expToNextLevel[totalLevel - 1])
            {
                IncreaseBaseLevel();
                exp = 0;
				if(objectType == ObjectType.Enemy)
				{
					gameObject.GetComponent<BarUI>().SetLevel(totalLevel);					
				}
				else
				{
					gameObject.GetComponent<BarUI>().SetNextLevel(expToNextLevel[totalLevel]);
					gameObject.GetComponent<BarUI>().SetLevel(totalLevel);
				}
            }
			//Debug.Log("level up");
			if(objectType != ObjectType.Enemy)
			{
				gameObject.GetComponent<BarUI>().SetXP(exp);
			}
        }
        else
        {
            //Debug.Log("Reached the max level and failed to level up again");
        }
    }

    public void IncreaseBaseLevel()
    {
        //Check if tower is lower than max level
        if (totalLevel < maxLevel)
        {
            //Increase the current level
            baseLevel++;
            totalLevel++;

            SetNewStats();
            // -- Added by Arjun to get the UI elements to show when you level up from sources other than XP
            if (objectType == ObjectType.Enemy)
            {
                gameObject.GetComponent<BarUI>().SetLevel(totalLevel);
                GetComponent<EnemyUnitAI>().SetLevelStats();
            }
            else
            {
                gameObject.GetComponent<BarUI>().SetNextLevel(expToNextLevel[totalLevel]);
                gameObject.GetComponent<BarUI>().SetLevel(totalLevel);
            }
            // -- End code added by Arjun
        }
        else
        {
            //Debug.Log("Reached the max level and failed to level up again");
        }
    }

    /*
    public void IncreaseSeasonalLevel(SeasonType seasonType)
    {
        //Only allow this level up if the max level has not been reached
        if (totalLevel < maxLevel)
        {
            int i = 0;
            //Check each element for the appropriate elementType to level up
            foreach (SeasonTypes season in seasons)
            {
                
            }

            SetNewStats();
        }
        else
            Debug.Log("Reached the max level and failed to level up again");
    }
    */

    /*
    public void IncreaseElementalLevel(ElementType elementType)
    {
        //Only allow this level up if the max level has not been reached
        if (totalLevel < maxLevel)
        { 
            int i = 0;
            //Check each element for the appropriate elementType to level up
            foreach (ElementalLevels element in elementalLevels)
            {
                if(element.elementType == elementType)
                {
                    //Remove the buff of the previous level of this element
                    elementalAttackPower -= element.attackPower[elementLevel[i]];
                    elementalAttackRate -= element.attackRate[elementLevel[i]];
                    elementalAttackRange -= element.attackRange[elementLevel[i]];
                    elementalMinionHealth -= element.minionHealth[elementLevel[i]];
                    elementalMinionArmor -= element.minionArmor[elementLevel[i]];

                    //Increase the total level and element level of this element type
                    totalLevel++;
                    elementLevel[i]++;
                    Debug.Log(elementType.ToString() + " element is now level " + elementLevel[i]);

                    //Add the buff of the new level of this element
                    elementalAttackPower += element.attackPower[elementLevel[i]];
                    elementalAttackRate += element.attackRate[elementLevel[i]];
                    elementalAttackRange += element.attackRange[elementLevel[i]];
                    elementalMinionHealth += element.minionHealth[elementLevel[i]];
                    elementalMinionArmor += element.minionArmor[elementLevel[i]];
                    break;
                }
                i++;
            }

            SetNewStats();
        }
        else
            Debug.Log("Reached the max level and failed to level up again");
    }
    */

    //Set the new stats for the leveled up object
    private void SetNewStats()
    {       
        switch (objectType)
        {
            case ObjectType.Building:
                GetComponent<Building>().SetLevelUpStats();
                break;
            case ObjectType.Minion:
                GetComponent<PlayerUnitAI>().SetLevelUpStats();
                break;
            case ObjectType.Enemy:
                //GetComponent<EnemyUnitAI>().LevelUp();
                break;
            default:
                break;
        }
    }
}
