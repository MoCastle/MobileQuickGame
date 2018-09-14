using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SenceData
{
    int PlayerSpawnID;
}

public abstract class BaseDir : MonoBehaviour {
    protected Transform MainCamera;
    public Transform CameraTrans
    {
        get
        {
            if( MainCamera == null )
            {
                MainCamera = Camera.main.transform;
            }
            return MainCamera;
        }
    }

    PlayerActor _Player;
    public PlayerActor Player
    {
        get
        {
            return _Player;
        }
        set
        {
            _Player = value;
        }
    }
    // Use this for initialization

    Dictionary<int, BaseActor> ActorMenue;
    protected GameCtrl GM;
    private void Awake()
    {
        CenceMgr.Mgr.CurSenceDir = this;
        ActorMenue = new Dictionary<int, BaseActor>();
        GM = GameCtrl.GameCtrler;
        GM.CurDir = this;
    }

    //生命循环相关
    #region 
    //开幕
    public void StartGame( ScenceMsg InMsg )
    {
        BaseActor Player = SendPlayer(InMsg.JumpID);
        _Player = Player as PlayerActor;
    }

    //谢幕
    public void End()
    {
        foreach (BaseActor Actor in ActorMenue.Values)
        {
            if ((Actor as EnemyActor) != null)
            {
                GamePoolManager.Manager.Despawn(Actor.gameObject.transform);
            }
        }
    }
    #endregion

    //角色生成相关
    #region
    DoorSpawn GetDoor(int ID)
     {
        DoorSpawn DoorSpawn = null;
        Transform DoorTrans = transform.FindChild("Doors");
        if(DoorTrans== null|| ID + 1 > DoorTrans.childCount )
        {

        }else
        {
            Transform SpawnTrans = DoorTrans.GetChild(ID);

            if (SpawnTrans != null)
            {
                DoorSpawn = SpawnTrans.GetComponent<DoorSpawn>();
            }
        }
        
        return DoorSpawn;
    }

    PlayerSpawn GetPlayerSpawn( int ID = 0 )
    {
        PlayerSpawn Spawn = null;
        Transform SpawnTrans = transform.FindChild("PlayerSpawn");
        if( SpawnTrans!= null )
            Spawn = SpawnTrans.GetComponent< PlayerSpawn >();
        return Spawn;
    }

    public BaseActor GenActor( string Name )
    {
        BaseActor NewActor = null;
        return NewActor;
    }
    public BaseActor GenActor(GameObject SampleObj)
    {
        GameObject NewEnemy = GamePoolManager.Manager.GenObj(SampleObj);
        BaseActor NewActor = NewEnemy.GetComponent<EnemyActor>();
        ActorMenue.Add(NewActor.ActorID, NewActor);
        return NewActor;
    }
    public BaseActor SendPlayer( int DoorID )
    {
        BaseSpawn Spawn = null;
        if (DoorID >= 0)
        {
            Spawn = GetDoor(DoorID);
        }else
        {
            Spawn = GetPlayerSpawn();
        }
        BaseActor Player = null;
        if(Spawn != null)
            Player = Spawn.GenActor();
        return Player;
    }
    #endregion
}
