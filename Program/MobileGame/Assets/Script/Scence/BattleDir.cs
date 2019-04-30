/*作者:Mo
 * 用于战斗场景控制
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameScene;

public class BattleDir : BaseDir
{
    PlayerMgr _PlayerMgr;
    [Title("该关卡是否需要存档", "black")]
    public bool IfSaveData = true;
    SceneDoor[] _Doors;
    Transform _DoorCollision;
    Transform DoorCollision
    {
        get
        {
            if(_DoorCollision == null)
            {
                _DoorCollision = (new GameObject("DoorCollision")).transform;
            }
            return _DoorCollision;
        }
    }
    LinkedList<EnemyObj> NPCList;
    //装内存池取出来的角色
    protected LinkedList<BaseActorObj> ActorList;
    public SceneDoor[] Doors
    {
        get
        {
            if (_Doors == null)
            {
                _Doors = new SceneDoor[0];
            }
            return _Doors;
        }
    }

    public PlayerObj PlayerActor;

    //读档
    protected void LoadSceneData()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneData sceneData = PlayerMgr.Mgr.GetSceneData(sceneName);
        NPCList = new LinkedList<EnemyObj>();
        if (sceneData.EnemyArr!=null)
        {
            foreach( CharacterData character in sceneData.EnemyArr )
            {
                if (!character.propty.IsDeath)
                {
                    BaseActorObj newActor = ActorManager.Mgr.GenActor(character.propty.name);
                    NPCList.AddLast(newActor as EnemyObj);
                    newActor.propty = character.propty;
                    ActorList.AddFirst(newActor);
                    newActor.transform.position = character.position;
                }
            }
        }
        if(sceneData.DoorArr!=null)
        {
            _Doors = new SceneDoor[sceneData.DoorArr.Length];
            for(int doorIdx =0;doorIdx< sceneData.DoorArr.Length;++doorIdx)
            {
                DoorData doorData = sceneData.DoorArr[doorIdx];
                GameObject loadObj = Resources.Load<GameObject>("Prefab\\Scene\\Door");
                GameObject instntiateObj = GameObject.Instantiate(loadObj);
                SceneDoor door = instntiateObj.GetComponent<SceneDoor>();
                _Doors[doorIdx] = door;
                doorData.SetData(door);
                door.transform.SetParent(DoorCollision.transform);
            }
        }
    }
    
    //存档
    protected void SaveSceneData()
    {
        if(IfSaveData)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneData sceneData = PlayerMgr.Mgr.GetSceneData(sceneName);
            sceneData.SceneName = sceneName;
            if(NPCList!=null)
            {
                sceneData.EnemyArr = new CharacterData[NPCList.Count];
                LinkedListNode<EnemyObj> actorNode = null;
                for(int idx = 0;idx<NPCList.Count;++idx)
                {
                    actorNode = idx == 0 ? NPCList.First : actorNode.Next;
                    sceneData.EnemyArr[idx].propty = actorNode.Value.propty;
                    sceneData.EnemyArr[idx].position = actorNode.Value.transform.position;
                }
            }
            sceneData.DoorArr = new DoorData[DoorCollision.transform.childCount];
            for (int idx = 0; idx < DoorCollision.transform.childCount; ++idx)
            {
                Transform doorTrans = DoorCollision.transform.GetChild(idx);
                SceneDoor door = doorTrans.GetComponent<SceneDoor>();
                sceneData.DoorArr[idx].WriteData(door);
            }
            PlayerMgr.Mgr.SetSceneData(sceneData);
        }
    }

    //生成玩家
    protected virtual void GenPlayer(Vector3 BirthPS)
    {
        BaseActorObj player = ActorManager.Mgr.GenActor("Player");
        PlayerActor = player as PlayerObj;
        PlayerActor.transform.position = BirthPS;
        ActorList.AddFirst(player);
    }

    //重生玩家
    public virtual void ReBirth()
    {
        PlayerActor.Birth();
    }
    #region 生命周期
    protected override void Awake()
    {
        _PlayerMgr = PlayerMgr.Mgr;
        base.Awake();
        ActorList = new LinkedList<BaseActorObj>();

        _SceneMgr.EnterScene(new BattleScene(this));
    }

    protected override void StartPrepare()
    {
        LoadSceneData();
        BattleSceneInfo info = BattleMgr.Mgr.CurSceneInfo;
        if (PlayerActor == null)
            GenPlayer(info.PS);
        if (PlayerActor != null && Doors.Length > 0 && info.Idx >= 0 && Doors[info.Idx] != null)
            Doors[info.Idx].GenPlayer(PlayerActor.transform);

        if (MainCamera == null && PlayerActor != null)
        {
            MainCamera = Camera.main;
            if (MainCamera == null)
            {
                MainCamera = Resources.Load("Prefab\\Camera\\Main Camera") as Camera;
            }
            CameraObj cameraObj = MainCamera.GetComponent<CameraObj>();
            cameraObj.TraceTarget = PlayerActor.transform.Find("CameraPoint");
        }
        base.StartPrepare();
    }
    protected override void StartComplete()
    {
        base.StartComplete();
        _UIMgr.ShowUI("ScrollArea");
        _UIMgr.ShowUI("MainWindow");
        
    }
    public override bool IsLeaved
    {
        get
        {
            return this.PlayerActor == null && this.ActorList.Count <= 0;
        }
    }
    public override void Leave()
    {
        //this.PlayerActor.Leave();
        //GamePoolManager.Manager.Despawn(this.PlayerActor.transform);
        SaveSceneData();
        while (ActorList.Count > 0)
        {
            BaseActorObj Actor = ActorList.First.Value;
            ActorList.RemoveFirst();
            GamePoolManager.Manager.Despawn(Actor.transform);
            base.Leave();
        }
    }
    #endregion


    //角色退场
    public void CharacterLeave(BaseActorObj baseActorObj)
    {
        ActorList.Remove(baseActorObj);
        GamePoolManager.Manager.Despawn(baseActorObj.transform);

    }

}
 */

