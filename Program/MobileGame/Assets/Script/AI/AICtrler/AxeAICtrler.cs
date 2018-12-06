using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAICtrler : BaseAICtrler
{
    
    bool InBattle = false;
    public AxeAICtrler(EnemyObj enemyObj, ActionCtrler actionCtrler):base(enemyObj, actionCtrler)
    {

    }

    protected override void GenBehaviour()
    {
        ClearAIList();
        if(InBattle)
        {
            HeadAddBehaviour(new CruiseBehaviour( _ActorObj,this ));   
        }else
        {

        }
    }
    
}
