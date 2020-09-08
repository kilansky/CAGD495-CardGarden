using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public float tileSpawnOffset = 1f;
    public RectTransform HandZone;
    public Material highlightMat;

    private Material objectMat;
    private GameObject hitObject;

    private void Update()
    {
        //Check if there is a stored hit object
        if (hitObject != null)
        {
            //Reset the material of the hit object and un-store it
            hitObject.GetComponent<Renderer>().material = objectMat;
            hitObject = null;
        }

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Check if the mouse is above the Hand Zone & Shoot a raycast at the mouse position
        if (Input.mousePosition.y > HandZone.rect.height && Physics.Raycast(mouseRay, out hitInfo))
        {
            //Debug.Log("Ray Hit");

            //Check if the raycast hit a tile
            if (hitInfo.transform.gameObject.GetComponent<Tile>())
            {
                //Store the hit tile and its material
                hitObject = hitInfo.transform.gameObject;
                objectMat = hitObject.GetComponent<Renderer>().material;

                //Change the material of the hit tile
                hitObject.GetComponent<Renderer>().material = highlightMat;

                //Check if the mouse is left clicked & there is a card selected currently
                if (Input.GetMouseButtonDown(0) && CardSelector.cardSelected)
                {
                    //Get the data for the currently selected tile and card
                    Card selectedCard = CardSelector.cardSelected.GetComponent<DisplayCard>().card;
                    Tile selectedTile = hitObject.GetComponent<Tile>();

                    //Attempt to play the selected card
                    if (validTilePlacement(selectedTile, selectedCard))
                        PlaySelectedCard(selectedCard);
                    else
                        Debug.Log("Unable to place the selected card on this tile");
                }
            }
        }
        else //Raycast did not hit anything
        {
            //Debug.Log("Ray did not Hit");
        }
    }

    private void PlaySelectedCard(Card cardData)
    {
        //Store the data from the selected card on this tile
        hitObject.GetComponent<Tile>().storedCard = cardData;

        //Discard the selected card
        CardManager.Instance.DiscardCard();

        //Spawn the object stored on the selected card
        Instantiate(cardData.thingToSpawn, hitObject.transform.position + new Vector3(0, tileSpawnOffset,0), Quaternion.identity);
    }

    //Returns true if the selected card can be placed on the selected tile
    private bool validTilePlacement(Tile selectedTile, Card selectedCard)
    {
        //NEEDS TO CHECK IF SOMETHING HAS ALREADY BEEN SPAWNED ON THE SELECTED TILE

        //Tile = Empty && Card = Building
        if (selectedTile.tileType == tileEnum.Building && selectedCard.cardType == CardType.Building)
        {
            return true;
        }
        //Tile = Path && Card = Minion
        else if (selectedTile.tileType == tileEnum.Lane && selectedCard.cardType == CardType.Minion)
        {
            return true;
        }
        else
            return false;
    }
}
