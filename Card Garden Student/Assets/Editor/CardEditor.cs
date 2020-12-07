using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(Card))]
public class CardEditor : Editor
{
    private int numLevels = 20;

    private bool costPanel;
    private bool timePanel;
    private bool powerPanel;
    private bool rangePanel;
    private bool ratePanel;
    private bool goldPanel;
    private bool healthPanel;
    private bool durationPanel;

    private bool costStatsPanel;
    private bool timeStatsPanel;
    private bool powerStatsPanel;
    private bool rangeStatsPanel;
    private bool rateStatsPanel;
    private bool goldStatsPanel;
    private bool healthStatsPanel;
    private bool durationStatsPanel;

    //Overrides the default inspector of the Card script
    public override void OnInspectorGUI()
    {
        Card card = (Card)target;

        //if an array has less elements than the number of levels, initialize it
        if (card.costs != null && card.costs.Length < numLevels)
            card.costs = new float[numLevels];
        if (card.buildTimes != null && card.buildTimes.Length < numLevels)
            card.buildTimes = new float[numLevels];
        if (card.attackRanges != null && card.attackRanges.Length < numLevels)
            card.attackRanges = new float[numLevels];
        if (card.attackPowers != null && card.attackPowers.Length < numLevels)
            card.attackPowers = new float[numLevels];
        if (card.attackRates != null && card.attackRates.Length < numLevels)
            card.attackRates = new float[numLevels];
        if (card.goldGenAmounts != null && card.goldGenAmounts.Length < numLevels)
            card.goldGenAmounts = new float[numLevels];
        if (card.minionHealths != null && card.minionHealths.Length < numLevels)
            card.minionHealths = new float[numLevels];
        if (card.spellDurations != null && card.spellDurations.Length < numLevels)
            card.spellDurations = new float[numLevels];

        //----------------Card Type & Prefabs----------------
        EditorGUILayout.LabelField("Card Type & Prefabs", EditorStyles.boldLabel);
        card.seasonType = (SeasonTypes)EditorGUILayout.ObjectField("Season", card.seasonType, typeof(SeasonTypes), true);
        card.cardType = (CardType)EditorGUILayout.EnumPopup("Card Type", card.cardType);

        //Building Exclusive values
        if (card.cardType == CardType.Building)
        {
            card.buildingSubtype = (BuildingSubtype)EditorGUILayout.EnumPopup("Building Subtype", card.buildingSubtype);
            card.thingToSpawn = (GameObject)EditorGUILayout.ObjectField("Building", card.thingToSpawn, typeof(GameObject), true);
            card.thingInProgress = (GameObject)EditorGUILayout.ObjectField("Buiding In Progress", card.thingInProgress, typeof(GameObject), true);
        }
        //Minion Exclusive values
        else if (card.cardType == CardType.Minion)
        {
            card.minionSubtype = (MinionSubtype)EditorGUILayout.EnumPopup("Minion Subtype", card.minionSubtype);
            card.thingToSpawn = (GameObject)EditorGUILayout.ObjectField("Minion", card.thingToSpawn, typeof(GameObject), true);
            card.thingInProgress = (GameObject)EditorGUILayout.ObjectField("Minion In Progress", card.thingInProgress, typeof(GameObject), true);
        }
        //Spell Exclusive values
        else if (card.cardType == CardType.Spell)
        {
            card.spellSubtype = (SpellSubtype)EditorGUILayout.EnumPopup("Spell Subtype", card.spellSubtype);
            card.spell = (Spell)EditorGUILayout.EnumPopup("Spell", card.spell);
            card.thingToSpawn = (GameObject)EditorGUILayout.ObjectField("Spell Effect", card.thingToSpawn, typeof(GameObject), true);
        }

        //----------------Card Text & Visuals----------------
        EditorGUILayout.LabelField("Text & Visuals", EditorStyles.boldLabel);
        card.name = EditorGUILayout.TextField("Name", card.name);
        card.description = (string)EditorGUILayout.TextField("Description", card.description);
        card.artwork = (Sprite)EditorGUILayout.ObjectField("Sprite", card.artwork, typeof(Sprite), allowSceneObjects: true);

        //----------------Card Stats----------------
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);

        costPanel = EditorGUILayout.Foldout(costPanel, "Costs", true);
        DisplayAllStats(card.costStats, "Cost", costPanel);

