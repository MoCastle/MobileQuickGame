using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class AttackBehaviour : BaseBehaviour {
    BaseActorObj Target;
    string TriggerName;
	// Use this for initialization
    public AttackBehaviour(EnemyObj enemy, BaseAICtrler ctrler,string setParam):base(enemy, ctrler)
    {
        TriggerName = setParam;

    }
    public override void Update()
    {
        base.Update();
        if(!Inited)
        {
            return;
        }
        if(Target == null)
        {
            _AICtrler.CompleteTarget();
            ComplteteBehaviour();
            return;
        }
        if (!_ActionCtrler.IsName("Attack"))
        {
            ComplteteBehaviour();
        }
        _ActionCtrler.CurAction.InputDirect(Target.transform.position -_Actor.transform.position );
    }
    public override void ComplteteBehaviour()
    {
        base.ComplteteBehaviour();
        _AICtrler.RemoveHead();
    }
    public override void OnStartBehaviour()
    {
        //因为用了Trigger 避免重复设置 这里再判断一次
        SetInit();
        if(!Inited)
        {
            base.OnStartBehaviour();
            _ActionCtrler.SetTriiger(TriggerName);
        }
    }
    protected override void SetInit()
    {

        if (!_Inited && _ActionCtrler.IsName("Attack"))
        {
            _Inited = true;
        }
    }

    protected override void InitFunc()
    {
        Target = _AICtrler.TargetActor;
    }
}
