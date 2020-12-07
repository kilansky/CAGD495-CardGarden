using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerAttack : MonoBehaviour
{
    private SeasonType SeasonType {
        get { return transform.parent.GetComponent<Building>().seasonType.season; }
    }

    // finds all utility buildings and sum's their bonuses for each projectile shot
    private int AttackBonus {
        get
        {
            int bonus = 0;
            Utility[] utilityBuildings = FindObjectsOfType<Utility>();
            foreach (Utility u in utilityBuildings)
                bonus += u.GetAttackBonus(SeasonType);
            return bonus;
        }
    }
	
	public AudioSource coinDrop, attackImpact;
	
    protected Transform firePoint;
    protected GameObject projectile;
    protected float attackRate; // TODO make protected
    protected float attackPower;// TODO make protected

    protected float fireCountdown = 0;
    protected List<Transform> enemies = new List<Transform>();
    protected List<Transform> killedEnemies = new List<Transform>();

    private void Start()
    {
        firePoint = transform.parent.GetComponent<Building>().firePoint;
        projectile = transform.parent.GetComponent<Building>().projectile;
    }

    public void SetLevelStats(float newAttackPower, float newAttackRate, float newAttackRadius)
    {
        attackPower = newAttackPower;
        attackRate = newAttackRate;
        transform.localScale = new Vector3(newAttackRadius, newAttackRadius, newAttackRadius);
    }

    private void Update()
    {
        Attack();
    }

    // This default attack will be overriden by derived tower attack scripts
    protected virtual void Attack()
    {
        //Fires a single projectile at the closest enemy to the lair within this tower's radius
        //Check if there are any enemies in range and if the tower is ready to fire
        if (enemies.Count > 0 && fireCountdown <= 0)
        {
            RemoveKilledEnemies(); //Check for any null references in the enemies list and remove them
            Transform target = SetTarget(); //Get enemy to attack

            if (target)
            {
                FireProjectile(target); //Fire a projectile
                fireCountdown = attackRate; //Reset fireCountdown
            }
        }
        fireCountdown -= Time.deltaTime; //Decrease the fireCountdown timer
		attackImpact.Play();
    }

    //Instantiate a projectile and set its target
    protected void FireProjectile(Transform target)
    {
        GameObject firedProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        firedProjectile.GetComponent<Projectile>().SetOwner(transform.parent.gameObject);
        firedProjectile.GetComponent<Projectile>().SetTarget(target);
        //print("attack bonus: " + AttackBonus);
		if((int)attackPower+AttackBonus >= target.gameObject.GetComponent<Damageable>().Health)
		{
			coinDrop.Play();
		}
        firedProjectile.GetComponent<Projectile>().SetDamage((int)attackPower + AttackBonus);
		gameObject.GetComponent<AudioSource>().Play();
    }

    protected void RemoveKilledEnemies()
    {
        //Check if any enemies in the list have died (are null), and add them to the killedEnemies list
        foreach (Transform enemy in enemies)
            if (enemy == null)
                killedEnemies.Add(enemy);

        //For each enemy in the killedEnemies list, remove them from the enemies list
        foreach (Transform killedEnemy in killedEnemies)
            enemies.Remove(killedEnemy);

        //Now that all killedEnemies have been removed from the enemies list, clear killedEnemies
        killedEnemies.Clear();
    }
    
    //Returns the enemy in range that has the shortest path distance remaining
    protected Transform SetTarget()
    {
        //If there are no enemies in the list, the target is null
        if (enemies.Count == 0)
            return null;

        //Set the first enemy in the list as the closest to the lair
        Transform closestEnemy = enemies[0];
        float mostTraveledDist = closestEnemy.GetComponent<EnemyUnitAI>().distanceTraveled;

        //Compare each enemy's path distance to the lair and find the closest one
        for (int i = 1; i < enemies.Count; i++)
        {
            float enemyTravelDist = enemies[i].GetComponent<EnemyUnitAI>().distanceTraveled;
            if (enemyTravelDist > mostTraveledDist)
            {
                closestEnemy = enemies[i];
                mostTraveledDist = enemyTravelDist;
            }
        }
        return closestEnemy;
    }

    //If an enemy comes within range of this tower, add it to the enemies list
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies.Add(other.transform);
    }

    //If an enemy leaves the range of this tower, remove it from the enemies list
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            enemies.Remove(other.transform);
    }
}
