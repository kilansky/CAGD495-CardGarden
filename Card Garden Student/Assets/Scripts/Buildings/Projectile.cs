using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private Transform target;
    private int damage;

    public void SetDamage(int _damage) {damage = _damage;}
    public void SetTarget (Transform _target) {target = _target;}

    private void Update()
    {
        //Make sure the target still exists
        if (target)
        {
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
        target.GetComponent<Damageable>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
