using UnityEngine;

public class EntityStat : MonoBehaviour
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

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
        float evasionCut = 85f;
        float finalEvasion = Mathf.Clamp(baseEvasion + bonusEvasion, 0, evasionCut);
        return finalEvasion;
    }

    public float GetPhysicalDamage(out bool isCrit)
    {
        // 计算总伤害
        float baseDamage = offence.damage.GetValue();
        float strength = major.strength.GetValue();
        float bonusDamage = strength * 1.0f;
        float totalBaseDamage = baseDamage + bonusDamage;
        // 计算暴击率
        // [0,100]
        float baseCritChance = offence.critChange.GetValue();
        float agility = major.agility.GetValue();
        float bonusCritChance = agility * 0.3f;
        float totalCritChance = baseCritChance + bonusCritChance;
        float critChanceCut = 85;
        totalCritChance = Mathf.Clamp(totalCritChance, 0, critChanceCut);
        // 计算暴击加成
        float baseCritPower = offence.critPower.GetValue();
        float bonusCritPower = strength * 0.5f;
        float totalCritPower = (baseCritPower + bonusCritPower) / 100;
        // 计算最终伤害
        bool critted = Random.Range(0, 100) < totalCritChance;
        float finalDamage = totalBaseDamage * (critted ? totalCritPower : 1);

        Debug.LogFormat(
            "totalBaseDamage={0:F2}, totalCritChance={1:F2}, totalCritPower={2:F2}, critted={3}, finalDamage={4:F2}",
            totalBaseDamage, totalCritChance, totalCritPower, critted, finalDamage);
        isCrit = critted;
        return finalDamage;
    }

    public float GetArmorMitigation()
    {
        // TODO GetArmorMitigation
        float baseArmor = defence.armor.GetValue();
        float bonusArmor = major.vitality.GetValue() * 1.0f;
        float totalArmor = baseArmor + bonusArmor;

        float mitigation = totalArmor / (totalArmor + 100);
        const float mitigationCut = 85;
        mitigation = Mathf.Clamp(mitigation, 0, mitigationCut);
        return mitigation;
    }

    public float GetArmorReduction()
    {
        // 100 -> 1
        float baseReduction = offence.armorReduction.GetValue();
        return baseReduction / 100;
    }
}