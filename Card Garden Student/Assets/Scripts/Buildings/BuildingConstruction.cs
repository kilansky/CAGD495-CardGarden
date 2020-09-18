using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingConstruction : MonoBehaviour
{
    public TextMeshProUGUI buildTimeText;

    private Card cardData;
    private float timeRemaining;

    public void SetCard(Card _cardData) {cardData = _cardData;}

    void Start()
    {
        StartCoroutine(BuildBuilding());
    }

    private IEnumerator BuildBuilding()
    {
        //Wait for timeToBuild, check again each half second
        for (float i = 0; i < cardData.timeToBuild; i+=0.5f)
        {
            timeRemaining = cardData.timeToBuild - i;
            if(timeRemaining % 1 == 0)
                buildTimeText.text = timeRemaining.ToString();

            yield return new WaitForSeconds(0.5f);
        }
            

        //Spawn the building and destroy self
        GameObject newBuilding = Instantiate(cardData.thingToSpawn, transform.position, transform.rotation);
        newBuilding.GetComponent<Building>().cardData = cardData;
        Destroy(gameObject);
    }
}
