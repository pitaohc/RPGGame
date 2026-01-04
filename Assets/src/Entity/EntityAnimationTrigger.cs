using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Move after the MonoBehaviour is created
    private Entity entity;
    private EntityCombat combat;
    void Start()
    {
    }

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        combat = GetComponentInParent<EntityCombat>();

    }

    private void CurrentStateTrigger()
    {
        //Debug.Log("CurrentStateTrigger");
        entity.CurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        combat.PerformAttack();
    }
}
