﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    //Overrides the default inspector of the Card script
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Card card = (Card)target;

        //Card Type & Prefabs
        EditorGUILayout.LabelField("Card Type & Prefabs", EditorStyles.boldLabel);
        card.cardType = (CardType)EditorGUILayout.EnumPopup("Card Type", card.cardType);

        //Building Exclusive values
        if (card.cardType == CardType.Building)
        {
            card.buildingSubtype = (BuildingSubtype)EditorGUILayout.EnumPopup("Building Subtype", card.buildingSubtype);
            card.thingToSpawn = (GameObject)EditorGUILayout.ObjectField("Building", card.thingToSpawn, typeof(GameObject), true);
            card.buildingInProgress = (GameObject)EditorGUILayout.ObjectField("Buiding In Progress", card.buildingInProgress, typeof(GameObject), true);
        }
        //Minion Exclusive values
        else if (card.cardType == CardType.Minion)
            card.thingToSpawn = (GameObject)EditorGUILayout.ObjectField("Minion", card.thingToSpawn, typeof(GameObject), true);

        //Card Text & Visuals
        EditorGUILayout.LabelField("Text & Visuals", EditorStyles.boldLabel);
        card.name = EditorGUILayout.TextField("Name", card.name);
        card.description = (string)EditorGUILayout.TextField("Description", card.description);
        card.artwork = (Sprite)EditorGUILayout.ObjectField("Sprite", card.artwork, typeof(Sprite), allowSceneObjects: true);

        //Card Stats
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        card.cost = EditorGUILayout.IntField("Cost", card.cost);

        //Building Exclusive Stats
        if (card.cardType == CardType.Building)
        {
            card.timeToBuild = EditorGUILayout.FloatField("Time To Build", card.timeToBuild);

            //Tower Exclusive Stats
            if (card.buildingSubtype == BuildingSubtype.Tower)
            {              
                card.attackRadius = EditorGUILayout.FloatField("Attack Radius", card.attackRadius);
                card.attackPower = EditorGUILayout.FloatField("Attack Power", card.attackPower);
                card.attackRate = EditorGUILayout.FloatField("Attack Speed", card.attackRate);
            }
            //Generator Exclusive Stats
            else if (card.buildingSubtype == BuildingSubtype.Generator)
                card.goldGenPerSec = EditorGUILayout.FloatField("Gold Per Sec", card.goldGenPerSec);
        }

        //Minion Exclusive Stats
        if (card.cardType == CardType.Minion)
        {
            card.minionHealth = EditorGUILayout.FloatField("Minion Health", card.minionHealth);
            card.minionArmor = EditorGUILayout.FloatField("Minion Armor", card.minionArmor);
            card.attackPower = EditorGUILayout.FloatField("Attack Power", card.attackPower);
            card.attackRate = EditorGUILayout.FloatField("Attack Speed", card.attackRate);
        }
    }
}
