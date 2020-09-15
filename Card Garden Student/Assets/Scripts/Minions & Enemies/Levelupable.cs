using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelupable : MonoBehaviour
{
  	// unit's arrays are indexed by level and sized by max level
    public int level;
    public int maxLevel;
    public int startingLevel; 

    private void Start()
    {
        level = startingLevel;
    }

    public void LevelUp()
    {
        level = Mathf.Min(level + 1, maxLevel);
    }
}
