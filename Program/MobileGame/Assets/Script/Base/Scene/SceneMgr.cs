using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr {

    static SceneMgr _Mgr;
    public static SceneMgr Mgr
    {
        get
        {
            if(_Mgr==null)
            {
                _Mgr = new SceneMgr();
            }
            return _Mgr;
        }
    }


    SceneMgr()
    {

    }

    public void JumpScene(string sceneName)
    {

    }
    /*
    BaseDir _CurDir;
    BaseDir CurDir
    {
        get
        {
            return _CurDir;
        }
    }*/
    BaseScene _CurScene;
    BaseScene CurScene
    {
        get
        {
            return _CurScene;
        }
    }
    public void EnterScene(BaseScene scene)
    {
        _CurScene = scene;
        CurScene.EnterScene();
    }
}
