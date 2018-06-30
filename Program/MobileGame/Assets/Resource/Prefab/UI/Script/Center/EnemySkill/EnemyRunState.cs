using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    public Vector2 Direction
    {
        get
        {
            return Vector2.left;
        }
    }
    public override void Update()
    {
        Vector2 Speed = _Actor.MoveSpeed * Direction;
        _Actor.Move(Speed);
    }
    EnemyRunState( EnemyActor InActor ):base(InActor )
    {

    }
}
