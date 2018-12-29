using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene {
    protected bool Inited;
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
    public abstract void LeaveScene();
    //进入场景
    public void EnterScene()
    {
        _CurDir.EnterScene();
    }
}
