using System;
using System.Threading;
using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    public string animBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("Enter: " + this.GetType().Name + " " + animBoolName);
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
    }

    public virtual void Exit()
    {
        //Debug.Log("Exit: " + this.GetType().Name + " " + animBoolName);
        anim.SetBool(animBoolName, false);
    }


    public void AnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameters()
    {
    }
}
