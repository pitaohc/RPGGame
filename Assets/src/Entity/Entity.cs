using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public event Action onFlip;
    public StateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    

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


    private bool isKnocked;
    private Coroutine knockbackCo;
    private Coroutine slowDownEntityCo;


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
        if (isKnocked)
        {
            return;
        }
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
        faceDirection = -faceDirection;
        transform.Rotate(0f, 180f, 0f);
        onFlip?.Invoke();
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

        //Handles.Label(primaryWallCheck.position, "Hello World");
    }

    public void CurrentStateAnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    private IEnumerator KnockbackCo(Vector2 knockback, float duration)
    {
        isKnocked = true;
        rb.linearVelocity = knockback * new Vector2(1, 1);
        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        isKnocked = false;
    }
    public void ReceiveKnockback(Vector2 knockback, float duration)
    {
        if (knockbackCo != null)
        {
            StopCoroutine(knockbackCo);
        }

        knockbackCo = StartCoroutine(KnockbackCo(knockback, duration));
    }

    public virtual void EntityDeath()
    {

    }

    public bool IsFaceRight()
    {
        return faceDirection > 0;
    }

    public void SlowDownEntity(float duration, float slowMultiplier)
    {
        if (slowDownEntityCo != null)
        {
            Debug.Log("co is not null");
            StopCoroutine(slowDownEntityCo);
        }
        
        slowDownEntityCo = StartCoroutine(SlowDownEntityCo(duration, slowMultiplier));
    }

    protected virtual IEnumerator SlowDownEntityCo(float duration, float slowMultiplier)
    {
        yield return null;
    }
}
