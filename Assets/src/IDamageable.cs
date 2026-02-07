using UnityEngine;

public interface IDamageable
{
    public bool TakeDamage(float damage,float elementDamage, ElementType elementType, Transform damageDealer);
}
