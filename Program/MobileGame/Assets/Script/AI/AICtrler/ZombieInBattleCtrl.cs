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
        Vector3 Direction = Vector2.left;
        Direction.x *= Random.Range(0f, 1f) > 0.5 ? 1 : -1;
        PushAction(new RunToAction(Actor, this, Direction*10 + Actor.transform.position));
    }

    public override void LogicUpdate()
    {
        //throw new System.NotImplementedException();
    }
}
