using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell { Freeze, FirePower, OnFire, DeusExMachina, BeeInfestation };

public class Spells : SingletonPattern<Spells>
{
    //Chooses which spell to cast
    public void CastSpell(Card card, int level, Transform spellLocation)
    {
        switch (card.spell)
        {
            case Spell.Freeze:
                StartCoroutine(Freeze(card, level, spellLocation));
                break;
            case Spell.FirePower:
                StartCoroutine(FirePower(card, level, spellLocation));
                break;
            case Spell.OnFire:
                StartCoroutine(TileEffect(card, level, spellLocation));
                break;
            case Spell.DeusExMachina:
                StartCoroutine(DeusExMachina(card, level));
                break;
            case Spell.BeeInfestation:
                StartCoroutine(TileEffect(card, level, spellLocation));
                break;
            default:
                Debug.LogWarning("Spell not found, failed to cast " + card);
                break;
        }
    }


    //------------------LEVEL UP SPELLS------------------

    //Adds a fire level to any building or minion
    private IEnumerator FirePower(Card card, int level, Transform spellLocation)
    {
        //Play an effect/particle system to indicate spell activation
        Instantiate(card.thingToSpawn, spellLocation.position, Quaternion.identity);

        //Wait briefly
        yield return new WaitForSeconds(card.buildTimes[level]);

        //Cast the spell
        GameObject building = spellLocation.gameObject;
        //building.GetComponent<LevelUp>().IncreaseElementalLevel(SeasonalType.Fire);
    }

    //------------------TILE EFFECT SPELLS------------------
    // freeze enemies if they are in the specified tile 
    private IEnumerator Freeze(Card card, int level, Transform spellLocation)
    {
        //Wait briefly to spawn
        yield return new WaitForSeconds(card.buildTimes[level]);

        //Cast the spell - Play an effect/particle system to indicate spell activation
        GameObject tileEffect = Instantiate(card.thingToSpawn, spellLocation.parent.position, Quaternion.identity);

        // set startLifeTimer = spellDuration, freezeDuration = attackRates
        FreezeTile ft = tileEffect.GetComponent<FreezeTile>();
        if (ft != null)
        {
            ft.freezeDuration = card.attackRates[level];
            ft.startLifeTimer = card.spellDurations[level];
            Debug.Log(ft.freezeDuration + " " + ft.startLifeTimer);
        }
    }

    //Sets a lane tile on fire
    private IEnumerator TileEffect(Card card, int level, Transform spellLocation)
    {
        //Wait briefly to spawn
        yield return new WaitForSeconds(card.buildTimes[level]);

        //Cast the spell - Play an effect/particle system to indicate spell activation
        GameObject tileEffect = Instantiate(card.thingToSpawn, spellLocation.parent.position, Quaternion.identity);

        //Set damage, attack rate, attack range, and duration of tileEffect
        tileEffect.GetComponent<DamageOverTime>().damagePerTick = card.attackRates[level];
        tileEffect.GetComponent<DamageOverTime>().tickRate = card.attackRates[level];
        tileEffect.GetComponent<DamageOverTime>().duration = card.spellDurations[level];
    }

    //------------------EMERGENCY SPELLS------------------

    //Deals deusExMachinaDamage to all enemies
    private IEnumerator DeusExMachina(Card card, int level)
    {
        EnemyUnitAI[] enemies = FindObjectsOfType<EnemyUnitAI>();
        System.Array.Reverse(enemies);
        int enemyCount = enemies.Length;

        //Wait briefly to spawn
        yield return new WaitForSeconds(card.buildTimes[level]);

        foreach (EnemyUnitAI enemy in enemies)
        {
            //Play an effect/particle system to indicate spell activation
            Instantiate(card.thingToSpawn, enemy.transform.position + new Vector3(0, 10, 0), Quaternion.identity);

            //Wait briefly
            yield return new WaitForSeconds(card.attackRates[level]);

            //Deal damage to hit enemy
            enemy.gameObject.GetComponent<Damageable>().TakeDamage(card.attackPowers[level]);
        }
    }
}
