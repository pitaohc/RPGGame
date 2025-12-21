using System;
using System.Threading;
using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;

    protected PlayerInputSet input;


    public PlayerState(Player player, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        this.anim = player.anim;
        this.rb = player.rb;
        this.input = player.input;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        anim.SetFloat("yVelocity", player.rb.linearVelocityY);

        if (input.Player.Dash.WasPerformedThisFrame() && canDash())
        {
            stateMachine.ChangeState(player.dashState);
        }


        //Debug.Log("I run update of " + animBoolName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected bool canDash()
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

}