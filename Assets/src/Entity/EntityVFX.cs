using System;
using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Coroutine onDamageVfxCoroutine;
    private Entity entity;

    [Header("Element Colors")] [SerializeField]
    private Color chillVfxColor = Color.cyan;

    [SerializeField] private Color fireVfxColor = Color.red;
    [SerializeField] private Color electrifyVfxColor = Color.yellow;
    private Color originalColor = Color.white;

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
        originalColor = hitVfxColor;
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

    public void UpdateOnHitColor(ElementType elementType)
    {
        if (elementType == ElementType.Fire)
        {
            hitVfxColor = fireVfxColor;
        }
        else if (elementType == ElementType.Ice)
        {
            hitVfxColor = chillVfxColor;
        }
        else if (elementType == ElementType.Lightning)
        {
            hitVfxColor = electrifyVfxColor;
        }
        else
        {
            hitVfxColor = originalColor;
        }
    }
}