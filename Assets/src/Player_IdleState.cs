using UnityEngine;

public class Player_IdleState : Player_GroundedState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //rb.linearVelocityX = 0f;
    }

    public override void Update()
    {
        base.Update();
        if (player.moveInput.x != 0 &&
            !input.Player.Jump.WasPerformedThisFrame())
            stateMachine.ChangeState(player.moveState);

        //if (player.input.Player.Jump.WasPressedThisFrame())
        //{
        //    Debug.Log("Jump");
        //    //stateMachine.ChangeState(player.jumpState);
        //}
    }
}
