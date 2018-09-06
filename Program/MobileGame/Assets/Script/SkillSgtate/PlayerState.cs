using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : BaseState {
    public override int Layer
    {
        get
        {
            return 1 << LayerMask.NameToLayer("Enemy");
        }
    }

    protected PlayerActor Actor;
    public PlayerState(PlayerActor InActor ):base( InActor )
    {
        Actor = InActor;
        Actor.ReOpenPlatFoot();
        //有的技能需要发招前做修正 让它修
        DealFaceTo();
        if( SkillType != SkillEnum.Idle )
            Actor.PreState = SkillType;
    }

    //处理转向问题
    public virtual void DealFaceTo()
    {
       
    }
}
