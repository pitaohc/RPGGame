using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX entityVFX;
    private Entity entity;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] protected Vector2 knockbackPower = new(1.5f, 2.5f);
    [SerializeField] protected float knockbackDuration = 0.2f;

    //[Header("On Heavy Damage")]
    //[SerializeField] protected float heavyDamageThreshold = .3f;
    //[SerializeField] protected float heavyKnockbackDuration = .5f;
    //[SerializeField] protected Vector2 heavyKnockbackPower = new(7, 7);
    protected virtual void Awake()
    {
        entityVFX = GetComponent<EntityVFX>();
        entity = GetComponent<Entity>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        Vector2 knockback = CalculateKnockback(damageDealer);
        entity?.ReceiveKnockback(knockback, knockbackDuration);
        entityVFX?.PlayOnDamageVfx();
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        Debug.Log("Entity dead");
    }

    //private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    //{
    //    int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
    //    return knockbackPower * new Vector2(direction, 1);
    //}
}
