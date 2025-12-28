using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Entity
{
    public Enemy_IdleState idleState { get; protected set; }
    public Enemy_MoveState moveState { get; protected set; }
    public Enemy_AttackState attackState { get; protected set; }
    public Enemy_BattleState battleState { get; protected set; }
    public Transform player { get; private set; }

    [Header("Movement Details")]
    public float idleTime = 1.0f;
    public float moveSpeed = 1.0f;
    [Range(0f, 2f)]
    public float moveAnimSpeedMultiplier = 1.0f;
    [Header("Player Detection")]
    public LayerMask whatIsPlayer;
    public float playerCheckDistance = 10.0f;
    public Transform playerCheck;
    [Header("Battle Details")]
    public float battleTimeDuration = 5.0f;
    public float battleMoveVelocity = 3.0f;
    public float attackDistance = 2.0f;
    public float minRetreatDistance = 1.0f;
    public Vector2 retreatVelocity;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            playerCheck.position,
            playerCheck.position + faceDirection * playerCheckDistance * Vector3.right);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position,
            playerCheck.position + faceDirection * attackDistance * Vector3.right);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position,
            playerCheck.position + faceDirection * minRetreatDistance * Vector3.right);

    }

    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D result = Physics2D.Raycast(playerCheck.position, faceDirection * Vector2.right, playerCheckDistance, whatIsPlayer | whatIsGround);
        if (result.collider == null || result.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return default;
        }

        player = result.transform;
        return result;
    }

    public float GetBattleAnimSpeedMultiplier()
    {
        return battleMoveVelocity / moveSpeed;
    }

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState || stateMachine.currentState == attackState)
        {
            return;
        }
        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetPlayerReference()
    {
        return player;
    }
}
