using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAiCtrl : AICtrler
{
    public GuardAiCtrl(EnemyActor InEnemyActor) : base(InEnemyActor)
    {

    }

    public override void GenAction()
    {
        int Percent = Random.Range(0, 100);
        if (Percent > 50)
        {
            PushAction(new CruiteAct(Actor, this));
        }
        else
        {
            PushAction(new CruiteAct(Actor, this));
        }
    }

    public override void LogicUpdate()
    {
        BaseActor Target = Actor.CheckGetPlayer();
        if (Target != null)
        {
            Actor.EnterBattle();
            Actor.AICtrl.CurTarget = Target;
        }
    }
}
