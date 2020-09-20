using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour
{
    [HideInInspector] public Card cardData;

    [HideInInspector] public BuildingSubtype buildingSubtype;
    [HideInInspector] public GameObject rangeSphere;
    [HideInInspector] public GameObject projectile;
    [HideInInspector] public Transform firePoint;

    private float goldGenPerSec;

    private void Start()
    {
        //If TOWER: Set attack radius
        if(buildingSubtype == BuildingSubtype.Tower)
        {
            SetTowerRadius();
        }
        //If GENERATOR: Begin generating gold repeatedly
        else if (buildingSubtype == BuildingSubtype.Generator)
        {
            goldGenPerSec = cardData.goldGenAmount;
            PlayerStats.Instance.AddGoldIncome(goldGenPerSec);
        }
    }

    //Scales the range sphere to the radius of this tower, allowing trigger events with enemies
    public void SetTowerRadius()
    {
        float scaleAmt = cardData.attackRadius;
        rangeSphere.transform.localScale = new Vector3(scaleAmt, scaleAmt, scaleAmt);
    }
}
