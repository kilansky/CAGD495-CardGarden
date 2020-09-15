using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public float timeBetweenSpawns;
    public Vector3 spawnerOffset; // to spawn a little above tile

    private Tile tile;
    private Vector3 spawnPosition;
    private float timer;

    void Start()
    {
        timer = -1;
        spawnPosition = transform.position + spawnerOffset;
        tile = gameObject.GetComponent<Tile>();

        // some function may call this whenever enemies should spawn
        BeginSpawning();
    }

    void Update()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, timeBetweenSpawns); 
        if (timer == 0 && enemiesToSpawn.Count > 0 && tile.occupant == null)
        {
            tile.occupant = enemiesToSpawn[0];
            Instantiate(enemiesToSpawn[0], spawnPosition, Quaternion.identity);
            enemiesToSpawn.RemoveAt(0);
            timer = timeBetweenSpawns;
        }
    }

    public void BeginSpawning()
    {
        timer = timeBetweenSpawns;
    }
}