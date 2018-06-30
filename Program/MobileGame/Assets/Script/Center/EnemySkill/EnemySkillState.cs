using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : BaseState {
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    public EnemySkillState( PlayerActor actor ):base( actor )
    {
    }
    public virtual bool InEffectArea( Vector2 InPs )
    {
        return false;
    }

    public virtual Vector2 GetMoveToPs( Vector2 InPs )
    {
        return Vector2.zero;
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
