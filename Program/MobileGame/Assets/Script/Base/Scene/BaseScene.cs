using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene {
    SceneMgr _SceneMgr;
    BaseDir _CurDir;
    public BaseDir CurDIr
    {
        get
        {
            return _CurDir;
        }
    }
    public BaseScene( BaseDir dir)
    {
        _CurDir = dir;
        _SceneMgr = SceneMgr.Mgr;
    }
    public bool IsSceneReady=true;
    //离开场景
    public virtual void LeaveScene()
    {
        _CurDir.Leave();
    }
    //进入场景
    public virtual void EnterScene()
    {
        _CurDir.EnterScene();
    }
}