        //Building Exclusive Stats
        if (card.cardType == CardType.Building)
        {
            timePanel = EditorGUILayout.Foldout(timePanel, "Times To Build", true);
            DisplayAllStats(card.buildTimeStats, "Time To Build", timePanel);

            //Tower Exclusive Stats
            if (card.buildingSubtype == BuildingSubtype.Tower)
            {
                powerPanel = EditorGUILayout.Foldout(powerPanel, "Attack Powers", true);
                DisplayAllStats(card.attackPowerStats, "Attack Power", powerPanel);

                rangePanel = EditorGUILayout.Foldout(rangePanel, "Attack Ranges", true);
                DisplayAllStats(card.attackRangeStats, "Attack Ranges", rangePanel);

                ratePanel = EditorGUILayout.Foldout(ratePanel, "Attack Rates", true);
                DisplayDecreasingStats(card.attackRateStats, "Attack Rate", ratePanel);
                card.minAttackRate = EditorGUILayout.FloatField("Min Attack Rate", card.minAttackRate);
            }
            //Generator Exclusive Stats
            else if (card.buildingSubtype == BuildingSubtype.Generator)
            {
                goldPanel = EditorGUILayout.Foldout(goldPanel, "Gold Gen Amounts", true);
                DisplayAllStats(card.goldGenAmountStats, "Gold Gen Amount", goldPanel);
            }
            else //Utility Exclusive Stats
            {
                powerPanel = EditorGUILayout.Foldout(powerPanel, "Attack Powers", true);
                DisplayAllStats(card.attackPowerStats, "Attack Power", powerPanel);

                //Range = Projectile Count
                rangePanel = EditorGUILayout.Foldout(rangePanel, "Projectile Counts", true);
                DisplayAllStats(card.attackRangeStats, "Projectile Count", rangePanel);
            }
        }

        //Minion Exclusive Stats
        if (card.cardType == CardType.Minion)
        {
            timePanel = EditorGUILayout.Foldout(timePanel, "Times To Spawn", true);
            DisplayAllStats(card.buildTimeStats, "Time To Spawn", timePanel);

            healthPanel = EditorGUILayout.Foldout(healthPanel, "Minion Healths", true);
            DisplayAllStats(card.minionHealthStats, "Minion Health", healthPanel);

            powerPanel = EditorGUILayout.Foldout(powerPanel, "Attack Powers", true);
            DisplayAllStats(card.attackPowerStats, "Attack Power", powerPanel);

            rangePanel = EditorGUILayout.Foldout(rangePanel, "Attack Ranges", true);
            DisplayAllStats(card.attackRangeStats, "Attack Ranges", rangePanel);

            ratePanel = EditorGUILayout.Foldout(ratePanel, "Attack Rates", true);
            DisplayDecreasingStats(card.attackRateStats, "Attack Rate", ratePanel);
            card.minAttackRate = EditorGUILayout.FloatField("Min Attack Rate", card.minAttackRate);
        }

        if (card.cardType == CardType.Spell)
        {
            timePanel = EditorGUILayout.Foldout(timePanel, "Casting Times", true);
            DisplayAllStats(card.buildTimeStats, "Casting Time", timePanel);

            if(card.spellSubtype == SpellSubtype.Emergency)
            {
                powerPanel = EditorGUILayout.Foldout(powerPanel, "Attack Powers", true);
                DisplayAllStats(card.attackPowerStats, "Attack Power", powerPanel);

                ratePanel = EditorGUILayout.Foldout(ratePanel, "Attack Rates", true);
                DisplayBaseStatOnly(card.attackRateStats, "Attack Rate", ratePanel);
            }
            else if (card.spellSubtype == SpellSubtype.TileEffect)
            {
                powerPanel = EditorGUILayout.Foldout(powerPanel, "Damages Per Tick", true);
                DisplayAllStats(card.attackPowerStats, "Damage Per Tick", powerPanel);

                ratePanel = EditorGUILayout.Foldout(ratePanel, "Tick Rates", true);
                DisplayDecreasingStats(card.attackRateStats, "Tick Rate", ratePanel);
                card.minAttackRate = EditorGUILayout.FloatField("Min Tick Rate", card.minAttackRate);

                durationPanel = EditorGUILayout.Foldout(durationPanel, "Spell Durations", true);
                DisplayAllStats(card.spellDurationStats, "Spell Duration", durationPanel);
            }
            else //LevelUp Subtype
            {

            }
        }

        //----------------READONLY Card Stats----------------
        EditorGUILayout.LabelField("-----------------READONLY Stats-----------------", EditorStyles.boldLabel);

        //EditorGUILayout.PropertyField(serializedObject.FindProperty("costs"));
        costStatsPanel = EditorGUILayout.Foldout(costStatsPanel, "Cost Stats", true);
        DisplayReadOnlyStats(card.costs, "Cost", costStatsPanel);

