using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAICtrler : BaseAICtrler
{
    
    bool InBattle = false;
    Vector2 _BirthPlace;
    public AxeAICtrler(EnemyObj enemyObj, ActionCtrler actionCtrler):base(enemyObj, actionCtrler)
    {
        _BirthPlace = enemyObj.transform.position;
    }

    protected override void GenBehaviour()
    {
        ClearAIList();
        if(_TargetActor !=null)
        {
            float distance = (_ActorObj.transform.position - _TargetActor.transform.position).magnitude;
            if(distance > 3)
            {
                //追击
                TailAddBehaviour(new SpeedUpChasing(_ActorObj, this, 1));
                TailAddBehaviour(new AttackBehaviour(_ActorObj,this,"Attack"));
            }else
            {
                TailAddBehaviour(new NormalBehaviour(_ActorObj, this, 0.8f));
                TailAddBehaviour(new SpeedUpChasing(_ActorObj, this, 1));
                TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "Attack"));
            }
        }
        else
        {
            
            TailAddBehaviour(new CruiseBehaviour(_ActorObj, this, _BirthPlace + _ActorObj.FaceDir * 1));
            TailAddBehaviour(new GuardBehaviour(_ActorObj, this, 1f));
            TailAddBehaviour(new CruiseBehaviour(_ActorObj, this, _BirthPlace - _ActorObj.FaceDir * 1));
            TailAddBehaviour(new GuardBehaviour(_ActorObj, this, 1f));
        }
    }
    
}
