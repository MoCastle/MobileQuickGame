using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction {
    protected BaseActor _Actor;
    protected AICtrler _Ctrler;
    public AIAction(BaseActor InActor, AICtrler InCtrler)
    {
        _Actor = InActor;
        _Ctrler = InCtrler;
    }
    public void Update()
    {
        if(!IsInited)
        {
            Start();
            IsInited = true;
        }else
        {
            LogicUpdate();
        }
    }
    public abstract void LogicUpdate();
    public abstract void Start();
    public bool IsInited;
}
