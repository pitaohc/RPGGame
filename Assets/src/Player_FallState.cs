using UnityEngine;

public class Player_FallState : Player_AiredState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        // if player detecting the ground below, change state to grounded
        // should not use velocity to detect ground, use raycast
        // because there is delay between animations change
        //if (rb.linearVelocityY > -float.Epsilon)
        //{
        //    EntityState nextState =
        //        rb.linearVelocityX == 0 ?
        //        player.idleState :
        //        player.moveState;
        //    stateMachine.ChangeState(nextState);
        //    Debug.Log("change to grounded");
        //}
        if (player.groundCheck)
        {
            EntityState nextState =
                rb.linearVelocityX == 0 ?
                player.idleState :
                player.moveState;
            stateMachine.ChangeState(nextState);
        }
        if (player.wallCheck)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
