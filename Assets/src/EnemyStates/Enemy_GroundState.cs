using UnityEngine;

public class Enemy_GroundState : EnemyState
{
    public Enemy_GroundState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetection() == true)
        {
            //Debug.Log("to battle state");
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
