using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class LeaveAction : BaseAction
{
    LeaveAction(BaseActorObj baseActorObj, SkillInfo skillInfo) : base(baseActorObj, skillInfo)
    {
        baseActorObj.LeaveComplete();
    }
}
