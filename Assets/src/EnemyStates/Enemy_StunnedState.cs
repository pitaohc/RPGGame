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
        rb.linearVelocity = enemy.stunnedVelocity * new Vector2(-enemy.faceDirection, 1);
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
