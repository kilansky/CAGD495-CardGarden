using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FreezeTile : MonoBehaviour
{
    public float freezeDuration; // how long enemy will be frozen
    public float startLifeTimer; // how many seconds tile effect will live
    float lifeTimer;
    readonly List<EnemyUnitAI> enemies = new List<EnemyUnitAI>();

    void Start()
    {
        lifeTimer = startLifeTimer;
    }

    private void OnDestroy()
    {
        // give the original speed back to enemies
        // if the effect died before the enemies are thawed
        foreach (var e in enemies)
        {
            if (e != null) e.Unfreeze();
        }
    }

    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0)
            Destroy(transform.gameObject);
    }

    // freeze the enemy for a specified amt of time
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyUnitAI e = other.gameObject.GetComponent<EnemyUnitAI>();
            e.Freeze(freezeDuration);
            enemies.Add(e);
        }
    }

    // one the enemy moves out of the freeze tile, release it from the list
    private void OnTriggerExit(Collider other)
    {
        EnemyUnitAI e = other.gameObject.GetComponent<EnemyUnitAI>();
        enemies.Remove(e);
    }
}










