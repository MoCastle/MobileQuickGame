using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFlyAction : HurtAction {

	public ClickFlyAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        if (_Effect.MoveVector.y < 0)
        {
            _ActorObj.ActionCtrl.PlayerAnim("Falling");
        }
    }
    public override void Update()
    {
        base.Update();
        if (_ActorObj.PhysicCtrl.GetSpeed.y < 0.5f)
        {
            _ActorObj.PhysicCtrl.CopyData();
            _ActorObj.ActionCtrl.AnimSpeed = 1;
        }
        Debug.Log(_ActorObj.PhysicCtrl.GetSpeed);
    }
    public override void CompleteFunc()
    {
        _ActorObj.PhysicCtrl.ResetData();
        base.CompleteFunc();
        
    }
}
