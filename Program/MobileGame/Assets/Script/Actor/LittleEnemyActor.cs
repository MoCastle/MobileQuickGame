using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemyActor : EnemyActor {
    public override void LogicAwake()
    {
        base.LogicAwake();
    }

    public override void EnterGuarding()
    {
        if( _AICtrler != null )
        {
            _AICtrler.ResetAIStack();
        }
        
        _AICtrler = new GuardAiCtrl(this);
    }
    public override void EnterBattle()
    {
        if (_AICtrler != null)
        {
            _AICtrler.ResetAIStack();
        }
        _AICtrler = new ModEnemInBattleCtrl(this);
    }
}
