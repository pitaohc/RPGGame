using System;
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

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * 0.5f;
        float evasionCut = 85f;
        float finalEvasion = Math.Clamp(baseEvasion + bonusEvasion,0,evasionCut);
        return finalEvasion;
    }
}
