using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class RocketAction : HAttackAction {

	public RocketAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo)
    { }
    public override void SetStartDirection()
    {
        SetFaceLock(true);
        base.SetStartDirection();
    }
}
