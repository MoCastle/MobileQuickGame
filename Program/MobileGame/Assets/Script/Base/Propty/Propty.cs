﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Propty {
    [Title("属性", "black")]
    public ActorInfo _ActorInfo;

    public float Heavy
    {
        get
        {
            return _ActorInfo.Heavy;
        }
    }

    public float HeavyRate
    {
        get
        {
            return _ActorInfo.HeavyRate;
        }
    }

    //生命
    [System.NonSerialized]
    public int HP;
    [Title("最大生命值", "black")]
    public int MaxHP;
    //体力
    [System.NonSerialized]
    public int VIT;
    [Title("最大体力值", "black")]
    public int MaxVIT;
    [Title("可击退", "black")]
    public bool CanHitBack = true;
    [Title("可击飞", "black")]
    public bool CanCickFly = true;
    //移动速度
    [Title("移动速度", "black")]
    public float MoveSpeed;
    //攻击力
    [Title("攻击力", "black")]
    public int Attack;
    //击退速度
    [Title("击退基础速度", "black")]
    public float HitBackSpeed;

    public Propty(  )
    {
        
        NpcProptyReader NpcProptyCfg = NpcProptyReader.Cfg;

        HP = MaxHP;
        VIT = MaxVIT;
        
        Init();
    }
    public void Init()
    {
        HP = MaxHP;
        VIT = MaxVIT;
    }

    public float PercentLife
    {
        get
        {
            if (MaxHP == 0)
            {
                return 0;
            }
            float Percent = (float)HP / (float)MaxHP;
            Percent = Percent > 0.001f && Percent < 0.1f ? 0.1f : Percent;
            return Percent;
        }
    }

    public int DeDuctLife( int DeHp )
    {
        HP = HP > DeHp? (HP - DeHp):0;
        return HP;
    }
    public int ModLife(int ModHp)
    {
        HP = HP + ModHp > MaxHP ? (ModHp + ModHp) : MaxHP;
        return HP;
    }

    
    public float PercentVIT
    {
        get
        {
            if( MaxVIT ==0 )
            {
                return 0;
            }
            float Percent = (float)VIT / (float)MaxVIT;
            Percent = Percent > 0.001f && Percent < 0.1f ? 0.1f : Percent;
            return (float)VIT / (float)MaxVIT;
        }
    }
    public int DeDuctVIT(int InVIT)
    {
        VIT = VIT > InVIT ? (VIT - InVIT) : 0;
        return VIT;
    }
    public int ModVIT(int InVIT)
    {
        VIT = VIT + InVIT > MaxVIT ? MaxVIT:(VIT + InVIT);
        return VIT;
    }
}
