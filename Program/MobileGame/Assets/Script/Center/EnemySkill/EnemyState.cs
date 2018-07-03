using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : BaseState
{
    public override int Layer
    {
        get
        {
            return 1 << LayerMask.NameToLayer("Player");
        }
    }
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }
    public EnemyState(EnemyActor actor) : base(actor)
    {
    }
}
