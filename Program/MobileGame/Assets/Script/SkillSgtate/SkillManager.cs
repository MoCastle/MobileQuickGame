using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillInfo
{
    public int ID;
    [Header("受击硬直 秒")]
    public float BeAttackHardTime;
    [Header("攻击硬直 秒")]
    public float AttackHardTime;
    [Header("击退值")]
    public float HitFlyValue;
    [Header("伤害")]
    public float Damage;
    [Header("击飞方向")]
    public Vector2 HitFlyDirection;
    [Header("受击效果")]
    public HitEffectType HitType;
    [Header("击打特效")]
    public string EffectName;
    [Header("消耗体力")]
    public float CostVIT;
}
