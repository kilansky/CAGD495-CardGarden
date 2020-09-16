using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [HideInInspector] public BuildingSubtype buildingSubtype;
    [HideInInspector] public GameObject rangeSphere;
    [HideInInspector] public GameObject projectile;
    [HideInInspector] public Transform firePoint;
    [HideInInspector] public Card cardData;

    private void Start()
    {
        float scaleAmt = cardData.attackRadius * 2;
        rangeSphere.transform.localScale = new Vector3(scaleAmt, scaleAmt, scaleAmt);
    }
}
