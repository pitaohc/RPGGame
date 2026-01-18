using UnityEngine;
public class EntityStat: MonoBehaviour
{
    [SerializeField] private Stat maxHealth;
    [SerializeField] private StatMajorGroup major;
    [SerializeField] private StatOffenceGroup offence;
    [SerializeField] private StatDefenceGtoup defence;

    public float GetMaxHealth()
    {
        float baseHp = maxHealth.GetValue();
        float bonusHp = major.vitality.GetValue() * 5;
        return baseHp + bonusHp;
    }
}
