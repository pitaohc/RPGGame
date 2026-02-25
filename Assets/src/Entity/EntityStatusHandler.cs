
using System;
using System.Collections;
using UnityEngine;

public class EntityStatusHandler: MonoBehaviour
{
    private ElementType elementTypeCur;
    private Entity entity;
    private EntityVFX entityVfx;
    private EntityStat stat;
    private Coroutine chilledEffectCo;
    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<EntityVFX>();
        stat = GetComponent<EntityStat>();
    }

    public bool CanBeApplied(ElementType elementType)
    {
        return elementTypeCur == ElementType.None;
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        duration *= 1 - stat.GetElementalResistance(ElementType.Ice);
        entity.SlowDownEntity(duration, slowMultiplier);
        ChilledEffect(duration);
    }

    private void ChilledEffect(float duration)
    {
        if (chilledEffectCo != null)
        {
            StopCoroutine(chilledEffectCo);
        }
        chilledEffectCo = StartCoroutine(ChilledEffectCo(duration));
    }
    
    private IEnumerator ChilledEffectCo(float duration)
    {
        elementTypeCur = ElementType.Ice;
        entityVfx?.PlayStatusVfx(duration,ElementType.Ice);
        
        yield return new WaitForSeconds(duration);
        elementTypeCur = ElementType.None;
    }
}
