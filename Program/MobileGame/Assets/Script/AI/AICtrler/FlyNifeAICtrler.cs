using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNifeAICtrler : BaseAICtrler
{
    
    bool InBattle = false;
    Vector2 _BirthPlace;
    public FlyNifeAICtrler(EnemyObj enemyObj, ActionCtrler actionCtrler):base(enemyObj, actionCtrler)
    {
        _BirthPlace = enemyObj.transform.position;
    }

    protected override void GenBehaviour()
    {
        ClearAIList();
        if(_TargetActor !=null)
        {
            float distance = (_ActorObj.transform.position - _TargetActor.transform.position).magnitude;
            if(distance > 5)
            {
                //追击
                TailAddBehaviour(new ChasingBehaviour(_ActorObj, this, 4,1));
                TailAddBehaviour(new SpeedUpChasing(_ActorObj, this, 3));
                TailAddBehaviour(new AttackBehaviour(_ActorObj,this,"Attack"));
            }else
            {
                if(distance<2)
                {
                    //向后逃
                    TailAddBehaviour(new RunToBehaviour(_ActorObj,this,-4) );
                }
                TailAddBehaviour(new NormalBehaviour(_ActorObj,this,0.8f));
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
