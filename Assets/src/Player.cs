using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    // Events
    public static event Action OnPlayerDeath;
    // Input
    public PlayerInputSet input { get; private set; }
    // States
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_JumpAttackState jumpAttackState { get; private set; }
    public Player_DeadState deadState { get; private set; }
    public Player_CounterAttackState counterAttackState { get; private set; }

    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1.0f;
    private Coroutine queuedAttackCo;

    [Header("Movement Details")]
    public float moveSpeed = 1;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;
    [Range(0f, 1f)]
    public float inAirMoveMultiplier = 1.0f; // should be [0,1]
    [Range(0f, 1f)]
    public float inWallSlideMultiplier = 1.0f; // should be [0,1]
    public Vector2 moveInput { get; private set; }

    [Header("Dash Details")]
    public float dashDuration = 0.2f;
    public float dashSpeed = 20f;


    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInputSet();

        // make sure the state name matches the parameter in animator.
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump_fall");
        fallState = new Player_FallState(this, stateMachine, "jump_fall");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jump_fall");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
        deadState = new Player_DeadState(this, stateMachine, "dead");
        counterAttackState = new Player_CounterAttackState(this, stateMachine, "counterAttack");

    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCo != null)
        {
            StopCoroutine(queuedAttackCo);
        }
        queuedAttackCo = StartCoroutine(EnterAttackStateWithDelayCo());
    }
    private IEnumerator EnterAttackStateWithDelayCo()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(basicAttackState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        input.Disable();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        stateMachine.ChangeState(deadState);
        OnPlayerDeath?.Invoke();
    }
}
