using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();
        // 应该按照输入判断
        // 玩家移动->player.wallCheck为false，此时角色与墙已经有间隔
        // 会出现有贴墙动画不贴墙的效果

        if (!player.wallDetected)
        {
            //player.Flip();
            stateMachine.ChangeState(player.fallState);
        }
        if (input.Player.Jump.WasPressedThisFrame())
        {
            //player.Flip();
            stateMachine.ChangeState(player.wallJumpState);
        }
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocityY);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, player.inWallSlideMultiplier * rb.linearVelocityY);
            //Debug.Log("player y velocity: " + rb.linearVelocityY);
        }
    }
}
