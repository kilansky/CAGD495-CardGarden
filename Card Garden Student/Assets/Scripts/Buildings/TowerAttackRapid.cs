using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackRapid : TowerAttack
{
    public float burstSpeed = 0.1f; // speed in-between bursts in seconds

    protected override void Attack() 
    {
        if (enemies.Count > 0 && fireCountdown <= 0)
        {
            RemoveKilledEnemies(); 
            Transform target = SetTarget();

            if (target)
            {
                StartCoroutine(Burst(target));
                fireCountdown = attackRate;
            }
        }
        fireCountdown -= Time.deltaTime;
    }

    IEnumerator Burst(Transform target)
    {
        for (int i = 0; i < 3; i++)
        {
            FireProjectile(target);
            yield return new WaitForSeconds(burstSpeed);
        }
    }
}
