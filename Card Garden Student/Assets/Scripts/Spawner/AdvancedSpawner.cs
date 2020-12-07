using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lister : MonoBehaviour
{
    public PathList ListOfPathLists = new PathList();
}

[System.Serializable]
public class Path
{
    public List<Transform> list;
}

[System.Serializable]
public class PathList
{
    public List<Path> list;
}


public class AdvancedSpawner : MonoBehaviour
{
    public Transform spawnLocation;
    public List<Path> paths = new List<Path>();
    public List<Encounter> encounters = new List<Encounter>();

    [Header("Encounter")]
    public Encounter activeEncounter;
    public int currentEncounter = 0;
    public int totalEncounters = 0;

    [Header("Wave")]
    public SpawnGroup activeWave;
    public int currentWave = 0;
    public int totalWaves = 0;
    public int currentPath = 0;
    public int currentLevel = 0;

    [Header("Spawn")]
    public GameObject activeSpawn;
    public int currentSpawn = 0;
    public int totalSpawns = 0;

    [Header("Timer")]
    public float currentTimeDelay = 0;
    public bool isTimer = false;
    public bool requiresInput = false;

    [Header("Completion")]
    public int lairNumber;
    public bool allDone = false;

    public string spawnName;

    private void Start()
    {
        ReadEncounter(); // for ui
    }

    private void Update()
    {
        if (!allDone)
        {
            currentTimeDelay -= Time.deltaTime;
            if (currentTimeDelay < 0)
            {
                SpawnEnemy();
            }
        }
    }

    // Goes to next enemy or timer. Should update it with a function that
    // cares about the timer and then actually spawns things instead of debug log
    public void SpawnEnemy()
    {
        if (!isTimer)
        {
            GameObject enemyObject = Instantiate(activeSpawn, spawnLocation.position, transform.rotation, null);
            enemyObject.GetComponent<EnemyUnitAI>().destinations = paths[currentPath].list;
            enemyObject.GetComponent<EnemyUnitAI>().startingLevel = currentLevel;

            NextSpawn();
            ReadEncounter();
        }
        else
        {
            NextSpawn();
            ReadEncounter();
        }

    }

    // updates info
    public void ReadEncounter()
    {
        // Sets the active Encounter to this encounter.
        activeEncounter = encounters[currentEncounter];
        totalEncounters = encounters.Count;
        totalWaves = encounters[currentEncounter].Waves.Count;

        if (encounters[currentEncounter].Waves[currentWave] is SpawnDelay) // If the current "Wave" is a delay, sets the value of delay "CurrentTimeDelay" and isTimer to true
        {
            SpawnDelay delay = (SpawnDelay)encounters[currentEncounter].Waves[currentWave];
            currentTimeDelay = delay.timeToDelay;
            requiresInput = delay.requiresPlayerInput;
            spawnName = "Spawn delay " + delay.timeToDelay + "," + requiresInput;
            isTimer = true;
            totalWaves = encounters[currentEncounter].Waves.Count;
        }
        else if (encounters[currentEncounter].Waves[currentWave] is Wave) // If the current Wave is a wave, it finds how many waves & spawns there are and then the spawns.
        {
            Wave group = (Wave)encounters[currentEncounter].Waves[currentWave];
            activeWave = group;
            totalWaves = encounters[currentEncounter].Waves.Count;
            totalSpawns = group.enemiesToSpawn.Count;
            activeSpawn = group.enemiesToSpawn[currentSpawn];

            // ensure the startLevels range is correct
            if (currentSpawn < group.startLevels.Count && currentSpawn >= 0)
                currentLevel = group.startLevels[currentSpawn];
            else
            {
                currentLevel = 1;
                Debug.LogWarning("Out of range error in Wave scriptable object." +
                    " Forcing " + activeSpawn + " to level 1.");
            }

            // if level is greater than 20 or less than 1
            if (currentLevel > 20 || currentLevel < 1)
            {
                currentLevel = 1;
                Debug.LogWarning("Level out of range (must be 1-20), in Wave" + 
                    " scriptable object. Forcing " + activeSpawn + " to level 1.");
            }
            
            // ensures no path selected is larger than a path index possible
            if (currentPath < paths.Count)
                currentPath = group.pathIndices[currentSpawn];
            else
            {
                currentPath = 0;
                Debug.LogWarning("Path index out of range for enemy: " + activeSpawn +
                    ". Forcing path to index 0.");
            }

            spawnName = activeSpawn.name; 
            currentTimeDelay = group.timeBetweenSpawns;
            isTimer = false;
        }
    }

    // 3 functions to act as progression, occur automatically when beyond the scope of their relevant index in the scriptable objects
    private void NextEncounter()
    {
        if (currentEncounter < totalEncounters -1)
        {
            currentEncounter += 1;
			if (gameObject.GetComponent<Save>())
				gameObject.GetComponent<Save>().quickSave();
            currentWave = 0;
            CardManager.Instance.ResetDeck();
            CardManager.Instance.DrawCard();
            CardManager.Instance.DrawCard();
            CardManager.Instance.DrawCard();
            // MenuUI.Instance.PauseGame();
        }
        else
        {
            FinishedSpawning();
        }
    }

    private void NextWave()
    {
        currentSpawn = 0;
        if (currentWave < totalWaves - 1)
        {
            currentWave += 1;
        }
        else
        {
            currentWave = 1;
            NextEncounter();
        }
    }
    private void NextSpawn()
    {
        if (isTimer)
        {
            currentSpawn = 0;
            NextWave();
        }
        else
        {
            if (currentSpawn < totalSpawns - 1)
            {
                currentSpawn += 1;
            }
            else
            {
                currentSpawn = 0;
                NextWave();
            }
        }
    }

    private void FinishedSpawning()
    {
        allDone = true;
        //When the last wave has spawned, begin checking that all enemies have been defeated
        InvokeRepeating(nameof(CheckForGameEnd), 1, 1);
    }

    private void CheckForGameEnd()
    {
        //Get all enemies in the scene
        EnemyUnitAI[] enemies = FindObjectsOfType<EnemyUnitAI>();

        //If all enemies are dead, display the win screen and cancel the repeating invoke of this function
        if (enemies.Length == 0)
        {
            MenuUI.Instance.OpenPanel(1);
            CancelInvoke(nameof(CheckForGameEnd));
        }
    }

    private void OnDrawGizmos()
    {
        Color[] colors = { Color.blue, Color.red, Color.green, Color.yellow, Color.magenta };
        for (int i=0; i < paths.Count; ++i)
        {
            Color c = colors[i % colors.Length];
            Gizmos.color = c;
            for (int j=0; j < paths[i].list.Count; ++j)
            {
                Gizmos.DrawSphere(paths[i].list[j].position, 0.5f);
            }
        }
    }
}
