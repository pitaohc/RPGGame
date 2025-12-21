using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocityY);
        if (player.moveInput.x == 0 ||
            player.wallDetected)
            stateMachine.ChangeState(player.idleState);


    }
}
