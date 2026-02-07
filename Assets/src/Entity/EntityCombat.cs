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
            vfx.CreateOnHitVfx(target.transform,isCrit);
        }
        return detected;
    }
}
