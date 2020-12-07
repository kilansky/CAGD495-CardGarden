using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    public float damagePerTick;
    public float tickRate;
    public float duration;

    private float tickTime;
    readonly List<Damageable> damageables = new List<Damageable>();

    private void Start()
    {
        tickTime = tickRate; 
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(transform.gameObject);

        tickTime -= Time.deltaTime;
        if (tickTime < 0)
        {
            for (int i=0; i < damageables.Count; i++)
            {
                if (damageables[i] != null)
                    damageables[i].TakeDamage(damagePerTick);

                // seperate if's purposeful:
                // checking if the enemy has died after taking damage...
                if (damageables[i] == null) damageables.RemoveAt(i);
            }
            tickTime = tickRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable d = other.gameObject.GetComponent<Damageable>();
        if (d != null && d.CompareTag("Enemy"))
        {
            damageables.Add(d);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Damageable d = other.gameObject.GetComponent<Damageable>();
        if (d != null)
        {
            damageables.Remove(d);
        }
    }
}
