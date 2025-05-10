using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int _damange)
    {
        base.TakeDamage(_damange);

        enemy.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
