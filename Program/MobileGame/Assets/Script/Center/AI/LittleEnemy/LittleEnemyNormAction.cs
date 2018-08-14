using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyNormAction : EnemyAction {

	public LittleEnemyNormAction(EnemyActor InActor,AICtrler InCtrler):base(InActor, InCtrler)
    {
        //
    }

    public override void LogicUpdate()
    {
        if( !_Actor.AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
        {
            _Ctrler.PopAIStack();
        }
    }

    public override void Start()
    {
        _EnemyActor.AnimCtrl.SetTrigger("attack_Close_PT_2");
        _EnemyActor.FaceTo(_Ctrler.CurTarget.transform.position - _Actor.transform.position);
    }
}
