using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : SingletonPattern<CardPlacement>
{
    private float tileSpawnOffset = 0.25f;
    public RectTransform HandZone;
    public Material highlightMat;
    public bool cubePlacement;

    private GameObject hitObject;
    private Material objectMat;

    private void Update()
    {
        //Reset the material any highlighted tiles/buildings/minions and un-store them
        DeselectObjects();

        //Only perform a raycast if a card has been selected and the game is NOT paused
        if (CardSelector.cardSelected && !MenuUI.Instance.isPaused)
            MouseRaycast();
    }

    private void MouseRaycast()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Check if the mouse is above the Hand Zone & Shoot a raycast at the mouse position
        if (Input.mousePosition.y > HandZone.rect.height && Physics.Raycast(mouseRay, out hitInfo))
        {
            //Check if the raycast hit an object
            if (hitInfo.transform.gameObject)
            {
                //Store the hit object, the card to play, and the card's level
                hitObject = hitInfo.transform.gameObject;
                Card cardToPlay = CardSelector.cardSelected.GetComponent<DisplayCard>().card;
                int level = CardSelector.cardSelected.GetComponent<DisplayCard>().level;

                //Check if the object selected is a tile, and that the selected card can be played on it
                if (hitObject.GetComponent<Tile>() && validTilePlacement(cardToPlay, level))
                {
                    HighlightObject(); //Highlight the tile visually

                    //Check if the mouse is left clicked to play the selected card
                    if (Input.GetMouseButtonDown(0))
                        PlaySelectedCard(cardToPlay, level);
                }
                //Check if the selected card is a spell, and that it can be placed on the hit object
                else if (cardToPlay.cardType == CardType.Spell && validSpellPlacement(cardToPlay, level))
                {
                    HighlightObject(); //Highlight the tile visually

                    //Check if the mouse is left clicked to play the selected card
                    if (Input.GetMouseButtonDown(0))
                        PlaySelectedSpell(cardToPlay, level);
                }
                //Check if the selected card can upgrade the hit object
                else if (validUnitUpgrade(cardToPlay))
                {
                    HighlightObject(); //Highlight the tile visually

                    //Check if the mouse is left clicked to play the selected card
                    if (Input.GetMouseButtonDown(0))
                        UpgradeUnit(cardToPlay, level);
                }
            }
        }
    }

    private void DeselectObjects()
    {
        //Check if there is a stored hit object
        if (hitObject != null && objectMat != null)
        {
            //Reset the material of the hit object and un-store it
            hitObject.GetComponent<Renderer>().material = objectMat;
            hitObject = null;
            objectMat = null;
        }
    }

    private void HighlightObject()
    {
        objectMat = hitObject.GetComponent<Renderer>().material;
        hitObject.GetComponent<Renderer>().material = highlightMat;
    }


    private void PlaySelectedCard(Card cardToPlay, int level)
    {
        //Reduce player gold amount
        PlayerStats.Instance.SubtractGold(cardToPlay.costs[level]);

        //Spawn the object stored on the selected card
        Tile selectedTile = hitObject.GetComponent<Tile>();
        GameObject newSpawn;

        //TEMP CODE TO CHECK IF SCENE USES CUBES OR TILES FOR SPAWN OFFSETS, REMOVE LATER!
        if (!cubePlacement)
            newSpawn = Instantiate(cardToPlay.thingInProgress, hitObject.transform.parent.position + new Vector3(0, tileSpawnOffset, 0), Quaternion.identity);
        else
            newSpawn = Instantiate(cardToPlay.thingInProgress, hitObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        selectedTile.isOccupied = true;

        //Set the card data and level of the object under construction
        if (newSpawn.GetComponent<BuildingConstruction>())
        {
            newSpawn.GetComponent<BuildingConstruction>().SetCard(cardToPlay);
            newSpawn.GetComponent<BuildingConstruction>().SetLevel(level);
        }
        else if (newSpawn.GetComponent<MinionConstruction>())
        {
            newSpawn.GetComponent<MinionConstruction>().SetCard(cardToPlay);
            newSpawn.GetComponent<MinionConstruction>().SetLevel(level);
        }

        //Discard the selected card
        CardManager.Instance.DiscardCard();
        CardSelector.cardSelected = null;
    }


    private void PlaySelectedSpell(Card cardToPlay, int level)
    {
        //Reduce player gold amount
        PlayerStats.Instance.SubtractGold(cardToPlay.costs[level]);

        //Play the selected card's spell
        Spells.Instance.CastSpell(cardToPlay, level, hitObject.transform);

        //Discard the selected card
        CardManager.Instance.DiscardCard();
    }

    private void UpgradeUnit(Card cardToPlay, int level)
    {
        //Reduce player gold amount
        PlayerStats.Instance.SubtractGold(cardToPlay.costs[level]);

        //Increase the selected unit's level
        hitObject.GetComponent<LevelUp>().IncreaseBaseLevel();

        //Discard the selected card
        CardManager.Instance.DiscardCard();
    }


    //Returns true if the selected card can be placed on the selected tile
    private bool validTilePlacement(Card selectedCard, int level)
    {
        Tile selectedTile = hitObject.GetComponent<Tile>();
        int playerGold = (int)PlayerStats.Instance.PlayerGold;

        //Check if the tile is not occupied and player has enough gold to pay for the card
        if (!selectedTile.isOccupied && playerGold >= selectedCard.costs[level])
        {
            //Check if a BUILDING is being played on BUILDING tile
            if (selectedTile.tileType == tileEnum.Building && selectedCard.cardType == CardType.Building)
                return true;
            //Check if a MINON is being played on LANE tile
            else if (selectedTile.tileType == tileEnum.Lane && selectedCard.cardType == CardType.Minion)
                return true;
        }
        return false;
    }


    //Returns true if the selected card can be placed on the selected building/minion
    private bool validSpellPlacement(Card selectedCard, int level)
    {
        int playerGold = (int)PlayerStats.Instance.PlayerGold;

        //Check if player has enough gold to pay for the card
        if (playerGold >= selectedCard.costs[level])
        {
            //Check if a tile is selected, and a spell NOT of type LevelUp is selected
            if(hitObject.GetComponent<Tile>() && selectedCard.spellSubtype != SpellSubtype.LevelUp)
            {
                //Check if an EMERGENCY spell is being played on ANY tile
                if (selectedCard.spellSubtype == SpellSubtype.Emergency)
                    return true;
                //Check if a TILE EFFECT spell is being played on LANE tile
                else if (selectedCard.spellSubtype == SpellSubtype.TileEffect && hitObject.GetComponent<Tile>().tileType == tileEnum.Lane)
                    return true;
            }
            //Check if a level up spell is selected
            else if (selectedCard.spellSubtype == SpellSubtype.LevelUp)
            {
                //Check if the LEVEL UP spell is being played on a BUILDING or MINION
                if (hitObject.GetComponent<Building>() || hitObject.GetComponent<PlayerUnitAI>())
                    return true;
            }
        }

        return false;
    }

    //Returns true if the selected card can be placed on the selected building/minion in order to upgrade it
    private bool validUnitUpgrade(Card selectedCard)
    {
        int playerGold = (int)PlayerStats.Instance.PlayerGold;
        int level = CardSelector.cardSelected.GetComponent<DisplayCard>().level;

        //Check if player has enough gold to pay for the card
        if (playerGold >= selectedCard.costs[level])
        {
            //Check if the selected card is a building and the hit object is a building
            if (selectedCard.cardType == CardType.Building && hitObject.GetComponent<Building>())
            {
                //If the selected card matches the building's card and it is not max level
                if(selectedCard == hitObject.GetComponent<Building>().cardData && 
                    hitObject.GetComponent<LevelUp>().totalLevel < hitObject.GetComponent<LevelUp>().maxLevel)
                    return true;
            }
            //Check if the selected card is a minion and the hit object is a minion
            if (selectedCard.cardType == CardType.Minion && hitObject.GetComponent<PlayerUnitAI>())
            {
                //If the selected card matches the minion's card and it is not max level
                if (selectedCard == hitObject.GetComponent<PlayerUnitAI>().cardData &&
                    hitObject.GetComponent<LevelUp>().totalLevel < hitObject.GetComponent<LevelUp>().maxLevel)
                    return true;
            }
        }

        return false;
    }


    /* CODE FROM WHEN CARD DATA WAS PASSED TO BUILDINGS AND MINIONS
    //Set the card data of the object under construction
    if (newSpawn.GetComponent<BuildingConstruction>())
    {
        newSpawn.GetComponent<BuildingConstruction>().SetCard(cardToPlay);
        newSpawn.GetComponent<BuildingConstruction>().SetLevel(level);
    }
    else if(newSpawn.GetComponent<MinionConstruction>())
    {
        newSpawn.GetComponent<MinionConstruction>().SetCard(cardToPlay);
        newSpawn.GetComponent<MinionConstruction>().SetLevel(level);
    }
    */
}
