using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX entityVFX;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<EntityVFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
        {
            return;
        }
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
}
