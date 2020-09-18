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
    
    public int CurrentLevel
    {
        get { return gameObject.GetComponent<Levelupable>().level - 1; }
    }

    public int[] maxHealth, armor, damage;
    public float[] attackRadius, attackSpeed, movementSpeed;
    public int goldDropped;
    public string enemyClass, affix;

    // private vars
    private State state;
    private float attackTimer;
    private NavMeshAgent agent;
    private Transform lairTransform;

    private void Start()
    {
        state = State.Move;
        attackTimer = attackSpeed[CurrentLevel];
        agent = GetComponent<NavMeshAgent>();
        lairTransform = GameObject.FindWithTag("Lair").GetComponent<Transform>();
        agent.SetDestination(lairTransform.position);
        agent.speed = movementSpeed[CurrentLevel];
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
            attackTimer - Time.deltaTime, 0, attackSpeed[CurrentLevel]);
        if (attackTimer == 0)
        {
            DealDamage();
            attackTimer = attackSpeed[CurrentLevel];
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
            agent.speed = movementSpeed[CurrentLevel];
            return;
        }

        enemies[0].GetComponent<Damageable>().TakeDamage(damage[CurrentLevel]);
    }

    private List<GameObject> DamageablesNearby()
    {
        List<GameObject> damageables = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius[CurrentLevel]);
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
        Gizmos.DrawWireSphere(transform.position, attackRadius[CurrentLevel]);
    }
}












