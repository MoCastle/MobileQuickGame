using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class GameCtrl
{
    //暂停功能
    #region
    public bool _IsPaused = false;
    public bool IsPaused
    {
        get
        {
            return _IsPaused;
        }
    }

    //暂停/继续 操作
    public void SwitchPause()
    {
        _IsPaused = !IsPaused;
        Time.timeScale = _IsPaused ? 0 : 1;
    }

    #endregion

    static GameCtrl _GameCtrler;
    static public GameCtrl GameCtrler
    {
        get
        {
            if (_GameCtrler == null)
            {
                _GameCtrler = new GameCtrl();
            }
            return _GameCtrler;
        }
    }

    #region
    CenceCtrl CenceCtroler;
    #endregion
    GameCtrl()
    {
        CenceCtroler = new CenceCtrl();
    }

    SpawnPool _Pool;
    public SpawnPool Pool
    {
        get
        {
            return _Pool;
        }
        set
        {
            if (_Pool == null)
            {
                _Pool = value;
            }
        }
    }

    BaseDir _CurDir;
    public BaseDir CurDir
    {
        get
        {
            return _CurDir;
        }
        set
        {
            _CurDir = value;
        }
    }
}
