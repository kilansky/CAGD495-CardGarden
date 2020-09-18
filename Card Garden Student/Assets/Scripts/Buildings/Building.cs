using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building : MonoBehaviour
{
    [HideInInspector] [SerializeField] public Card cardData;

    [HideInInspector] [SerializeField] public BuildingSubtype buildingSubtype;
    [HideInInspector] [SerializeField] public GameObject rangeSphere;
    [HideInInspector] [SerializeField] public GameObject projectile;
    [HideInInspector] [SerializeField] public Transform firePoint;
    //[HideInInspector] [SerializeField] public float goldGenRate;

    private void Start()
    {
        if(buildingSubtype == BuildingSubtype.Tower)
        {
            float scaleAmt = cardData.attackRadius;
            rangeSphere.transform.localScale = new Vector3(scaleAmt, scaleAmt, scaleAmt);
        }
    }
}
