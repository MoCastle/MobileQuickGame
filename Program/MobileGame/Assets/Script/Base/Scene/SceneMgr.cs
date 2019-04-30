using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr {
    /*
    #region 对外接口
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
    
    public BaseDir CurDir
    {
        get
        {
            if (_CurScene != null)
                return _CurScene.CurDIr;
            else
                return null;
        }
    }
    #endregion

    SceneMgr()
    {

    }
    
    BaseScene _CurScene;
    public BaseScene CurScene
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
    public void JumpScene(string Name)
    {
        if(_CurScene != null)
        {
            _CurScene.LeaveScene();
        }
        SceneManager.LoadScene(Name);
    }*/
}
