using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFlyFalling : BaseState
{
    public ClickFlyFalling(BaseActor InActor):base(InActor)
    {
        _Actor.RigidCtrl.gravityScale = _Actor.RigidCtrl.gravityScale * 0.6f;
    }
}
