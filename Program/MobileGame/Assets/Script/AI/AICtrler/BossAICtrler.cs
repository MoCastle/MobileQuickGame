using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAICtrler : BaseAICtrler
{
    
    bool InBattle = false;
    Vector2 _BirthPlace;
    public BossAICtrler(EnemyObj enemyObj, ActionCtrler actionCtrler):base(enemyObj, actionCtrler)
    {
        _BirthPlace = enemyObj.transform.position;
    }

    protected override void GenBehaviour()
    {
        ClearAIList();
        if(_TargetActor !=null)
        {
            float distance = Mathf.Abs( _ActorObj.transform.position.x - _TargetActor.transform.position.x);
            if(distance > 2)
            {
                //追击
                TailAddBehaviour(new ChasingBehaviour(_ActorObj, this, 1));
                if( Random.Range(0f, 1f)> 0.5)
                {
                    TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "attack_1"));
                }else
                {
                    TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "attack_2"));
                }
            }
            else
            {
                float range = Random.Range(0f, 1f);
                if(range>0.7)
                {
                    TailAddBehaviour(new NormalBehaviour(_ActorObj, this, 0.2f));
                    TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "attack_1"));
                }else if(range > 0.5)
                {
                    TailAddBehaviour(new NormalBehaviour(_ActorObj, this, 0.4f));
                    TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "attack_2"));
                }else
                {
                    TailAddBehaviour(new NormalBehaviour(_ActorObj, this, 0.3f));
                    TailAddBehaviour(new AttackBehaviour(_ActorObj, this, "attack_3"));
                }
                
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
