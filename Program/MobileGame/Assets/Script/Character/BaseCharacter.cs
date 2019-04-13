using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
[Serializable]
public struct CharacterData
{
    public Propty Propty;
    public Vector3 Position;
    public Vector3 Scale;

    public void WriteCharacter( BaseCharacter character )
    {
        Propty = character.Propty;
        Position = character.Actor.transform.position;
        Scale = character.Actor.transform.localScale;
    }
    public void SetCharacter( BaseCharacter character )
    {
        character.Actor.transform.position = Position;
        character.Actor.transform.localScale = Scale;
        character.Propty = Propty;
    }
}

public class BaseCharacter {
    #region 内部属性
    private LinkedList<BaseBuff> m_BuffList;
    #endregion
    #region 流程
    public BaseCharacter(BaseActorObj actor = null)
    {
        _Actor = actor;
        m_BuffList = new LinkedList<BaseBuff>();
    }
    #endregion
    #region Buff
    public void AddBuff(BaseBuff buff)
    {
        m_BuffList.AddFirst(buff);
        buff.Start();
    }
    public void RemoveBuff(BaseBuff buff)
    {
        m_BuffList.Remove(buff);
        buff.End();
    }
    public BaseBuff GetBuffByType(BuffType type)
    {
        foreach( BaseBuff buff in m_BuffList )
        {
            if( buff.type == type )
            {
                return buff;
            }
        }
        return null;
    }
    #endregion
    #region 注册事件
    Dictionary<string, Action> _Event;
    public void RegistDeath(string name, Action action)
    {
        _Event[name] += action;
    }
    #endregion

    #region 属性以及对外接口
    //public Event 
    public event Action DeathEvent;
    Propty _Propty;
    public Propty Propty
    {
        get
        {
            return _Propty;
        }
        set
        {
            _Propty = value;
        }
    }
    BaseActorObj _Actor;
    public BaseActorObj Actor
    {
        get
        {
            return _Actor;
        }
        set
        {
            _Actor = value;
        }
    }
    #endregion
    
    public void OnDeath()
    {
        if(DeathEvent!=null)
            DeathEvent();
    }
}
