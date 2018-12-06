using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour {
    protected EnemyObj _Actor;
    protected BaseAICtrler _Ctrler;
    protected ActionCtrler _ActionCtrler;
    public BaseBehaviour(EnemyObj Enemy, BaseAICtrler ctrler)
    {
        _Actor = Enemy;
        _Ctrler = ctrler;
        _ActionCtrler = _Actor.ActionCtrl;
    }
    public void Update(  )
    {
    }
    protected virtual void _CompleteBehaviour()
    {
        
    }
}
