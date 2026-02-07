using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EntityHealth : MonoBehaviour, IDamageable
{
    public event Action onHealthChanged;
    public event Action onDie;
    private EntityVFX entityVFX;
    private Entity entity;
    private EntityStat stat;

    protected float curHealth;
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
        curHealth = stat.GetMaxHealth();
        onHealthChanged?.Invoke();
    }

    public virtual bool TakeDamage(float damage, float elementDamage, ElementType elementType, Transform damageDealer)
    {
        if (isDead) return false;

        if (AttackEvaded())
        {
            Debug.Log($"{gameObject.name} evaded attack");
            return false;
        }

        EntityStat dealerStat = damageDealer.GetComponent<EntityStat>();
        float reduction = dealerStat.GetArmorReduction();
        float reductionMultiplier = Mathf.Clamp01(1 - reduction); // 计算破甲比例，获得护甲有效倍率
        
        float mitigation = stat.GetArmorMitigation();
        float effectMitigation = mitigation * reductionMultiplier; // 获得真实有效的护甲值
        float finalPhysicalDamage = damage * (1 - effectMitigation); // 计算伤害的生效只

        float elementalResistance = stat.GetElementalResistance(elementType);
        float finalElementDamage = elementDamage * (1 - elementalResistance);
        // Debug.Log($"element damage: {elementDamage}, elemental Resistance: {elementalResistance}, element type: {elementType}, mitigation: {elementalResistance}, final element damage: {finalElementDamage}");
        // Debug.Log($"{gameObject.name} take {damage} damage, mitigation: {mitigation}, final damage {finalDamage}");
        Vector2 knockback = CalculateKnockback(finalPhysicalDamage, damageDealer);
        float duration = CalculateDuration(finalPhysicalDamage);
        // Debug.Log($"{gameObject.name} damage: {finalDamage}, element Damage: {elementDamage}");
        entity?.ReceiveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVfx();
        ReduceHealth(finalPhysicalDamage + finalElementDamage);

        return true;
    }

    private bool AttackEvaded() => Random.Range(0, 100) < stat.GetEvasion();
    
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
