using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAction :AIAction {
    EnemyActor Enemy;
	public GuardAction( EnemyActor Actor, AICtrler Ctrler ):base( Actor, Ctrler )
    {
        Enemy = Actor;
    }
    public override void LogicUpdate()
    {
        if (_Ctrler.CurTarget != null)
        {
            //_Ctrler.EnterBattle();
            return;
        }

        BaseActor FoundActor = Enemy.CheckGetPlayer();
        //检查是否发现玩家
        if( FoundActor != null )
        {
            _Ctrler.FoundTarget(FoundActor);
            //_Ctrler.EnterBattle();
        }

        
    }
    public override void Start()
    {
    }
}
