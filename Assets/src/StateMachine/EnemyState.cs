using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;
    private int Id_xVelocity;
    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
        Id_xVelocity = Animator.StringToHash("xVelocity");

    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        anim.SetFloat(Id_xVelocity, rb.linearVelocityX);

    }
}
