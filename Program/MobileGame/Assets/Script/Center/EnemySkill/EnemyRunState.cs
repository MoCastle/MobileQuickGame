using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    
    public override void Update()
    {
        Debug.Log("Running");
        Vector2 Speed = _Actor.MoveSpeed * Direction;
        _Actor.Move(Speed);
    }
    public EnemyRunState( EnemyActor InActor ):base(InActor )
    {

    }
}
