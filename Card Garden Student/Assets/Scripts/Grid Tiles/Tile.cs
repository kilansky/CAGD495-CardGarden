/**
* @author Jacob Picard
* @date 9/8/2020
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* @brief This class controls a tiles behavior in the grid.
**/
public enum tileEnum
{
	Locked,
	Lane,
	Building
}

public class Tile : MonoBehaviour
{
    public tileEnum tileType;

    [HideInInspector]
    public Card storedCard;

    public bool isOccupied;
	
	
	private void Start()
	{
		sendToList();
	}
	
	/**
	* @brief removes or adds an enemy or player character to the tile.
	**/
	public bool checkIfOccupied()
	{
        RaycastHit hit;
        Physics.BoxCast(transform.position + new Vector3(0,1,0), transform.localScale, transform.up, out hit, transform.rotation);

        if (hit.collider)
            return true;
        else
            return false;
    }
	
	private void sendToList()
	{
		if(tileType == tileEnum.Lane || tileType == tileEnum.Building)
		{
        	if (GameObject.Find("SpawnManager").GetComponent<Save>())
			{
                GameObject.Find("SpawnManager").GetComponent<Save>().tileList.Add(this);
			}
		}
	}
	
}
