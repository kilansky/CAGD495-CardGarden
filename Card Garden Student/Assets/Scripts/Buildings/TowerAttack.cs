using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    private Building tower;
    private Transform firePoint;
    private GameObject projectile;
    private float attackRate;
    private int attackPower;

    private Transform target;
    private float fireCountdown = 0;
    private List<Transform> enemies = new List<Transform>();

    private void Start()
    {
        //Set up all variables using the parent building and its card data
        tower = transform.parent.GetComponent<Building>();
        firePoint = tower.firePoint;
        projectile = tower.projectile;
        attackRate = tower.cardData.attackRate;
        attackPower = (int)tower.cardData.attackPower;
    }

    private void Update()
    {
        //If a target is within range, begin the countdown to fire upon it
        if(enemies.Count > 0)
        {
            //If the first enemy in the list has not been destroyed, set it as the current target
            if (enemies[0])
                target = enemies[0];
            else //Otherwise, remove it from the list
                enemies.Remove(enemies[0]);

            //If there is a target and fireCountdown is 0 or less, fire a projectile
            if (target && fireCountdown <= 0)
            {
                FireProjectile();
                fireCountdown = attackRate;
            }

            //Decrease the fireCountdown timer
            fireCountdown -= Time.deltaTime;
        }
    }

    //Instantiate a projectile and set its target
    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        firedProjectile.GetComponent<Projectile>().SetTarget(target);
        firedProjectile.GetComponent<Projectile>().SetDamage(attackPower);
    }


    private void OnTriggerEnter(Collider other)
    {
        //If an enemy comes within range of this tower, add it to the enemies list
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.transform);
        }      
    }

    private void OnTriggerExit(Collider other)
    {
        //If an enemy leaves the range of this tower, remove it from the enemies list
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.transform);
        }
    }
}
