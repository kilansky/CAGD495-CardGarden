using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int startingHealth;
    protected int Health { get; private set; }

    private void Start()
    {
        PlayerUnitAI p = gameObject.GetComponent<PlayerUnitAI>();
        EnemyUnitAI e = gameObject.GetComponent<EnemyUnitAI>();
        if (e != null)
        {
            Health = e.maxHealth[e.CurrentLevel];
        }
        else if (p != null)
        {
            Health = p.maxHealth[p.CurrentLevel];
        }
        else
        {
            Health = startingHealth; 
        }
    }

    public void TakeDamage(int value)
    {
        Health -= value;
        print(gameObject.name + " took " + value + " damage with " + Health + " health left.");
        if (Health <= 0) Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
