using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSkillState : PlayerState
{
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public CloseSkillState(EnemyActor InaActor) : base(InaActor) { }

    public override void Update()
    {
    }
    public override void Attacking()
    {
        _Actor.MovePs(Direction * _Actor.CAttackMove);
        
    }
}
