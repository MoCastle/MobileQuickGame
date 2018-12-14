using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAction : BaseAction {

    // Use this for initialization
    public DeathAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo) {
        GamePoolManager.Manager.Despawn(_ActorObj.transform);
    }

    public override void CompleteFunc()
    {
        base.CompleteFunc();
        
    }
}
