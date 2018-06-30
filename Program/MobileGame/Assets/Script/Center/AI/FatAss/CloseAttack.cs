using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : BaseAI {
    
    public CloseAttack( EnemyActor InActor ):base( InActor )
    {
        Actor.AnimCtrl.SetTrigger("attack_Close_PT_1");
    }
    public override void Update()
    {
    }
}
