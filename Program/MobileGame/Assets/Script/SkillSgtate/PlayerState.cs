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
    }
}
