using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn:MonoBehaviour {
    protected GameCtrl GM;
    protected BaseDir CenceDir;
    //玩家出生逻辑
    #region
    public Transform _PlayerSpawnList;
    public Transform PlayerSpawnList
    {
        get
        {
            if( _PlayerSpawnList == null )
            {
                _PlayerSpawnList = transform.FindChild("PlayerSpawnList");
            }
            return _PlayerSpawnList;
        }
    }

    public BaseSpawn GetPlayerSpawn( int ID )
    {
        ID = ID < 0 ? 0 : ID;
        Transform TargetSpawn = PlayerSpawnList.GetChild(ID);
        if( TargetSpawn == null )
        {
            TargetSpawn = PlayerSpawnList.GetChild(0);
        }
        return TargetSpawn.GetComponent<BaseSpawn>()     ;
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
}
