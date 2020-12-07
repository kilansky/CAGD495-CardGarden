using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private int numLevels = 20;

    private bool speedPanel;
    private bool healthPanel;
    private bool powerPanel;
    private bool rangePanel;
    private bool ratePanel;
    private bool expPanel;
    private bool goldPanel;

    private bool speedStatsPanel;
    private bool healthStatsPanel;
    private bool powerStatsPanel;
    private bool rangeStatsPanel;
    private bool rateStatsPanel;
    private bool expStatsPanel;
    private bool goldStatsPanel;

    //Overrides the default inspector of the Card script
    public override void OnInspectorGUI()
    {
        Enemy enemy = (Enemy)target;

        //if an array has less elements than the number of levels, initialize it
        if (enemy.movementSpeeds != null && enemy.movementSpeeds.Length < numLevels)
            enemy.movementSpeeds = new float[numLevels];
        if (enemy.maxHealths != null && enemy.maxHealths.Length < numLevels)
            enemy.maxHealths = new float[numLevels];
        if (enemy.attackPowers != null && enemy.attackPowers.Length < numLevels)
            enemy.attackPowers = new float[numLevels];
        if (enemy.attackRanges != null && enemy.attackRanges.Length < numLevels)
            enemy.attackRanges = new float[numLevels];
        if (enemy.attackRates != null && enemy.attackRates.Length < numLevels)
            enemy.attackRates = new float[numLevels];
        if (enemy.expValues != null && enemy.expValues.Length < numLevels)
            enemy.expValues = new float[numLevels];
        if (enemy.goldDropped != null && enemy.goldDropped.Length < numLevels)
            enemy.goldDropped = new float[numLevels];

        //----------------Enemy Type & Prefabs----------------
        EditorGUILayout.LabelField("Enemy Type & Prefabs", EditorStyles.boldLabel);
        enemy.seasonType = (SeasonTypes)EditorGUILayout.ObjectField("Season", enemy.seasonType, typeof(SeasonTypes), true);
        enemy.enemyClass = (EnemyClass)EditorGUILayout.EnumPopup("Enemy Class", enemy.enemyClass);
        enemy.projectilePrefab = (GameObject)EditorGUILayout.ObjectField("Projectile", enemy.projectilePrefab, typeof(GameObject), true);

        //----------------Card Stats----------------
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);

        speedPanel = EditorGUILayout.Foldout(speedPanel, "Movement Speeds", true);
        DisplayAllStats(enemy.movementSpeedStats, "Movement Speed", speedPanel);

        healthPanel = EditorGUILayout.Foldout(healthPanel, "Max Healths", true);
        DisplayAllStats(enemy.maxHealthStats, "Max Health", healthPanel);

        powerPanel = EditorGUILayout.Foldout(powerPanel, "Attack Powers", true);
        DisplayAllStats(enemy.attackPowerStats, "Attack Power", powerPanel);

        rangePanel = EditorGUILayout.Foldout(rangePanel, "Attack Ranges", true);
        DisplayAllStats(enemy.attackRangeStats, "Attack Ranges", rangePanel);

        ratePanel = EditorGUILayout.Foldout(ratePanel, "Attack Rates", true);
        DisplayDecreasingStats(enemy.attackRateStats, "Attack Rate", ratePanel);
        enemy.minAttackRate = EditorGUILayout.FloatField("Min Attack Rate", enemy.minAttackRate);

        expPanel = EditorGUILayout.Foldout(expPanel, "Exp Dropped", true);
        DisplayAllStats(enemy.expValueStats, "Exp Dropped", expPanel);

        goldPanel = EditorGUILayout.Foldout(goldPanel, "Gold Dropped", true);
        DisplayAllStats(enemy.goldDroppedStats, "Gold Dropped", goldPanel);

        //----------------READONLY Card Stats----------------
        EditorGUILayout.LabelField("-----------------READONLY Stats-----------------", EditorStyles.boldLabel);

        speedStatsPanel = EditorGUILayout.Foldout(speedStatsPanel, "Movement Speed Stats", true);
        DisplayReadOnlyStats(enemy.movementSpeeds, "Speed", speedStatsPanel);

        healthStatsPanel = EditorGUILayout.Foldout(healthStatsPanel, "Max Health Stats", true);
        DisplayReadOnlyStats(enemy.maxHealths, "Health", healthStatsPanel);

        powerStatsPanel = EditorGUILayout.Foldout(powerStatsPanel, "Attack Power Stats", true);
        DisplayReadOnlyStats(enemy.attackPowers, "Power", powerStatsPanel);

        rangeStatsPanel = EditorGUILayout.Foldout(rangeStatsPanel, "Attack Range Stats", true);
        DisplayReadOnlyStats(enemy.attackRanges, "Range", rangeStatsPanel);

        rateStatsPanel = EditorGUILayout.Foldout(rateStatsPanel, "Attack Rate Stats", true);
        DisplayReadOnlyStats(enemy.attackRates, "Rate", rateStatsPanel);

        expStatsPanel = EditorGUILayout.Foldout(expStatsPanel, "Exp Dropped Stats", true);
        DisplayReadOnlyStats(enemy.expValues, "Exp", expStatsPanel);

        goldStatsPanel = EditorGUILayout.Foldout(goldStatsPanel, "Gold Dropped Stats", true);
        DisplayReadOnlyStats(enemy.goldDropped, "Gold", goldStatsPanel);

        //base.OnInspectorGUI();

        //Save any changes to this Scriptable Object
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(enemy);
            AssetDatabase.SaveAssets();
            enemy.CalculateAllStats();
            Debug.Log(enemy.name + " Saved");
        }
    }

    //Add the base, growth, and progression variables for a Stat which increases over levels
    private void DisplayAllStats(EnemyStats stats, string name, bool panel)
    {
        if (panel)
        {
            stats.baseValue = EditorGUILayout.FloatField("Base Value", stats.baseValue);
            stats.growthValue = EditorGUILayout.FloatField("Growth Value", stats.growthValue);
            stats.progressionType = (ProgressionType)EditorGUILayout.EnumPopup("Progression Type", stats.progressionType);
        }
    }

    //Add the base, growth, and progression variables for a Stat which decreases over levels
    private void DisplayDecreasingStats(EnemyStats stats, string name, bool panel)
    {
        if (panel)
        {
            stats.baseValue = EditorGUILayout.FloatField("Base Value", stats.baseValue);
            stats.growthValue = EditorGUILayout.FloatField("Decrease Value", stats.growthValue);
            stats.progressionType = (ProgressionType)EditorGUILayout.EnumPopup("Progression Type", stats.progressionType);
        }
    }

    //Add the base, growth, and progression variables for a Stat which decreases over levels
    private void DisplayBaseStatOnly(EnemyStats stats, string name, bool panel)
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
                EditorGUILayout.FloatField(name + (i + 1) + ": ", stats[i]);

            EditorGUI.EndDisabledGroup();
        }
    }
}
