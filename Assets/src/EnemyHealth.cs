using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    public override bool TakeDamage(float damage, float elementDamage, ElementType elementType, Transform damageDealer)
    {
        if (base.TakeDamage(damage, elementDamage, elementType, damageDealer) == false) return false;
        if (!isDead && damageDealer.GetComponent<Player>() != null)
        {
            enemy.TryEnterBattleState(damageDealer);
        }

        return true;
    }
}