using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子类负责标记目标层级 记录目标角色
public abstract class EnemyState : BaseState
{
    public EnemyActor Actor;
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
        Actor = actor;
    }
}
