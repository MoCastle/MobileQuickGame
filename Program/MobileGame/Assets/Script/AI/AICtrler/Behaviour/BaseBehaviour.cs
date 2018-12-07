using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour {
    public BaseBehaviour(EnemyObj enemy, BaseAICtrler ctrler)
    {
        _Actor = enemy;
        _AICtrler = ctrler;
        _ActionCtrler = _Actor.ActionCtrl;
        _Inited = false;
    }

    protected EnemyObj _Actor;
    protected BaseAICtrler _AICtrler;
    protected ActionCtrler _ActionCtrler;
    protected bool _Inited;
    public bool Inited
    {
        get
        {
            return _Inited;
        }
    }
    
    public virtual void Update(  )
    {
        if(!_Inited)
        {
            OnStartBehaviour();
            //可初始化检测必须放在进入行为前 避免进入的设置行为做两次
            SetInit();
            if(Inited)
                InitFunc();
                
        }
        
    }
    //检测是否可进行初始化
    protected virtual void SetInit()
    {
        _Inited = true;
    }
    protected abstract void InitFunc();
    public virtual void ComplteteBehaviour()
    {
        if (_Inited == true)
        {
            _Inited = false;
        }  
    }
    #region 事件
    public virtual void OnStartBehaviour()
    {
        
    }
    public virtual void OnSwitchAnime()
    {

    }
    #endregion
}
