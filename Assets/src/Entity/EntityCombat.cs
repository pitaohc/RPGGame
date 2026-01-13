using System;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    public float damage = 10;
    [Header("Target Detection")]
    [SerializeField]
    private float targetCheckRadius = 1.0f;

    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private Transform targetCheck;

    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
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

            detected = true;
            damageable.TakeDamage(damage, transform);
            vfx.CreateOnHitVfx(target.transform);
        }
        return detected;
    }
}
