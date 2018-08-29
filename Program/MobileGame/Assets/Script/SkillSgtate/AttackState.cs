using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AttackState : PlayerState
{

    
    public override SkillEnum SkillType
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public AttackState(PlayerActor InaActor) : base(InaActor) { }
 
}
