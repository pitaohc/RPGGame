using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player_JumpAttackState : PlayerState
{
    private bool isGrounded; // make sure the trigger is only called once
    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        isGrounded = false;
        player.SetVelocity(player.faceDirection * player.jumpAttackVelocity.x, player.jumpAttackVelocity.y);
    }
    public override void Update()
    {
        base.Update();
        if (player.groundCheck && !isGrounded)
        {
            anim.SetTrigger("jumpAttackTrigger");
            player.SetVelocity(0, rb.linearVelocityY);
            isGrounded = true;
        }

        if (player.groundCheck && triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
