
using System;
using System.Collections;
using UnityEngine;

public class EntityStatusHandler: MonoBehaviour
{
    private ElementType elementTypeCur;
    private Entity entity;
    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public bool CanBeApplied(ElementType elementType)
    {
        return elementTypeCur == ElementType.None;
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        Debug.Log($"ApplyChillEffect duration {duration} slowMultiplier {slowMultiplier}");
        
        // elementTypeCur = ElementType.Ice;
        
        entity.SlowDownEntity(duration, slowMultiplier);
    }


}
