using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns particles immediately and does not delete them
// parent of ParticleControllerInterval 
public class ParticleController : MonoBehaviour
{
    public GameObject[] particlePrefabs;

    private void Start()
    {
        foreach (GameObject go in particlePrefabs)
            Instantiate(go, transform);
    }
}






