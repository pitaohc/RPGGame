using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private bool faceToRight = true;

    public int faceDirection { get; private set; } = 1;// face to right = 1, face to left = -1

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance = 1.0f;
    [SerializeField] private float wallCheckDistance = 1.0f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }


    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    public void HandleFlip(float xVelocity)
    {
        if (faceDirection * xVelocity < 0)
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

    protected void HandleCollisionDetection()
    {
        bool groundDetectedResult = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        groundDetected = groundDetectedResult;

        bool wallDetectedResult =
            Physics2D.Raycast(primaryWallCheck.position, faceDirection * Vector2.right, wallCheckDistance,
                whatIsGround);
        if (secondaryWallCheck)
        {
            wallDetectedResult &= Physics2D.Raycast(secondaryWallCheck.position, faceDirection * Vector2.right, wallCheckDistance,
                whatIsGround);
        }
        wallDetected = wallDetectedResult;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + groundCheckDistance * Vector3.down);
        Gizmos.DrawLine(
            primaryWallCheck.position,
            primaryWallCheck.position + faceDirection * wallCheckDistance * Vector3.right);
        if (secondaryWallCheck)
        {
            Gizmos.DrawLine(
                secondaryWallCheck.position,
                secondaryWallCheck.position + faceDirection * wallCheckDistance * Vector3.right);
        }

    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }



}
