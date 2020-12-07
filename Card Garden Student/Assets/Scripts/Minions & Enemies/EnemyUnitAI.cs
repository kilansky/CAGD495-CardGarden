using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyClass { Paladin, Archer, Rogue }

public class EnemyUnitAI : MonoBehaviour
{
    private enum State { Attack, Move, Frozen }
    private Vector3 lastPos;
    private int currentPathIndex;
    private State state;
    private float attackTimer;
    private float freezeTimer;
    private NavMeshAgent agent;
    private Animator animator;

    public Enemy enemy;
    public List<Transform> destinations;

    [HideInInspector] public int startingLevel = 1;
    [HideInInspector] public float distanceTraveled;
    [HideInInspector] public float health;
    [HideInInspector] public float expValue;
    [HideInInspector] public float goldDropped;


    private void Start()
    {
        GetComponent<LevelUp>().baseLevel = startingLevel - 1;
        animator = GetComponentInChildren<Animator>();
        if (animator == null) Debug.LogWarning("object wants animator: " + gameObject.name);

        state = State.Move;
        agent = GetComponent<NavMeshAgent>();
        currentPathIndex = 0;
        agent.SetDestination(destinations[currentPathIndex].position);
        lastPos = transform.position;
        distanceTraveled = 0;

        health = enemy.maxHealths[0];
        SetLevelStats();
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
            case State.Frozen:
                StateFrozen();
                break;
        }

        //Calculate the distance traveled (used for TowerAttack priority)
        distanceTraveled += Vector3.Distance(transform.position, lastPos);
        lastPos = transform.position;
    }

    public void SetLevelStats()
    {
        agent.speed = enemy.movementSpeeds[startingLevel];
        expValue = enemy.expValues[startingLevel];
        goldDropped = enemy.goldDropped[startingLevel];
        attackTimer = enemy.attackRates[startingLevel];

        //TO DO: ENEMY SHOULD ADD HEALTH ON LVL UP, NOT FULL HEAL
        health += enemy.maxHealths[startingLevel];
        gameObject.GetComponent<BarUI>().SetMaxHealth(health);
    }

    private void StateFrozen()
    {
        freezeTimer -= Time.deltaTime;
        if (freezeTimer < 0) Unfreeze();
    }

    private void StateMove()
    {
        // find a new destination
        float dist = Vector2.Distance(
            new Vector2(destinations[currentPathIndex].position.x, destinations[currentPathIndex].position.z),
            new Vector2(transform.position.x, transform.position.z));

        if (dist < 2f && currentPathIndex < destinations.Count - 1)
        {
            currentPathIndex++;
            agent.SetDestination(destinations[currentPathIndex].position);
        }

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
        attackTimer = Mathf.Clamp(attackTimer - Time.deltaTime, 0, enemy.attackRates[startingLevel]);

        if (attackTimer == 0)
        {
            DealDamage();
            if (animator != null) animator.SetTrigger("Attack");
            attackTimer = enemy.attackRates[startingLevel];
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
            agent.speed = enemy.movementSpeeds[startingLevel];
            return;
        }

        GameObject p = Instantiate(enemy.projectilePrefab, transform.position, transform.rotation);

        // do not show projectile if the enemy is a rogue or paladin
        if (enemy.enemyClass == EnemyClass.Paladin || enemy.enemyClass == EnemyClass.Rogue)
        {
            MeshRenderer[] meshRenderers = p.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in meshRenderers) 
                mr.enabled = false;
        }

        p.GetComponent<Projectile>().SetDamage((int)enemy.attackPowers[startingLevel]);
        p.GetComponent<Projectile>().SetSeasonType(enemy.seasonType);
        p.GetComponent<Projectile>().SetTarget(enemies[0].transform);
        p.GetComponent<Projectile>().SetOwner(gameObject);
    }

    private List<GameObject> DamageablesNearby()
    {
        List<GameObject> damageables = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, enemy.attackRanges[startingLevel]);
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
        Gizmos.DrawWireSphere(transform.position, enemy.attackRanges[startingLevel]);
    }

    // called by FreezeTile to freeze this enemy
    public void Freeze(float duration)
    {
        //Debug.Log("\"im cold!\", said the " + gameObject.name);
        if (animator != null) animator.SetBool("Frozen", true);
        freezeTimer = duration;
        state = State.Frozen;
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
    }

    // called by FreezeTile to unfreeze this enemy
    public void Unfreeze()
    {
        //Debug.Log("\"im thawed!\", said the " + gameObject.name);
        if (animator != null) animator.SetBool("Frozen", false);
        freezeTimer = -1;
        state = State.Move;
        gameObject.GetComponent<NavMeshAgent>().speed =
            enemy.movementSpeeds[startingLevel];
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerUnitAI puai = collision.gameObject.GetComponent<PlayerUnitAI>();
        if (puai)
        {
            print("AAAA!");
            puai.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        PlayerUnitAI puai = collision.gameObject.GetComponent<PlayerUnitAI>();
        if (puai)
        {
            print("AAAA!");
            puai.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}












