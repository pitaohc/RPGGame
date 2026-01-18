using UnityEngine;
public class EntityStat: MonoBehaviour
{
    [SerializeField] private Stat vitality;
    [SerializeField] private Stat maxHealth;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = vitality.GetValue() * 5;
        return baseHp + bonusHp;
    }
}
