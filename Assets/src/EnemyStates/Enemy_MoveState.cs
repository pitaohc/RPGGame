using UnityEngine;

public class Enemy_MoveState : Enemy_GroundState
{
    private static int animIdmoveAnimSpeedMultiplier = Animator.StringToHash("moveAnimSpeedMultiplier");
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (enemy.wallDetected || !enemy.groundDetected)
        {
            enemy.Flip();
        }
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.faceDirection, rb.linearVelocityY);
        if (enemy.wallDetected || !enemy.groundDetected)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat(animIdmoveAnimSpeedMultiplier, enemy.moveAnimSpeedMultiplier);
    }
}
