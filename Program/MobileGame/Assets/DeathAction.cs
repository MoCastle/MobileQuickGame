using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class DeathAction : BaseAction {

    // Use this for initialization
    public DeathAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo) {
    }

    public override void CompleteFunc()
    {
        base.CompleteFunc();
        
    }
}
