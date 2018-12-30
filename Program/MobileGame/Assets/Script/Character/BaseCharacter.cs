﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter {
    #region 属性以及对外接口
    //public Event 
    //public Event Action Death;
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

    }
}
