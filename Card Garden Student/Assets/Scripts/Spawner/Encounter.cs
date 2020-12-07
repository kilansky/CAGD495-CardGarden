using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Encounter", menuName = "Spawner/New Encounter")]
public class Encounter : ScriptableObject
{
    // put me into the advanced spawner
    public List<SpawnGroup> Waves = new List<SpawnGroup>();
}
