using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Building, Minion };

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Build Properties")]
    public CardType cardType;
    public GameObject thingToSpawn;
    public GameObject buildingInProgress;
    public float timeToBuild;

    [Header("Text & Visuals")]
    public new string name;
    [TextArea] public string description;   
    public Sprite artwork;

    [Header("Stats")]
    [Range(0, 99)] public int cost;
    public float attackPower;
    public float attackSpeed;
    [HideInInspector] public float buildingRange;
    [HideInInspector] public float minionHealth;
}
