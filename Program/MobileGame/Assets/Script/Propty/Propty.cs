using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Propty
{
    #region 属性
    [Title("资源名")]
    public string m_Name;
    [Header("移动速度")]
    public float m_Speed;
    [Header("最大生命值")]
    public int MaxLife;
    [Title("当前生命值", "black")]
    [SerializeField]
    int m_Life;
    #endregion

    #region 接口
    int CurLife
    {
        get
        {
            return m_Life;
        }
        set
        {
            m_Life = value;
            if (m_Life < 0)
            {
                m_Life = 0;
            }
            if (m_Life > MaxLife)
            {
                m_Life = MaxLife;
            }
        }
    }
    bool isDead
    {
        get
        {
            return CurLife <= 0.0001;
        }
    }
    public float percentLife
    {
        get
        {
            //最大血量小于0是不扣血的
            if (MaxLife <= 0)
            {
                return 1;
            }
            float Percent = (float)CurLife / (float)MaxLife;
            Percent = Percent > 0.001f && Percent < 0.1f ? 0.1f : Percent;
            return Percent;
        }
    }
    public bool IsDeath
    {
        get
        {
            return CurLife <= 0;
        }
    }
    public string name
    {
        get
        {
            return m_Name;
        }
        set
        {
            m_Name = value;
        }
    }
    #endregion

    #region
    //复活时候用
    public void ResetPropty()
    {
        CurLife = MaxLife;
    }

    /// <summary>
    /// 将配置初始化为设置状态
    /// </summary>
    public void InitPropty()
    {
        CurLife = CurLife > 0 ? CurLife : MaxLife;
    }
    public void ModLifeValue(float modValue)
    {
        //小于等于0就是没有血量
        if (isDead)
        {
            return;
        }
        modValue = Mathf.Ceil(modValue);
        CurLife += (int)modValue;
    }
    #endregion

    //移动速度
    public float moveSpeed
    {
        get
        {
            return m_Speed;
        }
    }
}
