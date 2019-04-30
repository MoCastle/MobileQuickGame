using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

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
        _Dir = m_ActorObj.Physic.MoveSpeed.normalized;
        _Dir.x *= -1;
        _Dir = _Dir.normalized;
        Vector2 moveVector = m_ActorObj.BeHitEffect.MoveVector;
        moveVector = moveVector.normalized;
        _Dir.x = Mathf.Abs(moveVector.x) > 0 ? moveVector.x : _Dir.x;
        _Dir.y = Mathf.Abs(moveVector.y) > 0 ? moveVector.y : _Dir.y;
        _Dir = _Dir.normalized;
        m_HSpeed = baseActorObj.propty.moveSpeed;
    }
    public override void Update()
    {
        base.Update();
    }
}
