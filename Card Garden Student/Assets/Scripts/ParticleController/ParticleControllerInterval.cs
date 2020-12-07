using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns particles in specicied intervals, rather than once.
public class ParticleControllerInterval : MonoBehaviour
{
    // spawns the particles between these two values
    public float spawnIntervalMin;
    public float spawnIntervalMax;

    private ParticleSystem[] particleSystems;
    private float timer;

    private void Start()
    {
        timer = 0;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Activate();
            timer = Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }

    private void Activate()
    {
        foreach (ParticleSystem ps in particleSystems)
            ps.Play();
    }
}






