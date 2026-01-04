using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    private EnemyVFX enemyVfx;
    public Enemy_StunnedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyVfx = enemy.GetComponent<EnemyVFX>();
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = enemy.stunnedVelocity; // TODO 考虑朝向
        stateTimer = enemy.stunnedDuration;
        enemy.EnableCounterWindow(false);
        enemyVfx.EnableAttackAlert(false);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
