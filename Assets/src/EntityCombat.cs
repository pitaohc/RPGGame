using System;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [Header("Target Detection")]
    [SerializeField]
    private float targetCheckRadius = 1.0f;

    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private Transform targetCheck;

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(targetCheck.position, targetCheckRadius);
    }

    public void PerformAttack()
    {

        foreach (Collider2D target in GetDetectedColliders())
        {
            Debug.Log("Attacking" + target.name);
        }
    }
}
