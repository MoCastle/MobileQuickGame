using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClickFlyState : EnemyHitBackState {
    float Speed = 3;
    public override Vector2 Direction
    {
        get
        {
            return _Actor.ForceMoveDirection;
        }
    }
    // Use this for initialization
    public EnemyClickFlyState( EnemyActor Actor ):base( Actor) {
        RangeTime = _Actor.BeCut.RangeTime;
        SpeedRate = _Actor.BeCut.SpeedRate;
        SetCutMeet();
    }
    public override void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Speed * Direction;
        RotateToDirection(Direction);
    }
}
