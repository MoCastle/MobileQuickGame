using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlyItemAction : BaseAction {
    Vector2 _Dir;
    protected override Vector2 MoveDir
    {
        get
        {
            return _Dir;
        }
    }
    float _Life = 3f;
    float _CountLife = 0;
    // Use this for initialization
    public NPCFlyItemAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        _CountLife = Time.time + _Life;
        BaseActorObj targetTrans = ((EnemyObj)baseActorObj).AICtrler.TargetActor;
        Vector3 targetPs = targetTrans.transform.position;
        targetPs.y += targetTrans.BodyCollider.offset.y;
        _Dir = (targetPs - baseActorObj.transform.position);
        _Dir = _Dir.normalized;
        if(_Dir.magnitude == 0 )
        {
            _Dir = baseActorObj.FaceDir;
        }
        SetSpeed(baseActorObj.ActorPropty.MoveSpeed);
    }
    public override void Update()
    {
        base.Update();
        if(Time.time > _CountLife)
        {
            CompleteFunc();
        }
    }
    public override void CompleteFunc()
    {
        base.CompleteFunc();
        _ActorObj.ActionCtrl.SetTriiger("Death");
        _ActorObj.ActionCtrl.CurAction = null;
        _ActorObj.ReCollect();
    }
}
