using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlyKnifeHurtAction : NPCFlyItemAction
{
    float _Time = 2f;
    Vector2 _Dir;
    protected override Vector2 MoveDir
    {
        get
        {
            return _Dir;
        }
    }
    // Use this for initialization
    public NPCFlyKnifeHurtAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo) {
        _CountTime = Time.time + _Time;
        _Dir = _ActorObj.PhysicCtrl.GetSpeed;
        _Dir.x *= -1;
        Vector2 moveVector = _ActorObj.BeHitEffect.MoveVector;
        _Dir = (_Dir + moveVector.normalized).normalized;
        if(_Dir.x<0.01&&_Dir.y<0.01)
        {
            Debug.Log("ErrorDir");
        }
        _Speed = baseActorObj.ActorPropty.MoveSpeed;
    }
    public override void Update()
    {
        base.Update();
    }
}
