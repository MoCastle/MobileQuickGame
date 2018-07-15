using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Propty {
    //生命
    public int Life;
    public int MaxLife;
    public float PercentLife
    {
        get
        {
            if (MaxLife == 0)
            {
                return 0;
            }
            float Percent = (float)Life / (float)MaxLife;
            Percent = Percent > 0.001f && Percent < 0.1f ? 0.1f : Percent;
            return Percent;
        }
    }
    public int DeDuctLife( int InLife )
    {
        Life = Life > InLife? (Life - InLife):0;
        return Life;
    }
    public int ModLife(int InLife)
    {
        Life = Life + InLife > MaxLife ? (InLife + InLife) : MaxLife;
        return Life;
    }

    //体力
    public int VIT;
    public int MaxVIT;
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
    public int Attack;
}
