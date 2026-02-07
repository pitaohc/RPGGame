using UnityEngine;

public interface IDamageable
{
    public bool TakeDamage(float damage,float elementDamage, Transform damageDealer);
}
