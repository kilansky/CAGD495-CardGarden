using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Card card = (Card)target;

        if (card.cardType == CardType.Building)
        {
            card.buildingRange = EditorGUILayout.FloatField("Building Range", card.buildingRange);
        }
        else if (card.cardType == CardType.Minion)
        {
            card.minionHealth = EditorGUILayout.FloatField("Minion Health", card.minionHealth);
        }
    }
}
