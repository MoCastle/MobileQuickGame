using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyAICtrler : AICtrler {
    public override void LogicUpdate()
    {

    }
    public LittleEnemyAICtrler( EnemyActor InActor ) : base(InActor)
    {
        
    }
    
    //进入战斗力状态
    public override void EnterBattle()
    {
        if( CurTarget == null )
        {
            return;
        }
        ResetAIStack();
        Vector2 Dis = CurTarget.TransCtrl.position - Actor.TransCtrl.position;
        if (Target != null)
        {
            //已经几乎脱战了 追
            if(Mathf.Abs(Dis.x) > 5)
            {
                PushAction(new ChasingAction( Actor, this,3f ));
            }
            //最近距离直接打击
            else
            {
                int RandomNum = Random.Range(0, 99);
                if(RandomNum < 60)
                {
                    NormalAttackAI();
                }
                else
                {
                    PushAction(new ChasingAction(Actor, this, 1f));
                    PushAction(new LittleEnemyNormAction(Actor, this, "attack_1", "attack_Close_PT_1"));
                    PushAction(new LittleEnemyNormAction(Actor, this, "attack_2", "attack_Close_PT_2"));
                }
                
            }
        }
    }
    //近战平砍
    public void NormalAttackAI()
    {
        PushAction(new ChasingAction(Actor, this, 1f));
        PushAction(new LittleEnemyNormAction(Actor, this, "attack_1", "attack_Close_PT_1"));
    }
}
