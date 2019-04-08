using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NPCFlyItemAction : BaseAction {
    protected float _CountTime = 0;
    float _Time = 5;
    Vector2 _Dir;
    protected override Vector2 MoveDir
    {
        get
        {
            return _Dir;
        }
    }
    // Use this for initialization
    public NPCFlyItemAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        _CountTime = Time.time + _Time;
        _Dir = baseActorObj.transform.rotation * Vector3.right;
    }
    public override void Update()
    {
        base.Update();
        if(Time.time > _CountTime)
        {
            m_ActorObj.ActionCtrl.SetTriiger("Death");
        }
    }

}
