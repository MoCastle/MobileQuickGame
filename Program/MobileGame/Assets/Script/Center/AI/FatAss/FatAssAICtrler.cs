using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatAssAICtrler : AICtrler
{

    // Use this for initialization
    public override void LogicUpdate()
    {

    }

    public override void EnterBattle()
    {
        throw new System.NotImplementedException();
    }

    public FatAssAICtrler(EnemyActor InActor) : base(InActor)
    {

    }
}
