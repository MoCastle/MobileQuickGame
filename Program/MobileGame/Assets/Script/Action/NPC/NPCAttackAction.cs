using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NPCAttackAction : NpcBaseAction
{

    public NPCAttackAction(BaseActorObj baseActorObj, SkillInfo skillInfo) : base(baseActorObj, skillInfo)
    {
        npcActor = baseActorObj as EnemyObj;
    }
}
