using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public PlayerInputSet input { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 1.0f;
    [Header("Movement Details")]
    private bool faceToRight = true;
    public int faceDirection { get; private set; } = 1;// face to right = 1, face to left = -1
    public float moveSpeed = 1;
    public float jumpForce = 5;
    [Header("Dash Details")]
    public float dashDuration = 0.2f;
    public float dashSpeed = 20f;
    [Range(0f, 1f)]
    public float inAirMoveMultiplier = 1.0f; // should be [0,1]
    [Range(0f, 1f)]
    public float inWallSlideMultiplier = 1.0f; // should be [0,1]
    public Vector2 moveInput { get; private set; }
    public Vector2 wallJumpForce;
    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance = 1.0f;
    [SerializeField] private float wallCheckDistance = 1.0f;
    [SerializeField] private LayerMask whatIsGround;
    public bool groundCheck { get; private set; }
    public bool wallCheck { get; private set; }


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
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
    }

    private void OnEnable()
    {
        input.Enable();


        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (faceDirection * rb.linearVelocityX < 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        faceToRight = !faceToRight;
        faceDirection = -faceDirection;
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleCollisionDetection()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallCheck = Physics2D.Raycast(transform.position, faceDirection * Vector2.right, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            transform.position,
            transform.position + groundCheckDistance * Vector3.down);
        Gizmos.DrawLine(
            transform.position,
            transform.position + faceDirection * wallCheckDistance * Vector3.right);

    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }
}
