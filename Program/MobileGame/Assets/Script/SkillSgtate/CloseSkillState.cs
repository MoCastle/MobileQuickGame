using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSkillState : PlayerState
{

    public CloseSkillState(PlayerActor InaActor) : base(InaActor) { }

    public override void Update()
    {
    }
    public override void Attacking()
    {
        _Actor.MovePs(Direction * _Actor.CAttackMove);
        
    }
}
