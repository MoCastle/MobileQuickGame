using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public enum BuffType
{
    Hit,
    HitFly
}

public abstract class BaseBuff
{
    float m_StartTime;
    float m_LastTime;
    protected BaseActorObj m_Actor;
    BuffType m_Type;

    public BuffType type
    {
        get
        {
            return m_Type;
        }
    }
    public BaseBuff(BuffType type)
    {
        m_StartTime = Time.time;
        m_LastTime = 0;
        m_Type = type;
    }
    public virtual void Init(float lastTime)
    {
        m_LastTime = lastTime;
    }
    public virtual void AddToActor(BaseActorObj Actor)
    {
        BaseBuff curBuff = Actor.GetBuffByType(BuffType.Hit);
        if (curBuff != null)
            curBuff.RemoveSelf();
        m_Actor = Actor;
        Actor.AddBuff(this);
    }
    public abstract void Start();
    public abstract void End();
    public virtual void Update()
    {
        if (Time.time - m_StartTime - m_LastTime > 0)
        {
            this.RemoveSelf();
        }
    }
    public void RemoveSelf()
    {
        if(m_Actor!=null)
            m_Actor.RemoveBuff(this);
    }
}
