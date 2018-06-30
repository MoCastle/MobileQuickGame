using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : BaseAI {
    public GuardAI( EnemyActor Actor ):base(Actor)  { }
    // Use this for initialization
    // Update is called once per frame
    public override void Update()
    {
        BaseActor FoundActor = Actor.CheckGetPlayer();
        //检查是否发现玩家
        if(FoundActor != null)
        {
            //一旦发现玩家 立即切换到作战状态
            InBattleAI NewAI = new InBattleAI(Actor, FoundActor);
            Actor.AICtrl = NewAI;
            return;
        }
    }
}
