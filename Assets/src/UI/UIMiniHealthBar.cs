using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMiniHealthBar : MonoBehaviour
{
    private EntityHealth entityHealth;
    private Entity entity;
    private Slider healthbar;
    private void OnEnable()
    {
        entity = GetComponentInParent<Entity>();
        entityHealth = entity.GetComponent<EntityHealth>();
        healthbar = GetComponentInChildren<Slider>();

        entity.onFlip += HandleFlip;
        entityHealth.onHealthChanged += HandleHealthUpdate;

        HandleHealthUpdate();
    }

    private void OnDisable()
    {
        entity.onFlip -= HandleFlip;
        entityHealth.onHealthChanged -= HandleHealthUpdate;
    }


    void HandleFlip()
    {
        transform.rotation = Quaternion.identity;
    }

    void HandleHealthUpdate()
    {
        healthbar.value = entityHealth.GetHealthRate();
    }
}
