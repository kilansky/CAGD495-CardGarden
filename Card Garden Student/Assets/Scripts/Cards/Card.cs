using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { building, minion };

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public CardType cardType;

    public GameObject thingToSpawn;

    public new string name;
    [TextArea] public string description;
    [Range(0, 99)] public int cost;
    public Sprite artwork;
  
    public float attackPower;
    public float attackSpeed;
    [HideInInspector] public float buildingRange;
    [HideInInspector] public float minionHealth;
}
