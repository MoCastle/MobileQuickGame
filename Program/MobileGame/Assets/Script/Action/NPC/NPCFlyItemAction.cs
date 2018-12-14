using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlyItemAction : BaseAction {
    Vector2 _Dir;
    protected override Vector2 MoveDir
    {
        get
        {
            return base.MoveDir;
        }
    }
    // Use this for initialization
    public NPCFlyItemAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo)
    {
        //Vector3 target = 
        //_Dir = ((EnemyActor)baseActorObj)
        _Speed = _ActorObj.ActorPropty.MoveSpeed;
    }
	
	
}
