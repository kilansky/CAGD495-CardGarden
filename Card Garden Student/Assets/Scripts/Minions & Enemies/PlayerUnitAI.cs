using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitAI : MonoBehaviour
{
    private enum State
    {
        Attack,
        Idle
    }
    
    public int maxHealth, armor, damage;
    public float attackRadius, attackSpeed;
    public GameObject projectilePrefab;

    // private vars
    private State state;
    private float attackTimer;

    private void Start()
    {
        state = State.Idle;
        attackTimer = attackSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                StateIdle();
                break;
            case State.Attack:
                StateAttack();
                break;
        }
    }

    private void StateIdle()
    {
        // check if there's anything to hit
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count > 0)
        {
            state = State.Attack;
            attackTimer = 0;
        }
    }

    private void StateAttack()
    {
        attackTimer = Mathf.Clamp(
            attackTimer - Time.deltaTime, 0, attackSpeed);
        if (attackTimer == 0)
        {
            DealDamage();
            attackTimer = attackSpeed;
        }
    }

    // will check the surrounding area, if there is anything to hit, it will deal damage to 
    // the first thing it found
    // if there is nothing to hit then the minion will go back to move
    private void DealDamage()
    {
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count == 0)
        {
            state = State.Idle;
            return;
        }

        GameObject p = Instantiate(projectilePrefab, transform);
        p.GetComponent<MeshRenderer>().material.color = Color.green;
        p.GetComponent<Projectile>().SetDamage(damage);
        p.GetComponent<Projectile>().SetTarget(enemies[0].transform);
    }

    private List<GameObject> DamageablesNearby()
    {
        List<GameObject> damageables = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
        foreach (var hitCollider in hitColliders)
        {
            Damageable d = hitCollider.GetComponent<Damageable>();
            if (d != null && d.gameObject != gameObject && d.gameObject.CompareTag("Enemy"))
                damageables.Add(d.gameObject);
        }

        return damageables;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}













