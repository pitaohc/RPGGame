using System;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState { get; protected set; }
    public Enemy_MoveState moveState { get; protected set; }
    public Enemy_AttackState attackState { get; protected set; }
    public Enemy_BattleState battleState { get; protected set; }

    [Header("Movement Details")]
    public float idleTime = 1.0f;
    public float moveSpeed = 1.0f;
    [Range(0f, 2f)]
    public float moveAnimSpeedMultiplier = 1.0f;
    [Header("Player Detection")]
    public LayerMask whatIsPlayer;
    public float playerCheckDistance = 10.0f;
    public Transform playerCheck;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(
            playerCheck.position,
            playerCheck.position + faceDirection * playerCheckDistance * Vector3.right);

    }

    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D result = Physics2D.Raycast(playerCheck.position, Vector2.right, playerCheckDistance, whatIsPlayer | whatIsGround);
        if (result.collider == null || result.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return default;
        }

        return result;
    }
}
