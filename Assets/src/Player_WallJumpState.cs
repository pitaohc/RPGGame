using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Player_WallJumpState : EntityState
{
    public Player_WallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Player_WallJumpState" + player.faceDirection);
        player.SetVelocity(player.wallJumpForce.x * -player.faceDirection, player.wallJumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        if (player.wallCheck)
        {
            // TODO: here is a bug, when jump leave the wall, the player is also facing the wall.
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (player.rb.linearVelocityY < 0)
        {
            //Debug.Log("player.fallState");
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Exit Player_WallJumpState");
    }
}
