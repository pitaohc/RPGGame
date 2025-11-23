using System;
using System.Threading;
using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    public string animBoolName;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;
    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.anim = player.anim;
        this.rb = player.rb;
        this.input = player.input;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("Enter: " + this.GetType().Name + " " + animBoolName);
        anim.SetBool(animBoolName, true);
        this.triggerCalled = false;

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        anim.SetFloat("yVelocity", player.rb.linearVelocityY);

        if (input.Player.Dash.WasPerformedThisFrame() && canDash())
        {
            stateMachine.ChangeState(player.dashState);
        }


        //Debug.Log("I run update of " + animBoolName);
    }

    public virtual void Exit()
    {
        //Debug.Log("Exit: " + this.GetType().Name + " " + animBoolName);
        anim.SetBool(animBoolName, false);
    }

    protected bool canDash()
    {
        if (player.stateMachine.currentState == player.dashState)
        {
            return false;
        }

        if (player.wallCheck)
        {
            return false;
        }

        return true;
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
