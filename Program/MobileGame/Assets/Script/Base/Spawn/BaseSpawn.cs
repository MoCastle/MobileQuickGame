using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn:ScenceObj {
    protected GameCtrl GM;
    //玩家出生逻辑
    #region
    public BaseActor Create( GameObject Sample )
    {
        BaseActor NewActor = CurDir.GenActor(Sample);
        NewActor.ActorPropty.Init();
        return NewActor;
    }
    
    #endregion
    public void Awake()
    {
        GM = GameCtrl.GameCtrler;
        CurDir = GM.CurDir;
        LogicStart();
    }

    public virtual void LogicStart( )
    {

    }
    public virtual void DeathEvent( )
    {

    }
    public BaseActor GenActor(string actorName)
    {
        BaseActor NewActor = CurDir.GenActor(actorName);
        return NewActor;
    }
    public abstract BaseActor GenActor();
}
