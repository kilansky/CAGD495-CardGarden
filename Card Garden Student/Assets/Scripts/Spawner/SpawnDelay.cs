using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spawn Delay", menuName = "Spawner/New Spawn Delay")]
public class SpawnDelay : SpawnGroup
{
    // put me into an encounter
    public float timeToDelay;
    public bool requiresPlayerInput = false;
}
