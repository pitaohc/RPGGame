using System;
using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Coroutine onDamageVfxCoroutine;
    private Entity entity;

    [Header("On Taking Damage VFX")] [SerializeField]
    private Material onDamageMaterial;

    [SerializeField] private float onDamageVfxDuration = .2f;

    [Header("On Doing Damage VFX")] [SerializeField]
    private GameObject hitVfx;

    [SerializeField] private GameObject critHitVfx;
    [SerializeField] private Color hitVfxColor = Color.white;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        entity = GetComponent<Entity>();
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
        {
            StopCoroutine(onDamageVfxCoroutine);
        }

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        Material originalMaterial = sr.material;
        sr.material = onDamageMaterial;
        yield return new WaitForSeconds(onDamageVfxDuration);
        sr.material = originalMaterial;
    }

    public void CreateOnHitVfx(Transform target, bool isCrit)
    {
        GameObject go = isCrit ? critHitVfx : hitVfx;
        GameObject vfx = Instantiate(go, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
        vfx.transform.Rotate(0, entity.IsFaceRight() ? 0 : 180, 0);
    }
}