        if (card.cardType == CardType.Building)
        {
            timeStatsPanel = EditorGUILayout.Foldout(timeStatsPanel, "Build Time Stats", true);
            DisplayReadOnlyStats(card.buildTimes, "Time", timeStatsPanel);

            if (card.buildingSubtype == BuildingSubtype.Tower)
            {
                powerStatsPanel = EditorGUILayout.Foldout(powerStatsPanel, "Attack Power Stats", true);
                DisplayReadOnlyStats(card.attackPowers, "Power", powerStatsPanel);

                rangeStatsPanel = EditorGUILayout.Foldout(rangeStatsPanel, "Attack Range Stats", true);
                DisplayReadOnlyStats(card.attackRanges, "Range", rangeStatsPanel);

                rateStatsPanel = EditorGUILayout.Foldout(rateStatsPanel, "Attack Rate Stats", true);
                DisplayReadOnlyStats(card.attackRates, "Rate", rateStatsPanel);
            }

            if (card.buildingSubtype == BuildingSubtype.Generator)
            {
                goldStatsPanel = EditorGUILayout.Foldout(goldStatsPanel, "Gold Gen Stats", true);
                DisplayReadOnlyStats(card.goldGenAmounts, "Gold Gen", goldStatsPanel);
            }

            if (card.buildingSubtype == BuildingSubtype.Utility)
            {
                powerStatsPanel = EditorGUILayout.Foldout(powerStatsPanel, "Attack Power Stats", true);
                DisplayReadOnlyStats(card.attackPowers, "Power", powerStatsPanel);

                rangeStatsPanel = EditorGUILayout.Foldout(rangeStatsPanel, "Projectile Count Stats", true);
                DisplayReadOnlyStats(card.attackRanges, "Count", rangeStatsPanel);
            }
        }
        else if(card.cardType == CardType.Minion)
        {
            timeStatsPanel = EditorGUILayout.Foldout(timeStatsPanel, "Spawn Time Stats", true);
            DisplayReadOnlyStats(card.buildTimes, "Time", timeStatsPanel);

            healthStatsPanel = EditorGUILayout.Foldout(healthStatsPanel, "Health Stats", true);
            DisplayReadOnlyStats(card.minionHealths, "Health", healthStatsPanel);

            powerStatsPanel = EditorGUILayout.Foldout(powerStatsPanel, "Attack Power Stats", true);
            DisplayReadOnlyStats(card.attackPowers, "Power", powerStatsPanel);

            rangeStatsPanel = EditorGUILayout.Foldout(rangeStatsPanel, "Attack Range Stats", true);
            DisplayReadOnlyStats(card.attackRanges, "Range", rangeStatsPanel);

            rateStatsPanel = EditorGUILayout.Foldout(rateStatsPanel, "Attack Rate Stats", true);
            DisplayReadOnlyStats(card.attackRates, "Rate", rateStatsPanel);
        }
        else //Spell Cards
        {
            timeStatsPanel = EditorGUILayout.Foldout(timeStatsPanel, "Casting Times Stats", true);
            DisplayReadOnlyStats(card.buildTimes, "Time", timeStatsPanel);

            if (card.spellSubtype != SpellSubtype.LevelUp)
            {
                powerStatsPanel = EditorGUILayout.Foldout(powerStatsPanel, "Attack Power Stats", true);
                DisplayReadOnlyStats(card.attackPowers, "Power", powerStatsPanel);

                rateStatsPanel = EditorGUILayout.Foldout(rateStatsPanel, "Attack Rate Stats", true);
                DisplayReadOnlyStats(card.attackRates, "Rate", rateStatsPanel);
            }

            if (card.spellSubtype == SpellSubtype.TileEffect)
            {
                durationStatsPanel = EditorGUILayout.Foldout(durationStatsPanel, "Spell Duration Stats", true);
                DisplayReadOnlyStats(card.spellDurations, "Duration", durationStatsPanel);
            }
        }

        //base.OnInspectorGUI();

        //Save any changes to this Scriptable Object
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(card);
            AssetDatabase.SaveAssets();
            card.CalculateAllStats();
            Debug.Log(card.name + " Saved");
        }
    }

    //Add the base, growth, and progression variables for a Stat which increases over levels
    private void DisplayAllStats(CardStats stats, string name, bool panel)
    {
        if (panel)
        {
            stats.baseValue = EditorGUILayout.FloatField("Base Value", stats.baseValue);
            stats.growthValue = EditorGUILayout.FloatField("Growth Value", stats.growthValue);
            stats.progressionType = (ProgressionType)EditorGUILayout.EnumPopup("Progression Type", stats.progressionType);
        }
    }

    //Add the base, growth, and progression variables for a Stat which decreases over levels
    private void DisplayDecreasingStats(CardStats stats, string name, bool panel)
    {
        if (panel)
        {
            stats.baseValue = EditorGUILayout.FloatField("Base Value", stats.baseValue);
            stats.growthValue = EditorGUILayout.FloatField("Decrease Value", stats.growthValue);
            stats.progressionType = (ProgressionType)EditorGUILayout.EnumPopup("Progression Type", stats.progressionType);
        }
    }

    //Add the base, growth, and progression variables for a Stat which decreases over levels
    private void DisplayBaseStatOnly(CardStats stats, string name, bool panel)
    {
        if (panel)
            stats.baseValue = EditorGUILayout.FloatField("Base Value", stats.baseValue);

        stats.growthValue = 0;
        stats.progressionType = ProgressionType.Full;
    }

    //Add the base, growth, and progression variables for a Stat which increases over levels
    private void DisplayReadOnlyStats(float[] stats, string name, bool panel)
    {
        if (panel)
        {
            EditorGUI.BeginDisabledGroup(true);
            for (int i = 0; i < 6; i++)
                EditorGUILayout.FloatField(name + (i+1) + ": ", stats[i]);

            EditorGUI.EndDisabledGroup();
        }
    }
}
