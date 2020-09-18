using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionConstruction : MonoBehaviour
{
    private Card cardData;

    public void SetCard(Card _cardData) { cardData = _cardData; }

    void Start()
    {
        StartCoroutine(SpawnMinion());
    }

    private IEnumerator SpawnMinion()
    {
        //Wait for timeToBuild, check again each half second
        for (float i = 0; i < cardData.timeToBuild; i += 0.5f)
            yield return new WaitForSeconds(0.5f);

        //Spawn the minion and destroy self
        GameObject newMinion = Instantiate(cardData.thingToSpawn, transform.position, transform.rotation);
        //newMinion.GetComponent<Building>().cardData = cardData;
        Destroy(gameObject);
    }
}
