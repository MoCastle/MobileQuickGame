using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingAI : BaseAI {
    BaseActor TargetActor;
    public ChasingAI(EnemyActor InActor, BaseActor InTargetActor) : base(InActor)
    {
        TargetActor = InTargetActor;
        Actor.AnimCtrl.SetBool("Run", true);
    }
	// Use this for initialization
    public override void Update( )
    {
        Vector2 Dis = TargetActor.TransCtrl.position - Actor.TransCtrl.position;
        if (Mathf.Abs(Dis.x) < 2)
        {
            Actor.AnimCtrl.SetBool("Run", false);
            Actor.AICtrl = new InBattleAI(Actor, TargetActor);
            return;
        }        
    }

}
