using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn:MonoBehaviour {
    protected GameCtrl GM;
    protected BaseDir CenceDir;
    //玩家出生逻辑
    #region
    public BaseActor Create( GameObject Sample )
    {
        BaseActor NewActor = CenceDir.GenActor(Sample);
        NewActor.ActorPropty.Init();
        return NewActor;
    }

    #endregion
    public void Start()
    {
        GM = GameCtrl.GameCtrler;
        CenceDir = GM.CurDir;
        LogicStart();
    }
    public virtual void LogicStart( )
    {

    }
    public virtual void DeathEvent( )
    {

    }

    public abstract BaseActor GenActor();
}
