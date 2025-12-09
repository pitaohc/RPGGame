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
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundCheck { get; private set; }
    public bool wallCheck { get; private set; }


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

    protected void HandleCollisionDetection()
    {
        groundCheck = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallCheck =
            Physics2D.Raycast(primaryWallCheck.position, faceDirection * Vector2.right, wallCheckDistance,
                whatIsGround) &&
            Physics2D.Raycast(secondaryWallCheck.position, faceDirection * Vector2.right, wallCheckDistance,
                whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            transform.position,
            transform.position + groundCheckDistance * Vector3.down);
        Gizmos.DrawLine(
            primaryWallCheck.position,
            primaryWallCheck.position + faceDirection * wallCheckDistance * Vector3.right);
        Gizmos.DrawLine(
            secondaryWallCheck.position,
            secondaryWallCheck.position + faceDirection * wallCheckDistance * Vector3.right);

    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }



}
