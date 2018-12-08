using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoombieAICtrler : BaseAICtrler
{
    
    bool InBattle = false;
    Vector2 _BirthPlace;
    public ZoombieAICtrler(EnemyObj enemyObj, ActionCtrler actionCtrler):base(enemyObj, actionCtrler)
    {
        _BirthPlace = enemyObj.transform.position;
    }

    protected override void GenBehaviour()
    {
        ClearAIList();
        TailAddBehaviour(new CruiseBehaviour(_ActorObj, this, _BirthPlace + _ActorObj.FaceDir * 1));
        TailAddBehaviour(new GuardBehaviour(_ActorObj, this, 1f));
        TailAddBehaviour(new CruiseBehaviour(_ActorObj, this, _BirthPlace - _ActorObj.FaceDir * 1));
        TailAddBehaviour(new GuardBehaviour(_ActorObj, this, 1f));
    }
    public override void BeBreak()
    {
        float distance = UnityEngine.Random.Range(2f, 4f) * (UnityEngine.Random.Range(0f, 1f) > 0.5 ? 1 : -1);
        base.BeBreak();
        Action EndInfo = ResetPlace;
        HeadAddBehaviour(new RunToBehaviour(_ActorObj, this, distance,"Run", EndInfo));
    }
    public void ResetPlace()
    {
        _BirthPlace = _ActorObj.transform.position;
        GenBehaviour();
    }
}
