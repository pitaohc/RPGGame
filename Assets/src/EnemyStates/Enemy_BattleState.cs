using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform transform_player;
    private static int animIdBattleAnimSpeedMultiplier = Animator.StringToHash("battleAnimSpeedMultiplier");
    private float lastFindPlayerTime = 0.0f; // TODO private

    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        UpdateBattleTimer();
        transform_player ??= enemy.GetPlayerReference();
        //Debug.Log("Enter Battle State " + transform_player.name);
        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(-enemy.retreatVelocity.x * DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();
        if (enemy.PlayerDetection())
        {
            UpdateBattleTimer();
        }

        if (BattleTimeIsOver())
        {
            //Debug.Log("change state to move");
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (WithAttackRange() && DirectionToPlayer() == enemy.faceDirection)
        {
            //Debug.Log("change state to attack");
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            //Debug.Log("move to player");
            enemy.SetVelocity(enemy.battleMoveVelocity * DirectionToPlayer(), rb.linearVelocityY);
        }
    }
    private void UpdateBattleTimer() => lastFindPlayerTime = Time.time;
    public bool BattleTimeIsOver() =>
        Time.time >= lastFindPlayerTime + enemy.battleTimeDuration;
    private bool WithAttackRange() =>
        DistanceToPlayer() <= enemy.attackDistance;

    private bool ShouldRetreat() => DistanceToPlayer() <= enemy.minRetreatDistance;
    private int DirectionToPlayer()
    {
        if (transform_player == null)
        {
            return 0;
        }

        return (enemy.transform.position.x - transform_player.position.x > 0) ? -1 : 1;
    }
    private float DistanceToPlayer()
    {
        if (transform_player == null)
        {
            return float.MaxValue;
        }

        return Mathf.Abs(enemy.transform.position.x - transform_player.position.x);
        //return Vector2.Distance(enemy.transform.position, transform_player.position);
    }

    public override void UpdateAnimationParameters()
    {
        base.UpdateAnimationParameters();
        anim.SetFloat(animIdBattleAnimSpeedMultiplier, enemy.GetBattleAnimSpeedMultiplier());

    }
}
