﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public Material testMat;

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

        //Shoot a raycast at the mouse position
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            //Debug.Log("Ray Hit");

            //Check if the raycast hit a tile
            if (hitInfo.transform.gameObject.GetComponent<Tile>())
            {
                //Store the hit tile and its material
                hitObject = hitInfo.transform.gameObject;
                objectMat = hitObject.GetComponent<Renderer>().material;

                //Change the material of the hit tile
                hitObject.GetComponent<Renderer>().material = testMat;

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
        Instantiate(cardData.thingToSpawn, hitObject.GetComponent<Tile>().spawnPos.position, Quaternion.identity);
    }

    //Returns true if the selected card can be placed on the selected tile
    private bool validTilePlacement(Tile selectedTile, Card selectedCard)
    {
        //Tile = Empty && Card = Building
        if (selectedTile.tileType == TileType.empty && selectedCard.cardType == CardType.building)
        {
            return true;
        }
        //Tile = Path && Card = Minion
        else if (selectedTile.tileType == TileType.path && selectedCard.cardType == CardType.minion)
        {
            return true;
        }
        else
            return false;
    }
}