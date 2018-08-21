using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyAICtrler : AICtrler {
    public override void LogicUpdate()
    {
        if (AIActionList.First == null && Actor.Alive && Actor.AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            AIActionList = new LinkedList<AIAction>();
            GuardAction Guarding = new GuardAction(Actor, this);
            AddAction(Guarding);
        }
    }
    public LittleEnemyAICtrler( EnemyActor InActor ) : base(InActor)
    {
        
    }
    public void AddAction( AIAction InAction )
    {
        AIActionList.AddLast(InAction);
    }
    
    public override void EnterBattle()
    {
        ResetAIStack();
        Vector2 Dis = CurTarget.TransCtrl.position - Actor.TransCtrl.position;
        if (Target != null)
        {
            //已经几乎脱战了 追
            if(Mathf.Abs(Dis.x) > 5)
            {
                AddAction(new ChasingAction( Actor, this,3f ));
            }
            //最近距离直接打击
            else
            {
                NormalAttackAI();
            }
        }
    }
    //近战平砍
    public void NormalAttackAI()
    {
        AddAction(new LittleEnemyNormAction(Actor, this));
        AddAction(new ChasingAction(Actor, this, 1f));
    }
}
