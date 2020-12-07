using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BarUI))]
public class Damageable : MonoBehaviour
{
    public float startingHealth;
    public float Health { get; private set; }
    public GameObject goldParticlePrefab;

	private PlayerUnitAI p;
    private EnemyUnitAI e;
	
    private void Start()
    {
        p = gameObject.GetComponent<PlayerUnitAI>();
		e = gameObject.GetComponent<EnemyUnitAI>();
        if (e != null)
        {
            Health = e.health;
        }
        else if (p != null)
        {
            Health = p.maxHealth;
        }
        else //object is not a player or enemy
        {
            Health = startingHealth; 
            gameObject.GetComponent<BarUI>().SetMaxHealth(startingHealth);
        }
    }

    public void TakeDamage(float value)
    {
        Health -= value;			
        //print(gameObject.name + " took " + value + " damage with " + Health + " health left.");
		gameObject.GetComponent<BarUI>().SetHealth(Health);
        if (Health <= 0)
		{
			Die();
		}
    }

    private void Die()
    {
        //Add gold if this is object is an enemy
        if (gameObject.GetComponent<EnemyUnitAI>())
        {
            PlayerStats.Instance.AddGold(gameObject.GetComponent<EnemyUnitAI>().goldDropped);
            if (goldParticlePrefab != null)
            {
                GameObject particleInstance = Instantiate(goldParticlePrefab, transform.position, transform.rotation, null);
                float duration = particleInstance.GetComponentInChildren<ParticleSystem>().main.duration; 
                Destroy(particleInstance, duration + 1f);
            }
            else
            {
                Debug.LogWarning("Gold particle not loaded for " + gameObject.name);
            }
        }

        //Activate game end if this is the lair
        if (gameObject.GetComponent<Lair>())
        {
			print(Health);
            MenuUI.Instance.OpenPanel(2);
            gameObject.SetActive(false);
        }
        else
            Destroy(gameObject);
    }
}
