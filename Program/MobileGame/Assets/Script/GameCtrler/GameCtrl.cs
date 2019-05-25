using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class GameCtrl
{
    #region 部件
    GameDriver _GameDriver;
    SpawnPool _Pool;
    public SpawnPool Pool
    {
        get
        {
            return _Pool;
        }
    }
   // SceneMgr _SceneMgr;
    #endregion
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

    //场景功能
    #region

    #endregion
    GameCtrl()
    {
        //CenceCtroler = new CenceCtrl();
        GameObject modelCtrler = Resources.Load("Prefab\\Base\\GameControler") as GameObject;
        if (modelCtrler != null)
        {
            GameObject driverObj = GameObject.Instantiate(modelCtrler);
            _GameDriver = driverObj.GetComponent<GameDriver>();
            _Pool = driverObj.GetComponent<SpawnPool>();
        }
        //_SceneMgr = SceneMgr.Mgr;
    }
    public void Update()
    {
    }
}