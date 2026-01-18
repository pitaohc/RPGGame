using System;
using UnityEngine;


[Serializable]
public class StatOffenceGroup
{
    // Physical Damage
    public Stat damage; // 伤害
    public Stat critPower; // 暴击伤害
    
    // Elemental Damage
    public Stat critChange; // 暴击概率
    public Stat fireDamage; // 火焰伤害
    public Stat iceDamage; // 冰冻伤害
    public Stat lightingDamage; // 光明伤害
}
