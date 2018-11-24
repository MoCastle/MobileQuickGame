using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyNormAction : EnemyAction {

    string AnimName;
    string Triggername;
	public LittleEnemyNormAction(EnemyActor InActor,AICtrler InCtrler,string InAnimeName,string InTriggerName):base(InActor, InCtrler)
    {
        //
        AnimName = InAnimeName;
        Triggername = InTriggerName;
    }

    public override void LogicUpdate()
    {
        if( !_Actor.AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            _Ctrler.PopAIStack();
        }
    }

    public override void Start()
    {
        _EnemyActor.AnimCtrl.SetTrigger(Triggername);
        if(_Ctrler.CurTarget!=null)
        {
            _EnemyActor.FaceTo(_Ctrler.CurTarget.transform.position - _Actor.transform.position);
        }
        
    }
}
