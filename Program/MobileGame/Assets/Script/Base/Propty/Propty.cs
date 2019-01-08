using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Propty {
    [Title("属性", "black")]
    public ActorInfo ActorInfo;
    [Title("当前生命值", "black")]
    [SerializeField]
    float _Life;
    float _CurLife
    {
        get
        {
            return _Life;
        }set
        {
            //最大生命值小于等于0则不会有血量变化
            if(ActorInfo.Life<=0)
            {
                return;
            }
            _Life = value;
            if (_Life < 0)
            {
                _Life = 0;
            }
            else if (_Life > ActorInfo.Life)
            {
                _Life = ActorInfo.Life;
            }

            if (_Life <= 0)
            {
                _IsDeath = true;
            }
        }
    }
    public float CurLife
    {
        get
        {
            return _CurLife;
        }
    }
    public float PercentLife
    {
        get
        {
            //最大血量小于0是不扣血的
            if (ActorInfo.Life <= 0)
            {
                return 1;
            }
            float Percent = (float)_CurLife / (float)ActorInfo.Life;
            Percent = Percent > 0.001f && Percent < 0.1f ? 0.1f : Percent;
            return Percent;
        }
    }
    bool _IsDeath;
    public bool IsDeath
    {
        get
        {
            return _IsDeath;
        }
    }
    #region
    //复活时候用
    public void ResetPropty()
    {
        _CurLife = ActorInfo.Life;
        _IsDeath = false;
    }
    public void ModLifeValue(float modValue)
    {
        //小于等于0就是没有血量
        if(ActorInfo.Life<=0)
        {
            return;
        }
        _CurLife += modValue;
        
    }
    #endregion
    public float Heavy
    {
        get
        {
            return ActorInfo.Heavy;
        }
    }
    public float HeavyRate
    {
        get
        {
            return ActorInfo.HeavyRate;
        }
    }

    //移动速度
    public float MoveSpeed
    {
        get
        {
            return ActorInfo.Speed;
        }
    }
}
