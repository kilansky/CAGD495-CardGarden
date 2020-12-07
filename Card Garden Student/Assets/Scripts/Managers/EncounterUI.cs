using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EncounterUI : SingletonPattern<EncounterUI>
{
    public AdvancedSpawner SpawnerRef;

    public Text encounterNumbers;
    public Text delayTimer;
    public Text encounterName;

    private void Update()
    {
        RefreshEncounterUI();
    }
    private void RefreshEncounterUI()
    {
        encounterNumbers.text = "Encounter " + (SpawnerRef.currentEncounter + 1);
        delayTimer.text = "Next Enemy " + SpawnerRef.currentTimeDelay.ToString("0.0");
        encounterName.text = SpawnerRef.spawnName;
    }

}
