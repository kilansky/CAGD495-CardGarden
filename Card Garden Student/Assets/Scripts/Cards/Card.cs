using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Building, Minion };
public enum BuildingSubtype { Tower, Generator, Utility };

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [HideInInspector] public CardType cardType;
    [HideInInspector] public BuildingSubtype buildingSubtype;
    [HideInInspector] public GameObject thingToSpawn;
    [HideInInspector] public GameObject thingInProgress;
    
    [HideInInspector] public new string name;
    [HideInInspector] public string description;
    [HideInInspector] public Sprite artwork;

    [HideInInspector] public int cost = 0;
    [HideInInspector] public float timeToBuild = 0;
    [HideInInspector] public float attackPower = 0;
    [HideInInspector] public float attackRate = 0;
    [HideInInspector] public float attackRadius = 0;
    [HideInInspector] public float minionHealth = 0;
    [HideInInspector] public float minionArmor = 0;
    [HideInInspector] public float goldGenAmount = 0;
}
