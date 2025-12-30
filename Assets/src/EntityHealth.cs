using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX entityVFX;
    private Entity entity;
    protected float curHealth = 100f;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] protected float knockbackDuration = 0.2f;
    [SerializeField] protected Vector2 knockbackPower = new(1.5f, 2.5f);

    [Header("On Heavy Damage")]
    [SerializeField] protected float heavyDamageThreshold = .3f;
    [SerializeField] protected float heavyKnockbackDuration = .5f;
    [SerializeField] protected Vector2 heavyKnockbackPower = new(7, 7);
    protected virtual void Awake()
    {
        curHealth = maxHealth;
        entityVFX = GetComponent<EntityVFX>();
        entity = GetComponent<Entity>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        Vector2 knockback = CalculateKnockback(damage, damageDealer);
        float duration = CalculateDuration(damage);
        entity?.ReceiveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVfx();
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        entity?.EntityDeath();
        Debug.Log("Entity dead");
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = (damage < maxHealth * heavyDamageThreshold) ? knockbackPower : heavyKnockbackPower;
        return knockback * new Vector2(direction, 1);
    }

    private float CalculateDuration(float damage)
    {
        return (damage < maxHealth * heavyDamageThreshold) ? knockbackDuration : heavyKnockbackDuration;
    }
}
