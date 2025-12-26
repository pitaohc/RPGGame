using System;
using System.Threading;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;
    private static int animIdYVelocity = Animator.StringToHash("yVelocity");

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        this.anim = player.anim;
        this.rb = player.rb;
        this.input = player.input;
    }


    public override void Update()
    {
        base.Update();

        if (input.Player.Dash.WasPerformedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    protected bool CanDash()
    {
        if (player.stateMachine.currentState == player.dashState)
        {
            return false;
        }

        if (player.wallDetected)
        {
            return false;
        }

        return true;
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat(animIdYVelocity, player.rb.linearVelocityY);
    }
}