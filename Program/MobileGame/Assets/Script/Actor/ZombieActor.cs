using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieActor : EnemyActor {


    public override void EnterGuarding()
    {
        if (_AICtrler != null)
        {
            _AICtrler.ResetAIStack();
        }

        _AICtrler = new ZombieInBattleCtrl(this);
    }
    public override void EnterBattle()
    {
        if (_AICtrler != null)
        {
            _AICtrler.ResetAIStack();
        }
        _AICtrler = new ZombieInBattleCtrl(this);
    }
}
