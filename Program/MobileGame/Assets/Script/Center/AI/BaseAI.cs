using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAI {
    EnemyActor _Actor;
    public EnemyActor Actor
    {
        get
        {
            return _Actor;
        }
    }
	public BaseAI(EnemyActor InBaseActor )
    {
        _Actor = InBaseActor;
    }
    public virtual void Update( )
    {

    }
}
