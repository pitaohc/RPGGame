using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EntityHealth : MonoBehaviour, IDamageable
{
    public event Action onHealthChanged;
    public event Action onDie;
    private EntityVFX entityVFX;
    private Entity entity;
    private EntityStat stat;

    protected float curHealth = 100.0f;
    // [SerializeField] protected float maxHealth = 100f;
    
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
        entityVFX = GetComponent<EntityVFX>();
        stat = GetComponent<EntityStat>();
        entity = GetComponent<Entity>();
        onHealthChanged?.Invoke();
        curHealth = stat.GetMaxHealth();
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
        onHealthChanged?.Invoke();
        if (curHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        isDead = true;
        entity?.EntityDeath();
        onDie?.Invoke();
        Debug.Log("Entity dead");
    }

    private Vector2 CalculateKnockback(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = (damage < stat.GetMaxHealth() * heavyDamageThreshold) ? knockbackPower : heavyKnockbackPower;
        return knockback * new Vector2(direction, 1);
    }

    private float CalculateDuration(float damage)
    {
        return (damage < stat.GetMaxHealth() * heavyDamageThreshold) ? knockbackDuration : heavyKnockbackDuration;
    }

    public float GetHealthRate()
    {
        return curHealth / stat.GetMaxHealth();
    }
}
