using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    public float tileSpawnOffset = 1f;
    public RectTransform HandZone;
    public Material highlightMat;

    private Material objectMat;
    private GameObject hitTile;

    private void Update()
    {
        //Reset the material any highlighted tiles and un-store them
        DeselectTiles();

        //Only perform the raycast to tiles if a card has been selected
        if (CardSelector.cardSelected)
            MouseRaycast();
    }

    private void MouseRaycast()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Check if the mouse is above the Hand Zone & Shoot a raycast at the mouse position
        if (Input.mousePosition.y > HandZone.rect.height && Physics.Raycast(mouseRay, out hitInfo))
        {
            //Check if the raycast hit a tile
            if (hitInfo.transform.gameObject.GetComponent<Tile>())
            {
                //Store the hit tile
                hitTile = hitInfo.transform.gameObject;

                //Highlight the tile visually
                HighlightTile();

                //Check if the mouse is left clicked
                if (Input.GetMouseButtonDown(0))
                {
                    PlaySelectedCard();
                }
            }
        }
    }

    private void DeselectTiles()
    {
        //Check if there is a stored hit object
        if (hitTile != null)
        {
            //Reset the material of the hit object and un-store it
            hitTile.GetComponent<Renderer>().material = objectMat;
            hitTile = null;
        }
    }

    private void HighlightTile()
    {
        //Store the hit tile's material
        objectMat = hitTile.GetComponent<Renderer>().material;

        //Change the material of the hit tile
        hitTile.GetComponent<Renderer>().material = highlightMat;
    }


    private void PlaySelectedCard()
    {
        //Get the data for the currently selected tile and card
        Tile selectedTile = hitTile.GetComponent<Tile>();
        Card cardToPlay = CardSelector.cardSelected.GetComponent<DisplayCard>().card;

        //Attempt to play the selected card
        if (validTilePlacement(selectedTile, cardToPlay))
        {
            //Store the data from the selected card on this tile
            selectedTile.storedCard = cardToPlay;

            //Discard the selected card
            CardManager.Instance.DiscardCard();

            //Spawn the object stored on the selected card
            GameObject newSpawn = Instantiate(cardToPlay.thingToSpawn, hitTile.transform.position + new Vector3(0, tileSpawnOffset, 0), Quaternion.identity);
            selectedTile.occupant = newSpawn;

            if(newSpawn.GetComponent<Building>())
                newSpawn.GetComponent<Building>().cardData = cardToPlay;
            //else if(newSpawn.GetComponent<PlayerUnitAI>())
                //newSpawn.GetComponent<PlayerUnitAI>().cardData = cardToPlay;

            //Reduce player gold amount
            PlayerStats.Instance.playerGold -= cardToPlay.cost;
        }
    }


    //Returns true if the selected card can be placed on the selected tile
    private bool validTilePlacement(Tile selectedTile, Card selectedCard)
    {
        int playerGold = (int)PlayerStats.Instance.playerGold;

        //Checks if tile is not occupied and the player has enough gold to pay for the card
        if (!selectedTile.occupant)
        {
            if(playerGold >= selectedCard.cost)
            {
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
                    Debug.Log("This type of card cannot be played on this tile");
            }
            else
                Debug.Log("Not enough gold to play this card");
        }
        else
            Debug.Log("This tile is occupied, cannot build here");

        return false;
    }
}
