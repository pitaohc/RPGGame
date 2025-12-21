using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState { get; protected set; }
    public Enemy_MoveState moveState { get; protected set; }

    [Header("Movement Details")]
    public float idleTime = 1.0f;
    public float moveSpeed = 1.0f;
    [Range(0f, 2f)]
    public float moveAnimSpeedMultiplier = 1.0f;

}
