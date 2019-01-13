using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public BaseCharacter(BaseActorObj actor =null)
    {
        _Actor = actor;
    }
    public void OnDeath()
    {
        if(DeathEvent!=null)
            DeathEvent();
    }
}
