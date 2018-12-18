using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBaseAction : BaseAction {
    protected EnemyObj _NPCActor;
    protected BaseAICtrler _AICtrler;
	// Use this for initialization
	public NpcBaseAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo) {
        _NPCActor = baseActorObj as EnemyObj;
        _AICtrler = _NPCActor.AICtrler;
    }
}
