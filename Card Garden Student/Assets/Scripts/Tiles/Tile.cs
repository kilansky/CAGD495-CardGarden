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
public class Tile : MonoBehaviour
{
    public enum tileEnum
	{
		Null,
		Lane,
		Building
	}	
	public enum directionEnum
	{
		Zup,
		Zdown,
		Xnegative,
		Xpositive,
		None
	}
	public enum textures
	{
		grass,
		rock,
		tree,
		water
	}
	
    public tileEnum tileType;
	public directionEnum direction;
	public textures tileTexture;
	
	public GameObject occupant;

    private Card storedCard;
	public Tile nextTile;
	
	/**
	* @brief instantiates a building on top of the tile based on the card played
	**/
	public void instantiateBuilding()
	{
		
	}
	/**
	* @brief captures tile events
	**/
	public void eventController()
	{
		
	}
	
	/**
	* @brief Changes the texture on a tile.
	**/
	private void tileTypeSelector()
	{
		/*if (this.tileTexture == textures.rock)
		{
			this.gameObject.GetComponent<Renderer>().material = 
		}*/
	}
	
	/**
	* @brief changes the tile this tile points to.
	*/
	public void directionChanger()
	{

	}
	
	/**
	* @brief removes or adds an enemy or player character to the tile.
	**/
	public void changeOccupant()
	{
		if(this.occupant == default(GameObject))
		{
			RaycastHit hit;
			Physics.BoxCast(this.transform.position,
			transform.localScale, transform.up, out hit,
			transform.rotation);
			occupant = hit.collider.gameObject;
		}
		else
		{
			occupant = default(GameObject);
		}
	}
	
}
