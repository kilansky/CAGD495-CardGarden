using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public enum CardType {building, minion};
    public CardType cardType;

    public new string name;

    public int cost;

    public float attackPower;

    [HideInInspector] public float buildingRange;
    [HideInInspector] public float minionHealth;


    void Start()
    {

    }
}
