using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingAction : PlayerAction {
    //重力系数缩减
    float _FallingGravity = 3;
    //空中停顿时间
    float _FallingTime = 1f;
    

    float _CountFallingTime = 0;
    float _GravityScale;
	// Use this for initialization
	public ChargingAction(BaseActorObj actionCtrler, SkillInfo skillInfo):base(actionCtrler,skillInfo) 
    {
        _CountFallingTime = Time.time + _FallingTime;
        _GravityScale = actionCtrler.PhysicCtrl.Gravity;
        actionCtrler.PhysicCtrl.Gravity = _FallingGravity;
    }

    // Update is called once per frame
    public override void LogicUpdate()
    {
        if (_CountFallingTime != 0 && _CountFallingTime > Time.time)
        {
            _ActorObj.PhysicCtrl.SetSpeed(0);
        }
    }
    public override void CompleteFunc()
    {
        base.CompleteFunc();
        _ActorObj.PhysicCtrl.Gravity = _GravityScale;
    }
    public override void InputNormInput(NormInput curInput)
    {
        InputInfo info = curInput.InputInfo;
        Vector2 inputDir = info.Shift;
        if( Mathf.Abs(inputDir.x)>0&& Mathf.Abs(inputDir.y/ inputDir.x) < 1)
        {
            info.Shift.y = 0;
            curInput = new NormInput(info, curInput.LifeTime, curInput.Gesture);
        }
        base.InputNormInput(curInput);

    }
}
