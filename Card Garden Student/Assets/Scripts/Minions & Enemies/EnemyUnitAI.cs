using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnitAI : MonoBehaviour
{
    private enum State
    {
        Attack,
        Move
    }

    public enum EnemyClass
    {
        Paladin,
        Archer,
        Rogue
    }
    
    public int maxHealth, armor, damage;
    public float attackRadius, attackSpeed, movementSpeed;
    public int goldDropped;
    public string affix;
    public GameObject projectilePrefab;
    public EnemyClass enemyClass;

    // private vars
    private State state;
    private float attackTimer;
    private NavMeshAgent agent;
    private Transform lairTransform;

    private void Start()
    {
        state = State.Move;
        attackTimer = attackSpeed;
        agent = GetComponent<NavMeshAgent>();
        lairTransform = GameObject.FindWithTag("Lair").GetComponent<Transform>();
        agent.SetDestination(lairTransform.position);
        agent.speed = movementSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Move:
                StateMove();
                break;
            case State.Attack:
                StateAttack();
                break;
        }
    }

    private void StateMove()
    {
        // check if there's anything to hit
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count > 0)
        {
            state = State.Attack;
            agent.speed = 0;
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
    // if there is nothing to hit then the enemy will go back to move
    private void DealDamage()
    {
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count == 0)
        {
            state = State.Move;
            agent.speed = movementSpeed;
            return;
        }

        GameObject p = Instantiate(projectilePrefab, transform);
        switch (enemyClass)
        {
            case EnemyClass.Rogue:
                p.GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case EnemyClass.Paladin:
                p.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case EnemyClass.Archer:
                p.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }

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
            if (d != null && d.gameObject != gameObject && !d.gameObject.CompareTag("Enemy"))
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












