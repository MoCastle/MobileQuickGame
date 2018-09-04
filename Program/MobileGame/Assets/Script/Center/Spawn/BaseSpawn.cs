using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn:MonoBehaviour {
    protected GameCtrl GM;
    protected BaseDir CenceDir;

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
}
