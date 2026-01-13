using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private PlayerCombat combat;
    private bool hasPerformedCounter;
    private int animIdCounterAttackPerformed = Animator.StringToHash("counterAttackPerformed");
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<PlayerCombat>();
        //Debug.Log(combat);
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = combat.GetCounterRecover();
        anim.SetBool(animIdCounterAttackPerformed, false);
        hasPerformedCounter = combat.CounterAttackPerformed();
        rb.linearVelocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if (hasPerformedCounter)
        {
            anim.SetBool(animIdCounterAttackPerformed, true);
        }
        if (!hasPerformedCounter && stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (triggerCalled)
        {
            anim.SetBool(animIdCounterAttackPerformed, false);
            stateMachine.ChangeState(player.idleState);
        }
    }

}
