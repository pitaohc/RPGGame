using UnityEngine;
using Random = UnityEngine.Random;


public class Chest : MonoBehaviour, IDamageable
{
    private static int animIdChestOpen = Animator.StringToHash("chestOpen");
    private Animator anim;
    private Rigidbody2D rb;
    [Header("Open Details")]
    [SerializeField] private float randomRotateRange = 100f;
    [SerializeField] private Vector2 knockback = new(1, 5);
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage, Transform damageDealer)
    {
        anim.SetBool(animIdChestOpen, true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-randomRotateRange, randomRotateRange);

    }
}
