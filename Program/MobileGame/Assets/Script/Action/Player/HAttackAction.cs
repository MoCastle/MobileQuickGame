using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class HAttackAction : PlayerAction
{
    protected override Vector2 MoveDir
    {
        get
        {
            return Vector2.up;
        }
    }
    // Use this for initialization
    public HAttackAction(BaseActorObj baseActorObj, SkillInfo skillInfo):base(baseActorObj, skillInfo)
    {

    }
}
