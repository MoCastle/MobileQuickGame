using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

struct SenceData
{
    int PlayerSpawnID;
}

public abstract class BaseDir : MonoBehaviour { 
    public event Action ActionEvent;
    //是否开拍
    bool _IsAction = false;
    public bool IsAction
    {
        get
        {
            return _IsAction;
        }
    }
    protected void ActionAnnounce()
    {
        _IsAction = true;
        if(ActionEvent != null )
        {
            ActionEvent();
        }
        
    }

    public CameraControler CameraCtrler;

    public Camera _MainCamera;
    public Camera MainCamera
    {
        get
        {
            if( _MainCamera == null )
            {
                _MainCamera = Camera.main;
                if (_MainCamera == null)
                    Debug.Log( "");
            }
            return _MainCamera;
        }
    }
    public Transform CameraTrans
    {
        get
        {
            return MainCamera.transform;
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

        //UI相关操作
        WindowMgr.ResetUI();
        WindowMgr.ShowMainWindow();
        GameCtrl.GameCtrler.CenceCtroler.CurCence.Director = this;
    }

    //生命循环相关
    #region 
    //开幕
    public void StartGame( ScenceMsg InMsg )
    {
        ActionAnnounce();
    }
    //每帧更新
    public void Update()
    {
        if (CameraCtrler != null)
            CameraCtrler.Update();
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

    //生成演员
    public BaseActorObj GenActor(string name)
    {
        BaseActorObj newActor = ActorManager.Mgr.GenActor(name);
        /*
        ActorMenue.Add(newActor.ActorID, newActor);
        if (newActor.Type == ActorType.Player)
        {
            Player = newActor as PlayerActor;
        }*/
        return newActor;
    }
    public BaseActor GenActor(GameObject SampleObj)
    {
        GameObject newObj = GamePoolManager.Manager.GenObj(SampleObj);
        BaseActor newActor = newObj.GetComponent<BaseActor>();
        ActorMenue.Add(newActor.ActorID, newActor);
        if(newActor.Type == ActorType.Player)
        {
            Player = newActor as PlayerActor;
        }
        return newActor;
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
        /*
        if(Spawn != null)
            Player = Spawn.GenActor();
            */
        return Player;
    }
    #endregion
}
