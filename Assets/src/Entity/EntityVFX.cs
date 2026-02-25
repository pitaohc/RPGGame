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

    public void PlayStatusVfx(float duration, ElementType elementType)
    {
        Color color = hitVfxColor;
        if (elementType == ElementType.Ice)
        {
            color = chillVfxColor;
        }
        // Debug.Log($"PlayStatusVfx: duration: {duration} color {color}");

        StartCoroutine(PlayStatusVfxCo(duration, color));
    }

    private IEnumerator PlayStatusVfxCo(float duration, Color effectColor)
    {
        float twinkInterval = .2f;
        float timer = 0f;
        float colorDiff = 0.05f;
        Color highlightColor = (1 + colorDiff) * effectColor;
        Color darkColor = (1 - colorDiff) * effectColor;
        bool toggle = true;
        // Debug.Log("PlayStatusVfxCo");
        while (timer <= duration)
        {
            sr.color = toggle ? highlightColor : darkColor;
            // Debug.Log($"timer: {timer}, color {(toggle ? highlightColor : darkColor)}");
            toggle = !toggle;
            timer += twinkInterval;
            yield return new WaitForSeconds(twinkInterval);
        }

        sr.color = Color.white;
    }
}