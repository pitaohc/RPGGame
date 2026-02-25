using System;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    private EntityStat stat;
    [Header("Target Detection")]
    [SerializeField]
    private float targetCheckRadius = 1.0f;

    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private Transform targetCheck;
    [Header("Status Effect Details")]
    [SerializeField] private float defaultDuration = 10;
    [SerializeField] private float chillSlowMultiplier = 0.2f;
    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
        stat = GetComponent<EntityStat>();
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

    public bool PerformAttack()
    {
        bool detected = false;
        foreach (Collider2D target in GetDetectedColliders())
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable == null) continue;
            bool isCrit;
            ElementType elementType = ElementType.None;

            float physicDamage = stat.GetPhysicalDamage(out isCrit);
            float elementDamage = stat.GetElementDamage(out elementType);
            if (!damageable.TakeDamage(physicDamage,elementDamage,elementType,transform)) continue;
            detected = true;
            vfx.UpdateOnHitColor(elementType);
            vfx.CreateOnHitVfx(target.transform,isCrit);
            if (elementType != ElementType.None)
                ApplyStatusEffect(target.transform, elementType);
        }
        return detected;
    }

    private void ApplyStatusEffect(Transform target, ElementType elementType)
    {
        EntityStatusHandler handler = target.GetComponent<EntityStatusHandler>();
        if (handler is null) return;

        if (elementType == ElementType.Ice && handler.CanBeApplied(elementType))
        {
            handler.ApplyChillEffect(defaultDuration, chillSlowMultiplier);
        }
    }
}
