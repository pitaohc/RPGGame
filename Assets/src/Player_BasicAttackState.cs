using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private const int FirstComboIndex = 1;
    private const int LastComboIndex = 3;
    private float attackVelocityDuration;
    private int comboIndex = FirstComboIndex;
    private const string AnimIndexParamName = "basicAttackIndex";
    private float lastTimeAttacked;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackVelocityDuration = player.attackVelocityDuration;
        ApplyAttackVelocity();
        if (comboIndex > LastComboIndex || Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
        anim.SetInteger(AnimIndexParamName, comboIndex);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 velocity = Vector2.zero;
        if (comboIndex - 1 < player.attackVelocity.Length)
        {
            velocity = player.attackVelocity[comboIndex - 1];
        }
        else
        {
            Debug.LogWarning("Unable Load Velocity in Player");

        }
        player.SetVelocity(velocity.x * player.faceDirection, velocity.y);
    }

    public override void Update()
    {
        base.Update();
        attackVelocityDuration -= Time.deltaTime;
        HandleAttackVelocity();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        comboIndex++;
        lastTimeAttacked = Time.time;
    }

    private void HandleAttackVelocity()
    {
        if (attackVelocityDuration < 0)
        {
            player.SetVelocity(0, rb.linearVelocityY);
        }
    }

}
