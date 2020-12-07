using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnGroup : ScriptableObject
{
    // exists so Wave & Spawn delay can be children of it. Wave stores a wave of enemies. Spawn delay stores a time to delay.
}
