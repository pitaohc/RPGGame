using UnityEngine;

public class Enemy_Skeleton : Enemy, ICounterable
{
    protected override void Awake()
    {
        base.Awake();
        // make sure the state name matches the parameter in animator.
        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        // the anim bool name is unusable, set "idle" to avoid warning that param does not exist
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    public void EnableCounterWindow(bool enable) => canBeStunned = enable;

    public void HandleCounter()
    {
        if (canBeStunned) stateMachine.ChangeState(stunnedState);
    }
}
