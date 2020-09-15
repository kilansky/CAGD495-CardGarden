using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int startingHealth;
    protected int Health { get; private set; }

    private void Start()
    {
        Health = startingHealth; 
    }

    public void TakeDamage(int value)
    {
        Health -= value;
        print(gameObject.name + " has taken " + value + " damage with " + Health + " health left.");
        if (Health < 0) Die();
    }

    private void Die()
    {
        print(gameObject.name + " has died.");
        Destroy(this.gameObject);
    }
}
