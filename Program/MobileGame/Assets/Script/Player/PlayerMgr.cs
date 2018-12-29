﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr {
    #region 对外接口
    Propty _PlayerPropty;
    public Propty PlayerPropty
    {
        get
        {
            if(_PlayerPropty == null)
            {
                _PlayerPropty = new Propty();
            }
            return _PlayerPropty;
        }
    }
    public string GetPlayerInfo()
    {
        return "Player";
    }
    #endregion
    static PlayerMgr _Mgr;
    public static PlayerMgr Mgr
    {
        get
        {
            if(_Mgr== null)
            {
                _Mgr = new PlayerMgr();
            }
            return _Mgr;
        }
    }

    PlayerMgr()
    {
    }
}
