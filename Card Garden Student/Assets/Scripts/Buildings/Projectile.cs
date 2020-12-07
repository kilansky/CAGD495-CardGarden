using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private SeasonTypes mySeasonType;
    private GameObject owner;
    private Transform target;
    private int damage;
      
    public void SetOwner(GameObject _owner) { owner = _owner; }
    public void SetSeasonType(SeasonTypes _season_type) { mySeasonType = _season_type; }
    public void SetTarget(Transform _target) { target = _target; }
    public void SetDamage(int _damage) { damage = _damage; }

    private void Update()
    {
        //Make sure the target still exists
        if (target)
        {
            // face your target
            transform.LookAt(target.transform);
            Vector3 eulerRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulerRot.y + 90, 0);

            //Get the direction and distance to the target
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = projectileSpeed * Time.deltaTime;

            //If the amt moved on this frame would cause us to overshoot our target, we've hit it
            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            //Move the projectile towards the target
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
        else //If the target no longer exists, destroy self
            Destroy(gameObject);
    }

    private void HitTarget()
    {
        //Debug.Log("Enemy hit with projectile");
        EnemyUnitAI targetEnemy = target.GetComponent<EnemyUnitAI>();
        PlayerUnitAI targetPlayer = target.GetComponent<PlayerUnitAI>();

        //Check if the enemy will die, and if it will then add its expValue to the owner of this projectile
        if (target.GetComponent<Damageable>().Health - damage <= 0)
        {
            if(targetEnemy)
            {
                int exp = (int)target.GetComponent<EnemyUnitAI>().expValue;
                owner.GetComponent<LevelUp>().AddExp(exp);
            }
            else if (targetPlayer)
            {
                int exp = target.GetComponent<PlayerUnitAI>().expValue;
                owner.GetComponent<LevelUp>().AddExp(exp);
            }
        }

        // get season information and change dmg multiplier accordingly
        float dmgMult = 1f;
        if (targetEnemy && mySeasonType != null)
        {
            dmgMult = mySeasonType.GetDamageMod(targetEnemy.enemy.seasonType.season);

            /*
            Debug.Log(target.name + " of type " + targetEnemy.enemy.seasonType.GetSeasonString() +
                " took " + damage + " * " + dmgMult + " damage by " +
                owner.name + " of type " + mySeasonType.GetSeasonString());
            */
        }
        else if (targetPlayer && mySeasonType != null)
        {
            dmgMult = mySeasonType.GetDamageMod(targetPlayer.seasonType.season);

            /*
            Debug.Log(target.name + " of type " + targetPlayer.seasonType.GetSeasonString() +
                " took " + damage + " * " + dmgMult + " damage by " +
                owner.name + " of type " + mySeasonType.GetSeasonString());
            */
        }

        //Deal Damage

        target.GetComponent<Damageable>().TakeDamage(Mathf.FloorToInt(damage * dmgMult));

        //Destroy Projectile
        Destroy(gameObject);
    }
}
