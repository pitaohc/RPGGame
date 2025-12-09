using Mono.Cecil.Cil;
using UnityEngine;

public class Player_DashState : PlayerState
{
    private int faceDirection;

    private float originalGravityScale;
    // Start is called once before the first execution of Move after the MonoBehaviour is created
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Enter Dash");
        stateTimer = player.dashDuration;
        faceDirection = (player.moveInput.x == 0) ? player.faceDirection : (int)player.moveInput.x;
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log("duration" + stateTimer);
        player.SetVelocity(player.dashSpeed * faceDirection, 0);
        cancelDashIfNeed();

    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Dash");
        player.SetVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void cancelDashIfNeed()
    {
        if (player.wallCheck)
        {
            stateMachine.ChangeState((player.groundCheck) ? player.idleState : player.wallSlideState);
        }
        if (stateTimer < 0)
        {
            stateMachine.ChangeState((player.groundCheck) ? player.idleState : player.fallState);
        }
    }
}
