using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    private const int FirstComboIndex = 1;
    private const int LastComboIndex = 3;
    private float attackVelocityDuration;
    private int comboIndex = FirstComboIndex;
    private const string AnimIndexParamName = "basicAttackIndex";
    private float lastTimeAttacked;
    private bool comboAttackQueued;
    private int attackDir;
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;

        // Define attack direction according to input
        attackDir = (player.moveInput.x == 0) ? player.faceDirection : (int)player.moveInput.x;

        attackVelocityDuration = player.attackVelocityDuration;

        ResetComboIndexIfNeed();
        ApplyAttackVelocity();
        anim.SetInteger(AnimIndexParamName, comboIndex);
    }

    private void ResetComboIndexIfNeed()
    {
        if (comboIndex > LastComboIndex || Time.time > lastTimeAttacked + player.comboResetTime)
        {
            comboIndex = FirstComboIndex;
        }
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
        player.SetVelocity(velocity.x * attackDir, velocity.y);
    }

    public override void Update()
    {
        base.Update();
        attackVelocityDuration -= Time.deltaTime;
        HandleAttackVelocity();

        if (input.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueueNextAttack()
    {
        if (comboIndex < LastComboIndex)
        {
            comboAttackQueued = true;
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
