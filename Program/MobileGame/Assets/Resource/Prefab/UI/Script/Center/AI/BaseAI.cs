using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAI {
    LinkedList<BaseAct> ActList;
    static BaseActor _TargetActor;

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
    public virtual void EndAI( )
    { }
}
