using System;
using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage, Transform damageDealer)
    {
        if (damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }
        base.TakeDamage(damage, damageDealer);
    }
}
