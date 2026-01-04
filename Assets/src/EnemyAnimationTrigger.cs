using UnityEngine;

public class EnemyAnimationTrigger : EntityAnimationTrigger
{
    private Enemy enemy;
    private EnemyVFX enemyVfx;
    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<EnemyVFX>();
    }

    private void EnableCounterWindow()
    {
        enemy.EnableCounterWindow(true);
        enemyVfx.EnableAttackAlert(true);
    }

    private void DisableCounterWindow()
    {
        enemy.EnableCounterWindow(false);
        enemyVfx.EnableAttackAlert(false);

    }
}
