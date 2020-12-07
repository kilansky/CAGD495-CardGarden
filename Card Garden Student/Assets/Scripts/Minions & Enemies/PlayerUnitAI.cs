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

    public enum MinionAttackType
    {
        Normal,
        Rapid
    }

    public SeasonType GetSeasonType { get { return seasonType.season; } }

    public SeasonTypes seasonType;

    [HideInInspector] public Card cardData;
    public int level;

    public int maxHealth;
    public int attackPower;
    public float attackRange, attackRate;

    Animator animator;
    public GameObject projectilePrefab;
    public int expValue;
    public MinionAttackType attackType = MinionAttackType.Normal;
    [HideInInspector] private readonly float burstSpeed = 0.1f; // for rapid minions only

    // private vars
    private State state;
    private float attackTimer;

    private void Start()
    {
		gameObject.GetComponent<BarUI>().SetMaxHealth(maxHealth);
        SetLevelUpStats();

        animator = GetComponentInChildren<Animator>();
        state = State.Idle;
        attackTimer = attackRate;
        seasonType = cardData.seasonType; // soon may change during gameplay
    }

    public void SetLevelUpStats()
    {
        level = GetComponent<LevelUp>().baseLevel;
        Debug.Log(gameObject.name + " is now level " + (level + 1));

        maxHealth = (int)cardData.minionHealths[level];
        attackPower = (int)cardData.attackPowers[level];
        attackRange = cardData.attackRanges[level];
        attackRate = cardData.attackRates[level];
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
        if (animator) animator.SetBool("Attack", false);

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
        if (animator) animator.SetBool("Attack", true);

        attackTimer = Mathf.Clamp(attackTimer - Time.deltaTime, 0, attackRate);
        if (attackTimer == 0)
        {
            switch (attackType)
            {
                case MinionAttackType.Normal:
                    DealDamageNormal();
                    break;
                case MinionAttackType.Rapid:
                    DealDamageRapid();
                    break;
            }
            attackTimer = attackRate;
        }
    }

    // will check the surrounding area, if there is anything to hit, it will deal damage to 
    // the first thing it found
    // if there is nothing to hit then the minion will go back to move
    private void DealDamageNormal()
    {
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count == 0)
        {
            state = State.Idle;
            return ;
        }

        GameObject p = Instantiate(projectilePrefab, transform);
        p.GetComponent<Projectile>().SetDamage(attackPower);
        p.GetComponent<Projectile>().SetSeasonType(seasonType);
        p.GetComponent<Projectile>().SetTarget(enemies[0].transform);
        p.GetComponent<Projectile>().SetOwner(gameObject);

        // face the enemy you're attacking
        if (enemies[0] != null)
            LookAt(enemies[0]);
    }

    private void DealDamageRapid()
    {
        List<GameObject> enemies = DamageablesNearby();
        if (enemies.Count == 0)
        {
            state = State.Idle;
            return ;
        }


        // prioritize nearest enemy
        float minDist = float.MaxValue;
        GameObject curTarget = null;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (minDist > dist)
            {
                minDist = dist;
                curTarget = enemy;
            }
        }

        if (curTarget != null)
        {
            StartCoroutine(Burst(curTarget.transform));
        }
    }

    IEnumerator Burst(Transform target)
    {
        for (int i = 0; i < 3; ++i)
        {
            LookAt(target.gameObject);
            GameObject p = Instantiate(projectilePrefab, transform);
            p.GetComponent<Projectile>().SetDamage(attackPower);
            p.GetComponent<Projectile>().SetSeasonType(seasonType);
            p.GetComponent<Projectile>().SetTarget(target);
            p.GetComponent<Projectile>().SetOwner(gameObject);
            yield return new WaitForSeconds(burstSpeed);
        }
    }

    private List<GameObject> DamageablesNearby()
    {
        List<GameObject> damageables = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
    private void LookAt(GameObject target)
    {
        // rotate body 
        AnimationBody animationBody = GetComponentInChildren<AnimationBody>();
        if (animationBody != null)
        {
            animationBody.transform.LookAt(target.transform);
            Vector3 eulerRot = animationBody.transform.rotation.eulerAngles;
            animationBody.transform.rotation = Quaternion.Euler(0, eulerRot.y, 0);
        }
        else Debug.LogWarning("animation body missing in " + gameObject.name);
    }
}













