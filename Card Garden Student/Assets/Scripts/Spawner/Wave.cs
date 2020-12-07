using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Spawner/New Wave")]
public class Wave : SpawnGroup
{
    [Header("Enemy GameObjects")]
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    [Header("Which Path Enemy Will Follow")]
    public List<int> pathIndices = new List<int>();

    [Header("Starting Level of Enemy")]
    public List<int> startLevels = new List<int>();

    public float timeBetweenSpawns;
}
