using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.enabled = false;

        rb.gravityScale = enemy.deathGravityScale;
        rb.linearVelocityY = enemy.deathLinearVelocityY;

        Collider2D collider = enemy.GetComponent<Collider2D>();
        collider.enabled = false;
        stateMachine.Disable();
    }
}
