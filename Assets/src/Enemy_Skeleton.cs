using UnityEngine;

public class Enemy_Skeleton : Enemy
{


    protected override void Awake()
    {
        base.Awake();
        // make sure the state name matches the parameter in animator.
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
