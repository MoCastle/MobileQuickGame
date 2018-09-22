using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInBattleCtrl : AICtrler
{
    public ZombieInBattleCtrl(EnemyActor InActor) : base(InActor)
    {
    }

    public override void GenAction()
    {
        PushAction(new WaitingAction(Actor, this, 1f));
        Vector2 Direction = Vector2.left;
        Direction.x *= Random.Range(0, 1) > 0.5 ? 1 : -1;
        PushAction(new RunToAction(Actor, this, Direction*3));
    }

    public override void LogicUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
