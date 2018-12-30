using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMgr {
    #region 对外接口
    BaseCharacter _PlayerCharactor;
    public BaseCharacter PlayerCharactor
    {
        get
        {
            return _PlayerCharactor;
        }
        set
        {
            _PlayerCharactor = value;
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
