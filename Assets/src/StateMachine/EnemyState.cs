using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    private static int animIdXVelocity = Animator.StringToHash("xVelocity");
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;

    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat(animIdXVelocity, rb.linearVelocityX);
    }
}
