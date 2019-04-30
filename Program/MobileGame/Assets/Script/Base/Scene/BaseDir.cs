/*作者:Mo
 *基础导演类 控制进入场景流程
*/
/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDir : MonoBehaviour {
    public Camera MainCamera;
    public virtual void EnterScene()
    {
        StartPrepare();
    }
    
    protected UIManager _UIMgr;
    protected SceneMgr _SceneMgr;
    //该场景重演
    public void Restart()
    {
        StartPrepare();
    }

    #region 生命周期相关
    private Action _UpdateFunc;
    protected virtual void Awake()
    {
        _UIMgr = UIManager.Mgr;
        _SceneMgr = SceneMgr.Mgr;
    }
    
    //准备阶段
    public virtual Boolean IsPrepared
    {
        get { return true; }
    }

    //开始的准备工作重写并放在这
    protected virtual void StartPrepare()
    {
        _UIMgr.ClearAll();
        this._UpdateFunc = Starting;
    }
    //需要等待的部分放这里
    protected virtual void Starting()
    {
        if(IsPrepared)
        {
            StartComplete();
            this._UpdateFunc = Updating;
        }
    }
    //这里初始化现场设置
    protected virtual void StartComplete()
    {
    }
    //进行中
    protected virtual void Updating()
    { }
    //结束
    //离开准备已做好
    public virtual Boolean IsLeaved
    {
        get { return true; }
    }
    public virtual void Leave()
    {
        this._UpdateFunc = Leaving;
    }
    
    protected virtual void Leaving()
    {
        if(IsLeaved)
        {
            LeaveComplete();
            _UpdateFunc = null;
        }
    }
    protected virtual void LeaveComplete()
    {
    }
    private void Update()
    {
        if (_UpdateFunc != null)
        {
            _UpdateFunc();
        }
    }
    #endregion


}
*/